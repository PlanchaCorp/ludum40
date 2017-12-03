using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenuUI : MonoBehaviour
{

    public Canvas CanvasUI;

    public void loadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void quit()
    {
        Application.Quit();
    }
}
