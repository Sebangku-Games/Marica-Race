using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public int amountGreenClickInARow;

    private int amountToGetBoost = 5;

    private void Start()
    {
        amountGreenClickInARow = 0;
        
    }

    public void UpdateBoost(){
        amountGreenClickInARow++;
        CheckAmountGreenClickInARow();
    }

    private void CheckAmountGreenClickInARow(){
        if (amountGreenClickInARow > amountToGetBoost){
            GameManager.instance.isBoosting = true;
        }
    }

    public void ResetAmountGreenClickInARow(){
        amountGreenClickInARow = 0;
        GameManager.instance.isBoosting = false;
    }


}
