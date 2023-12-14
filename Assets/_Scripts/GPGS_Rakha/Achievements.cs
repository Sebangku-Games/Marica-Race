using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Achievements : MonoBehaviour
{
    const string Ach_Login = "CgkIi8Dh0-sKEAIQCw";
    // HIDDEN ACHIEVEMENT
    const string Ach_ClearLessThan2Seconds = "CgkIi8Dh0-sKEAIQAw";
    const string Ach_KetemuHewanBuas = "CgkIi8Dh0-sKEAIQCg";

    // REVEALED ACHIEVEMENT
    //const string Ach_IdTestLOL = "CgkIi8Dh0-sKEAIQBA";
    const string Ach_IdHutan = "CgkIi8Dh0-sKEAIQBQ";
    const string Ach_IdGurun = "CgkIi8Dh0-sKEAIQBg";
    const string Ach_IdEs = "CgkIi8Dh0-sKEAIQBw";
    const string Ach_IdAir = "CgkIi8Dh0-sKEAIQCA";
    const string Ach_IdSeluruhWilayah = "CgkIi8Dh0-sKEAIQCQ";

    //[SerializeField] private GameObject loginPanel;

    public void ShowAchievementUI()
    {
        // check if player is authenticated
        if (Social.localUser.authenticated) // player has login
        {
            // show achievement
            Social.ShowAchievementsUI();
        } else { 
            // player hasnt login, try authenticate
            Social.localUser.Authenticate(success =>
            {
                if (success)
                {
                    Social.ShowAchievementsUI();
                }
                else
                { 
                    //loginPanel.SetActive(true);
                }
            });
        }
    }


    public void UnlockAchievementLogin(){
        ReportProgressAchievement(Ach_Login, 100f);
    }

    // unlock achievement for each
    public void UnlockAchievement(string achievementName)
    {
        switch (achievementName)
        {
            case "Hutan":
                ReportProgressAchievement(Ach_IdHutan, 100f);
                break;
            case "Gurun":
                ReportProgressAchievement(Ach_IdGurun, 100f);
                break;
            case "Es":
                ReportProgressAchievement(Ach_IdEs, 100f);
                break;
            case "Air":
                ReportProgressAchievement(Ach_IdAir, 100f);
                break;
            case "SeluruhWilayah":
                ReportProgressAchievement(Ach_IdSeluruhWilayah, 100f);
                break;
            case "ClearLessThan2Seconds":
                ReportProgressAchievement(Ach_ClearLessThan2Seconds, 100f);
                break;
            case "HewanBuas":
                ReportProgressAchievement(Ach_KetemuHewanBuas, 100f);
                break;
            default:
                break;
        }
    }


    public void ReportProgressAchievement(string achievementId, float progress){
        Social.ReportProgress(achievementId, progress, success => {
            if (success)
            {
                Debug.Log("Achievement Unlocked");
            }
            else
            {
                Debug.Log("Achievement Failed");
            }
        });
    }
}