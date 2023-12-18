using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public Button onSound;
    public Button offSound;
    private void Start()
    {
        // Add listener for the onSound button
        onSound.onClick.AddListener(AudioManager.instance.ToggleSound);
        // Add listener for the offSound button
        offSound.onClick.AddListener(AudioManager.instance.ToggleSound);
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
