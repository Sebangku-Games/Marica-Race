using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Scrollbar scrollBarImage;

    // Start is called before the first frame update
    void Start()
    {
        // set the scroll bar value to random
        RandomNumber();
    }


    // make random number and assign it to slider value
    public void RandomNumber()
    {
        int randomNumber = Random.Range(0, 100);
        // adjust the randomNumber to 0 ~ 1 for the slider value
        scrollBarImage.value = randomNumber / 100f;
    }

    
    public void ShowBarImage()
    {
        scrollBarImage.gameObject.SetActive(true);
    }

    public void HideBarImage()
    {
        scrollBarImage.gameObject.SetActive(false);
    }
}
