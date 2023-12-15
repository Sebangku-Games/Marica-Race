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
    public Image panelRound;
    public ParticleSystem gren;
    public ParticleSystem red;

    private void Start()
    {
        HideRoundOverPanel();
        HideGameOverPanel();
        //StartCountdownCoroutine();
        panelRound.gameObject.SetActive(false);
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
        UpdateRoundOverText();
    }

    internal void HideRoundOverPanel()
    {
        roundOverPanel.SetActive(false);
    }

    public void UpdateRoundOverText(){
        roundOverPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Round " + GameManager.instance.roundManager.currentRound + " Over";
    }

    public void UpdatePanelRoundText()
    {
        panelRound.GetComponentInChildren<TextMeshProUGUI>().text = "Ronde " + GameManager.instance.roundManager.currentRound;
    }

    public void ShowTextArea(string area)
    {
        areaText.text = "" + area;
        
        // Set color based on the current area
        Color textColor;
        Color outlineColor = Color.black;
        float outlineWidth = 0.3f; // You can adjust the outline width as needed
        switch (area)
        {
            case "Green Area":
                textColor = Color.green;
                areaText.text = "Mantap";
                gren.Play();
                break;
            case "Yellow Area":
                textColor = Color.yellow;
                areaText.text = "Lumayan";
                break;
            case "Red Area":
                textColor = Color.red;
                areaText.text = "Kasian";
                red.Play();
                break;
            default:
                textColor = Color.white;
                break;
        }

        // Set the color of the TextMeshPro text
        areaText.color = textColor;

        // Add outline to the text
        areaText.outlineColor = outlineColor;
        areaText.outlineWidth = outlineWidth;
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
        UpdatePanelRoundText();
        StartCoroutine(StartCountdown());
    }

    

    IEnumerator StartCountdown()
    {

        panelRound.gameObject.SetActive(true);
        for (int i = 0; i < countdownImages.Length; i++)
        {
            countdownImages[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            countdownImages[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.3f);


        panelRound.gameObject.SetActive(false);
        GameManager.instance.StartGame();
    }

}
