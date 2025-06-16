using DChild.Gameplay.UI;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextToButtonUIManager : MonoBehaviour
{
    [SerializeField]
    private List<SetTextToTextBox> m_textBoxList = new List<SetTextToTextBox>();
    [SerializeField]
    private CurrentDeviceType m_currentDeviceType;

    public CurrentDeviceType currentDevice { get { return m_currentDeviceType; } set { m_currentDeviceType = value; } }

    [SerializeField]
    private TMP_Dropdown m_dropDown;

    public event Action OnDeviceTypeChange;
    private int m_dropDownIndex = 0; 
    [Button]
    private void PopulateList()
    {
        var currentList = FindObjectsOfType<SetTextToTextBox>();
        for(int x = 0; x < currentList.Length; x++)
        {
            m_textBoxList.Add(currentList[x]);
        }
        
    }

    public void SetDeviceType()
    {
        m_dropDownIndex = m_dropDown.value;

        m_currentDeviceType = (CurrentDeviceType)m_dropDownIndex;

        //SetTextToTextBox.ChangeDeviceType(m_currentDeviceType);

        

        //for (int x = 0; x < m_textBoxList.Count; x++)
        //{
        //    m_textBoxList[x].deviceType = m_currentDeviceType;
        //}
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
