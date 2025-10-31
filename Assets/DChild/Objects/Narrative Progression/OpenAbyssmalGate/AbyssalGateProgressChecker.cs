using DChild.Gameplay;
using DChild.Gameplay.Environment.Interractables;
using DChild.Gameplay.Inventories;
using DChild.Gameplay.Items;
using Holysoft.Event;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbyssalGateProgressChecker : MonoBehaviour , IButtonToInteract
{
    PlayerInventory m_inventory;
    [SerializeField]
    ItemData[] FragmentsReference = new ItemData[6];
    [SerializeField]
    List<GameObject> Fragments = new List<GameObject>();
    [SerializeField]
    private Vector3 m_promptOffset;

    private int m_fragmentsDeposited;
    public event EventAction<EventActionArgs> InteractionOptionChange;

    public UnityEvent OnUse,QuestCompleted;
    public bool showPrompt => true;

    public string promptMessage => "Ehh";

    public Vector3 promptPosition => transform.position + m_promptOffset;

    // Start is called before the first frame update
    void Start()
    {
        m_inventory = GameplaySystem.playerManager.player.inventory;
    }


    private void CheckPlayerInventoryForFragment(int x)
    {
        if (m_inventory.GetItem(FragmentsReference[x]) == null)
        {
            Fragments[x].SetActive(true);
        }
    }

    public void CheckForFragmentQuest()
    {
        if(Fragments.Count<=0)
        {
            return;
        }
        Fragments[0].SetActive(true);
        OnUse.AddListener(Fragments[0].GetComponent<DialogueSystemTrigger>().OnUse);
    }

    public void RemoveFragmentQust(GameObject fragment)
    {
        Fragments.Remove(fragment);
        OnUse.RemoveListener(fragment.GetComponent<DialogueSystemTrigger>().OnUse);
        CheckForFragmentQuest();
        m_fragmentsDeposited++;
        if(m_fragmentsDeposited>=7)
        {
            QuestCompleted?.Invoke();
        }
    }

    public void AddCompletedFragmentQuest(GameObject fragment)
    {
        Fragments.Add(fragment);
        CheckForFragmentQuest();
    }

    public void CheckPlayerInventoryForFragmentsToInsert()
    {
        for(int x =0;x<FragmentsReference.Length-1;x++)
        {
            if (m_inventory.GetItem(FragmentsReference[x]) == null)
            {
                Fragments[x].SetActive(true);
            }
        }
    }

    public void TurnAllFragmentsOn()
    {
        foreach (GameObject fragment in Fragments)
        {
            fragment.SetActive(true);
        }
    }

    public void RemoveFragmentFromInventory(ItemData fragment)
    {
        m_inventory.RemoveItem(fragment);
    }

    public void Interact(Character character)
    {
        OnUse?.Invoke();
    }
}
