using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScreen : MonoBehaviour
{
    public void loadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the next scene index
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void quit()
    {
        Application.Quit();
    }
}
