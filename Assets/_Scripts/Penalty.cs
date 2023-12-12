using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty : MonoBehaviour
{
    public int amountRedClickInARow;

    public int amountToGetPenalty = 4;

    private void Start()
    {
        amountRedClickInARow = 0;
    }

    public void UpdatePenalty(){
        amountRedClickInARow++;
        CheckAmountRedClickInARow();
    }

    private void CheckAmountRedClickInARow(){
        if (amountRedClickInARow > amountToGetPenalty){
            GameManager.instance.isPenalty = true;
            
            GameManager.instance.player.SetPenaltyAnimation(true);
        }
    }

    public void ResetAmountRedClickInARow(){
        amountRedClickInARow = 0;
        GameManager.instance.isPenalty = false;
        
        GameManager.instance.player.SetPenaltyAnimation(false);
    }
}
