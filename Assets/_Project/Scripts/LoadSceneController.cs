using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneController : MonoBehaviour
{
    public string sceneName = "";
    public void LoadScene()
    {
        LoadSceneName(sceneName);
    }

    public void LoadSceneName (string name)
    {
        SceneManager.LoadScene(name);
    }
}
