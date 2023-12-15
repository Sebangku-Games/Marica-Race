using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void gantiscene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void keluar()
    {
        Application.Quit();
    }
}
