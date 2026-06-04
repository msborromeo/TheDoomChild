using DChild.Gameplay.Systems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DChild.Menu.Codex
{
    public class CodexPageHandler : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private CodexPage m_currentPage;
        public CodexPage currentPage => m_currentPage;
        public void SetCurrentPage(CodexPage page) => m_currentPage = page;

        public void Reset()
        {
            m_currentPage = CodexPage.Home;
        }
    }
}