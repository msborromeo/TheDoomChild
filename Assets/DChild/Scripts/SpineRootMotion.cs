/******************************************************************************
 * Spine Runtimes Software License v2.5
 *
 * Copyright (c) 2013-2016, Esoteric Software
 * All rights reserved.
 *
 * You are granted a perpetual, non-exclusive, non-sublicensable, and
 * non-transferable license to use, install, execute, and perform the Spine
 * Runtimes software and derivative works solely for personal or internal
 * use. Without the written permission of Esoteric Software (see Section 2 of
 * the Spine Software License Agreement), you may not (a) modify, translate,
 * adapt, or develop new applications using the Spine Runtimes or otherwise
 * create derivative works or improvements of the Spine Runtimes or (b) remove,
 * delete, alter, or obscure any trademarks or any copyright, trademark, patent,
 * or other intellectual property or proprietary rights notices on or in the
 * Software, including any copy thereof. Redistributions in binary or source
 * form must include this license and terms.
 *
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE "AS IS" AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL ESOTERIC SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES, BUSINESS INTERRUPTION, OR LOSS OF
 * USE, DATA, OR PROFITS) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System;

// Spine Root Motion for Spine-Unity 3.6

namespace Spine.Unity.Modules
{
    /// <summary>
    /// Add this component to a Spine GameObject to replace root bone motion into Transform or RigidBody motion.
    /// Only compatible with SkeletonAnimation (or other components that implement ISkeletonComponent, ISkeletonAnimation and IAnimationStateComponent)
    /// Set SpineRootMotion.enabled to enable and disable root motion override.
    /// </summary>
    public class SpineRootMotion : MonoBehaviour
    {
        #region Inspector
        [SpineBone]
        [SerializeField]
        protected string sourceBoneName = "root";
        public bool useX = true;
        public bool useY = false;

        [Tooltip("OPTIONAL Rigidbody2D: Set this if you want this component to apply the root motion to a Rigidbody2D.")]
        public Rigidbody2D rb;

        [SpineBone]
        [SerializeField]
        protected List<string> siblingBoneNames = new List<string>();

        [Header("Pinned Bone Options")]
        [SpineBone]
        [SerializeField]
        [Tooltip("The bone that should remain stationary in world space despite root motion.")]
        protected string pinBoneName = "";
        #endregion

        protected Bone bone;
        protected int boneIndex;
        public readonly List<Bone> siblingBones = new List<Bone>();

        // Pinned Bone Cache
        protected Bone pinBone;
        private Vector3 pinnedUnityWorldPos; // Tracks absolute Unity World position
        private bool hasPinnedWorldPos = false;
        private bool isPinningEnabled = false; // Starts disabled by default

        ISkeletonComponent skeletonComponent;
        AnimationState state;
        bool useRigidBody;

        Vector2 accumulatedDisplacement;

        [ContextMenu("Refresh Sibling Bones")]
        public void RefreshSiblingBones()
        {
            bone = GetComponent<ISkeletonComponent>().Skeleton.FindBone(sourceBoneName);
            if (bone == null) return;
            Bone boneParent = bone.Parent;

            siblingBoneNames.Clear();
            if (Application.isPlaying)
                siblingBones.Clear();

            if (boneParent != null)
            { // was root bone
                foreach (var b in boneParent.Children)
                {
                    if (b != bone) siblingBoneNames.Add(b.Data.Name);
                }

                if (Application.isPlaying)
                {
                    foreach (var b in boneParent.Children)
                    {
                        if (b != bone) siblingBones.Add(b);
                    }
                }
            }
        }

        void Start()
        {
            skeletonComponent = GetComponent<ISkeletonComponent>();

            var s = skeletonComponent as ISkeletonAnimation;
            if (s != null)
            {
                s.UpdateLocal += HandleUpdateLocal;
                s.UpdateWorld += HandleUpdateWorld;
            }

            var sa = skeletonComponent as IAnimationStateComponent;
            if (sa != null) this.state = sa.AnimationState;

            SetSourceBone(sourceBoneName);

            // Find and cache the bone reference, but keep the behavior disabled
            if (!string.IsNullOrEmpty(pinBoneName))
            {
                pinBone = s.Skeleton.FindBone(pinBoneName);
                if (pinBone == null)
                {
                    Debug.LogWarning("Pin Bone named \"" + pinBoneName + "\" could not be found.");
                }
            }

            var skeleton = s.Skeleton;
            siblingBones.Clear();
            foreach (var bn in siblingBoneNames)
            {
                var b = skeleton.FindBone(bn);
                if (b != null) siblingBones.Add(b);
            }

            useRigidBody |= (rb != null);
        }

        void HandleUpdateLocal(ISkeletonAnimation animatedSkeletonComponent)
        {
            if (!this.isActiveAndEnabled) return; // Root motion is only applied when component is enabled.

            // 1. Capture the absolute Unity world coordinates BEFORE the parent moves or root motion shifts anything
            if (isPinningEnabled && pinBone != null && !hasPinnedWorldPos)
            {
                pinnedUnityWorldPos = pinBone.GetWorldPosition(transform);
                hasPinnedWorldPos = true;
            }

            Vector2 localDelta = Vector2.zero;
            TrackEntry current = state.GetCurrent(0); // Only apply root motion using AnimationState Track 0.

            TrackEntry track = current;
            TrackEntry next = null;
            int boneIndex = this.boneIndex;

            while (track != null)
            {
                var a = track.Animation;
                var tt = a.FindTranslateTimelineForBone(boneIndex);

                if (tt != null)
                {
                    float start = track.AnimationLast;
                    float end = track.AnimationTime;
                    Vector2 currentDelta;
                    if (start > end)
                        currentDelta = (tt.Evaluate(end) - tt.Evaluate(0)) + (tt.Evaluate(a.Duration) - tt.Evaluate(start));  // Looped
                    else if (start != end)
                        currentDelta = tt.Evaluate(end) - tt.Evaluate(start);  // Non-looped
                    else
                        currentDelta = Vector2.zero;

                    float mix;
                    if (next != null)
                    {
                        if (next.MixDuration == 0)
                        {
                            mix = 1;
                        }
                        else
                        {
                            mix = next.MixTime / next.MixDuration;
                            if (mix > 1) mix = 1;
                        }
                        float mixAndAlpha = track.Alpha * next.InterruptAlpha * (1 - mix);
                        currentDelta *= mixAndAlpha;
                    }
                    else
                    {
                        if (track.MixDuration == 0)
                        {
                            mix = 1;
                        }
                        else
                        {
                            mix = track.Alpha * (track.MixTime / track.MixDuration);
                            if (mix > 1) mix = 1;
                        }
                        currentDelta *= mix;
                    }

                    localDelta += currentDelta;
                }

                next = track;
                track = track.MixingFrom;
            }

            var skeleton = animatedSkeletonComponent.Skeleton;
            if (skeleton.ScaleX <= 0) localDelta.x = -localDelta.x;
            if (skeleton.ScaleY <= 0) localDelta.y = -localDelta.y;

            if (useRigidBody)
            {
                accumulatedDisplacement += (Vector2)transform.TransformVector(localDelta);
            }
            else
            {
                transform.Translate(localDelta, Space.Self);
            }

            foreach (var b in siblingBones)
            {
                if (useX) b.X -= bone.X;
                if (useY) b.Y -= bone.Y;
            }

            if (useX) bone.X = 0;
            if (useY) bone.Y = 0;
        }

        void HandleUpdateWorld(ISkeletonAnimation animatedSkeletonComponent)
        {
            if (!this.isActiveAndEnabled || !isPinningEnabled || pinBone == null || !hasPinnedWorldPos) return;

            // Convert Unity Global space back into Spine Skeleton space first
            Vector3 skeletonSpacePos = transform.InverseTransformPoint(pinnedUnityWorldPos);

            // Convert Spine Skeleton space into the Pinned Bone's local parent space safely
            pinBone.Parent.WorldToLocal(skeletonSpacePos.x, skeletonSpacePos.y, out float localX, out float localY);

            pinBone.X = localX;
            pinBone.Y = localY;
        }

        /// <summary>
        /// Enables world-locking for the bone specified in the Unity inspector.
        /// </summary>
        public void EnablePinnedBone()
        {
            if (pinBone != null)
            {
                isPinningEnabled = true;
                hasPinnedWorldPos = false; // Triggers fresh absolute position capture
            }
            else
            {
                Debug.LogWarning("Cannot enable pin bone. No valid bone was specified in the inspector field 'Pin Bone Name'.");
            }
        }

        /// <summary>
        /// Disables pinning and lets the bone snap back to its regular parent-influenced animation.
        /// </summary>
        public void ResetPinnedBone()
        {
            isPinningEnabled = false;
            hasPinnedWorldPos = false;
        }

        void FixedUpdate()
        {
            if (this.isActiveAndEnabled && this.useRigidBody)
            {
                Vector2 v = rb.velocity;
                if (useX) v.x = accumulatedDisplacement.x / Time.fixedDeltaTime;
                if (useY) v.y = accumulatedDisplacement.y / Time.fixedDeltaTime;
                rb.velocity = v;
                accumulatedDisplacement = Vector2.zero;
            }
        }

        public void SetSourceBone(string name)
        {
            var skeleton = skeletonComponent.Skeleton;
            int bi = skeleton.FindBoneIndex(name);
            if (bi >= 0)
            {
                this.boneIndex = bi;
                this.bone = skeleton.Bones.Items[bi];
            }
            else
            {
                Debug.Log("Bone named \"" + name + "\" could not be found.");
                this.boneIndex = 0;
                this.bone = skeleton.RootBone;
            }
        }

        public static Bone GetRootBranchOf(Bone b)
        {
            if (b == null) return null;
            Bone rootBone = b.Skeleton.RootBone;
            if (b.Parent == null) return null;
            if (b.Parent == rootBone) return b;

            const int BoneSearchLimit = 500;
            for (int i = 0; i < BoneSearchLimit; i++)
            {
                Bone parent = b.Parent;
                if (parent == rootBone) return b;
                b = parent;
            }

            return null;
        }

        void OnDisable()
        {
            accumulatedDisplacement = Vector2.zero;
            ResetPinnedBone();
        }

        public void EnableRootMotion(bool v1, bool v2)
        {
            useX = v1;
            useY = v2;
        }
    }

    public static class TimelineTools
    {
        public static TranslateTimeline FindTranslateTimelineForBone(this Animation a, int boneIndex)
        {
            foreach (var t in a.Timelines)
            {
                var tt = t as TranslateTimeline;
                if (tt != null && tt.BoneIndex == boneIndex)
                    return tt;
            }
            return null;
        }

        public static Vector2 Evaluate(this TranslateTimeline tt, float time, SkeletonData skeletonData = null)
        {
            const int PREV_TIME = -3, PREV_X = -2, PREV_Y = -1;
            const int X = 1, Y = 2;

            var frames = tt.Frames;
            if (time < frames[0]) return Vector2.zero;

            float x, y;
            if (time >= frames[frames.Length - TranslateTimeline.ENTRIES])
            {
                x = frames[frames.Length + PREV_X];
                y = frames[frames.Length + PREV_Y];
            }
            else
            {
                int frame = Animation.BinarySearch(frames, time, TranslateTimeline.ENTRIES);
                x = frames[frame + PREV_X];
                y = frames[frame + PREV_Y];
                float frameTime = frames[frame];
                float percent = tt.GetCurvePercent(frame / TranslateTimeline.ENTRIES - 1, 1 - (time - frameTime) / (frames[frame + PREV_TIME] - frameTime));

                x += (frames[frame + X] - x) * percent;
                y += (frames[frame + Y] - y) * percent;
            }

            Vector2 o = new Vector2(x, y);

            if (skeletonData == null)
            {
                return o;
            }
            else
            {
                var boneData = skeletonData.Bones.Items[tt.BoneIndex];
                return o + new Vector2(boneData.X, boneData.Y);
            }
        }
    }
}