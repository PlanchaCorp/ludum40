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
        Debug.Log("LoadScene");
    }
    public void quit()
    {
        
        Application.Quit();
        Debug.Log("Quit");
    }
}
