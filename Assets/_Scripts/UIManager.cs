using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject roundOverPanel;

    private void Start()
    {
        HideRoundOverPanel();
    }

    internal void ShowRoundOverPanel()
    {
        roundOverPanel.SetActive(true);
    }

    internal void HideRoundOverPanel()
    {
        roundOverPanel.SetActive(false);
    }
}
