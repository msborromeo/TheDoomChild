using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DChild.Gameplay
{
    public class VFXSpawner : MonoBehaviour
    {
        [SerializeField, ShowIf("m_useNonAssetReference"), LabelText("FX")]
        private GameObject m_placeHolderFX;
        [SerializeField]
        private bool m_useNonAssetReference;
        [SerializeField, HideIf("m_useNonAssetReference")]
        private AssetReferenceFX m_fx;
        private bool m_usePooling;
        private FXSpawnHandle<FX> m_fxHandle;

        public void Set(AssetReferenceFX fx)
        {
            //m_fx = fx;
            //m_usePooling = ((GameObject)m_fx.Asset).GetComponent<FX>();
        }

        public void Spawn()
        {
            if (m_useNonAssetReference)
            {
                var fx = m_fxHandle.InstantiateFX(m_placeHolderFX, Vector3.zero, transform);
                fx.transform.localPosition = Vector3.zero;
                fx.transform.localScale = Vector3.one;
                fx.transform.parent = null;
                return;
            }

            //if (m_usePooling)
            //{
            //    throw new NotImplementedException();
            //}
            //else
            //{
            //    AddressableSpawner.Spawn(m_fx, transform.position, 0, OnSpawn);
            //}
        }

        private void OnSpawn(GameObject instance, int arg2)
        {
            instance.transform.parent = transform;
            instance.transform.localScale = Vector3.one;
            instance.transform.parent = null;
            instance.GetComponent<FX>().Play();
        }

        private void Awake()
        {
            //m_usePooling = ((GameObject)m_fx.Asset).GetComponent<FX>();
        }
    }
}