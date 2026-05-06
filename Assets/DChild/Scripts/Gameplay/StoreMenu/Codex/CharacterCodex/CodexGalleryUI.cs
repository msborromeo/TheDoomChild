namespace DChild.Codex.Characters
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class CodexGalleryUI<InfoType, ProgressTracker> : MonoBehaviour
    {
        [Header("Data & Progress")]
        [SerializeField] protected ProgressTracker m_playerTracker;

        [SerializeField, Tooltip("EDITOR ONLY: Bypasses progression checks")]
        protected bool m_revealAllData;
        
        protected List<InfoType> m_filteredList = new List<InfoType>();
        public List<InfoType> FilteredList => m_filteredList;
        protected abstract void RetrieveEntries();
        protected abstract bool CheckPlayerProgress(InfoType data);
        public abstract void SetupGalleryEntries();
        public virtual void Initialize()
        {
            RetrieveEntries();
            SetupGalleryEntries();
        }

        public event Action<InfoType> OnGalleryEntryReceived;
        public virtual void SetPopupEntryData(InfoType data) => OnGalleryEntryReceived.Invoke(data);
        protected virtual void Awake()
        {
            Initialize();
        }
    }
}