using DChild.Gameplay.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSnapShotManager : MonoBehaviour
{
    [SerializeField]
    private VolumeMixerManagerHandle volumeMixerManagerHandle;

    public void UseSnapShot(int snapshot)
    {
        volumeMixerManagerHandle.UseSnapshot((AudioSnapshot)snapshot);
    }

    void Start()
    {
        if (volumeMixerManagerHandle == null)
        {
            volumeMixerManagerHandle = GetComponent<VolumeMixerManagerHandle>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
