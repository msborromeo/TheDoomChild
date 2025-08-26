using Holysoft.Collections;
using Holysoft.Event;
using UnityEngine;

namespace DChild.Codex.Quests.UI
{
    public class CodexIndexUIHandle : MonoBehaviour, IPageHandle
    {
        public int currentPage => throw new System.NotImplementedException();

        public event EventAction<EventActionArgs> PageChange;

        public int GetTotalPages()
        {
            throw new System.NotImplementedException();
        }

        public void NextPage()
        {
            throw new System.NotImplementedException();
        }

        public void PreviousPage()
        {
            throw new System.NotImplementedException();
        }

        public void SetPage(int pageIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}