using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public Button onSound;
    public Button offSound;
    public Button achievementButton;
    public Button leaderboardButton;

    private void Start()
    {
        // Add listener for the onSound button
        onSound.onClick.AddListener(AudioManager.instance.ToggleSound);
        // Add listener for the offSound button
        offSound.onClick.AddListener(AudioManager.instance.ToggleSound);

        achievementButton.onClick.AddListener(GooglePlayGamesServices.instance.GetComponent<Achievements>().ShowAchievementUI);
        leaderboardButton.onClick.AddListener(GooglePlayGamesServices.instance.GetComponent<Leaderboards>().ShowLeaderboardUI);

        SetActiveButton();
    }

    private void SetActiveButton(){
        if (AudioManager.instance.onSound){
            onSound.gameObject.SetActive(true);
            offSound.gameObject.SetActive(false);
        } else {
            onSound.gameObject.SetActive(false);
            offSound.gameObject.SetActive(true);
        }
    }

    public void gantiscene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void keluar()
    {
        Debug.Log("Keluar");
        Application.Quit();
    }

}
