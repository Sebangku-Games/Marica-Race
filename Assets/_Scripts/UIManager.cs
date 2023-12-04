using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI areaText;
    public GameObject roundOverPanel;
    public GameObject gameOverPanel;
    public Image[] countdownImages;
    

    private void Start()
    {
        HideRoundOverPanel();
        HideGameOverPanel();

        //StartCountdownCoroutine();
    }

    internal void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    internal void ShowRoundOverPanel()
    {
        hideText();
        roundOverPanel.SetActive(true);
    }

    internal void HideRoundOverPanel()
    {
        roundOverPanel.SetActive(false);
    }

    public void ShowTextArea(string area)
    {
        areaText.text = "" + area;

        // Set color based on the current area
        Color textColor;
        switch (area)
        {
            case "Green Area":
                textColor = Color.green;
                areaText.text = "Mantap";
                break;
            case "Yellow Area":
                textColor = Color.yellow;
                areaText.text = "Lumayan";
                break;
            case "Red Area":
                textColor = Color.red;
                areaText.text = "Kasian";
                break;
            default:
                textColor = Color.white;
                break;
        }

        // Set the color of the TextMeshPro text
        areaText.color = textColor;

        // Optionally, you can add more UI-related logic here
    }

    internal void ShowAreaText(string area)
    {
        ShowTextArea(area);
    }

    private void hideText()
    {
        areaText.text = "";
    }

    public void StartCountdownCoroutine()
    {
        StartCoroutine(StartCountdown());
    }

    

    IEnumerator StartCountdown()
    {
        for (int i = 0; i < countdownImages.Length; i++)
        {
            countdownImages[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            countdownImages[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.3f);
        GameManager.instance.StartGame();
    }

}
