using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Leaderboards : MonoBehaviour
{
    const string IdLeaderboard = "CgkI_fC6_IEPEAIQAg";

    //[SerializeField] private GameObject loginPanel;

    public void ShowLeaderboardUI()
    {
        // check if player is authenticated
        if (Social.localUser.authenticated) // player has login
        {
            // show leaderboard
            Social.ShowLeaderboardUI();
        } else { 
            // player hasnt login, try authenticate
            Social.localUser.Authenticate(success =>
            {
                if (success)
                {
                    Social.ShowLeaderboardUI();
                }
                else
                { 
                    //loginPanel.SetActive(true);
                }
            });
        }
    }


    // Add score to leaderboard for each scene
    public void AddScoreToLeaderboard(int score)
    {
        if (Social.localUser.authenticated)
        {
            ReportScore(score, IdLeaderboard);
        }
    }

    public void ReportScore(int score, string leaderboardId)
    {
        Social.ReportScore(score, leaderboardId, success =>
        {
            if (success)
            {
                Debug.Log("Update Score Success");
            }
            else
            {
                Debug.Log("Update Score Failed");
            }
        });
    }


}