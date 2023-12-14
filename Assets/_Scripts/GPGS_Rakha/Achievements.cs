using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Achievements : MonoBehaviour
{
    const string ID_ACH_FIRSTROUNDCLEAR = "CgkI_fC6_IEPEAIQAw";
    const string ID_ACH_FIFTHROUNDCLEAR = "CgkI_fC6_IEPEAIQBA";
    const string ID_ACH_TENTHROUNDCLEAR = "CgkI_fC6_IEPEAIQBQ";
    const string ID_ACH_FIFTEENTHROUNDCLEAR = "CgkI_fC6_IEPEAIQBg";
    const string ID_ACH_TWENTIETHROUNDCLEAR = "CgkI_fC6_IEPEAIQBw";
    const string ID_ACH_BANTENGMARAH = "CgkI_fC6_IEPEAIQCA";
    const string ID_ACH_BOOST = "CgkI_fC6_IEPEAIQCQ";
    const string ID_ACH_HIDDEN = "CgkI_fC6_IEPEAIQCg";

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


    // public void UnlockAchievementLogin(){
    //     ReportProgressAchievement(Ach_Login, 100f);
    // }

    // unlock achievement for each
    public void UnlockAchievement(int index)
    {
        switch (index)
        {
            case 1:
                ReportProgressAchievement(ID_ACH_FIRSTROUNDCLEAR, 100f);
                break;

            case 2:
                ReportProgressAchievement(ID_ACH_FIFTHROUNDCLEAR, 100f);
                break;

            case 3:
                ReportProgressAchievement(ID_ACH_TENTHROUNDCLEAR, 100f);
                break;
            
            case 4:
                ReportProgressAchievement(ID_ACH_FIFTEENTHROUNDCLEAR, 100f);
                break;
            
            case 5:
                ReportProgressAchievement(ID_ACH_TWENTIETHROUNDCLEAR, 100f);
                break;

            case 6:
                ReportProgressAchievement(ID_ACH_BANTENGMARAH, 100f);
                break;

            case 7:
                ReportProgressAchievement(ID_ACH_BOOST, 100f);
                break;

            case 8: //belum, TODO
                ReportProgressAchievement(ID_ACH_HIDDEN, 100f);
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