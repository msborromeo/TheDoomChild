using DChild.Gameplay.Systems.Journal;
using System;

namespace DChild.Localization
{
    public interface IJournalDataLocalizer
    {
        event Action<JournalData> LocalizeJournal;
    }
}
