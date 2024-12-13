using DChild.Gameplay.Characters.Players;
using Holysoft.Collections;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

/*public struct ChestStateChangeEvent : IEventActionArgs
{
    public ChestStateChangeEvent(Chest index, bool isOpened)
    {
        this.index = index;
        this.isOpened = isOpened;
    }

    public Chest index { get; }
    public bool isOpened { get; }
}*/
public class ChestTrigger : MonoBehaviour
{
    [SerializeField]
    private bool m_isOpened;
    public bool isOpened => m_isOpened;
    public EventAction<EventActionArgs> OnChestOpened;
    public void SetState(bool isOpened)
    {
        SetStateAs(isOpened);
        OnChestOpened?.Invoke(this, EventActionArgs.Empty);
        //RevealValueChange?.Invoke(this, new ChestStateChangeEvent(m_chestIndex, m_isOpened));
    }
    public void SetStateAs(bool isOpened)
    {
        m_isOpened = isOpened;
    }
    /*public void SetStateAs(Chest state)
    {
        SetStateAs(state.HasFlag(m_chestIndex));
    }*/
    private void UpdateState()
    {
        SetState(m_isOpened);
    }
}
