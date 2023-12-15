using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textoutline : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    Color outlineColor = Color.black;
    float outlineWidth = 0.3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text1.outlineColor = outlineColor;
        text1.outlineWidth = outlineWidth;

        text2.outlineColor = outlineColor;
        text2.outlineWidth = outlineWidth;
    }
}
