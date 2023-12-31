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
    public TMP_Text gameOverText;
    public Image[] countdownImages;
    public Image panelRound;
    public ParticleSystem gren;
    public ParticleSystem red;
    public ParticleSystem finish1;
    public ParticleSystem finish2;
    public GameObject panelRoundGameplay;
    public GameObject giveUpButton;

    private void Start()
    {
        panelRoundGameplay.SetActive(false);
        giveUpButton.SetActive(false);
        HideRoundOverPanel();
        HideGameOverPanel();
        //StartCountdownCoroutine();
        panelRound.gameObject.SetActive(false);
    }

    public void PlayButtonSoundSFX()
    {
        AudioManager.instance.PlayButton();
    }

    internal void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        UpdateGameOverText();
    }

    private void UpdateGameOverText()
    {
        // get text component on child[3]
        gameOverText.text = "KAMU KALAH RONDE " + GameManager.instance.roundManager.currentRound;
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
        finish1.Play();
        finish2.Play();
    }

    internal void HideRoundOverPanel()
    {
        roundOverPanel.SetActive(false);
    }

    public void UpdateRoundOverText(){
        roundOverPanel.GetComponentInChildren<TextMeshProUGUI>().text = "KAMU MENANG RONDE " + GameManager.instance.roundManager.currentRound;
    }

    public void UpdatePanelRoundText()
    {
        panelRound.GetComponentInChildren<TextMeshProUGUI>().text = "Ronde " + GameManager.instance.roundManager.currentRound;
    }

    public void UpdatePanelRoundGameplay(){
        panelRoundGameplay.GetComponentInChildren<TextMeshProUGUI>().text = "Ronde " + GameManager.instance.roundManager.currentRound;
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
        UpdatePanelRoundGameplay();
        StartCoroutine(StartCountdown());
    }

    

    IEnumerator StartCountdown()
    {
        AudioManager.instance.PlayCountdownSfx();
        panelRound.gameObject.SetActive(true);
        for (int i = 0; i < countdownImages.Length; i++)
        {
            countdownImages[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            countdownImages[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.3f);


        panelRound.gameObject.SetActive(false);
        panelRoundGameplay.SetActive(true);
        giveUpButton.SetActive(true);
        GameManager.instance.StartGame();
    }

}
