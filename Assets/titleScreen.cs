using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreen : MonoBehaviour
{
    [SerializeField] GameObject howtoPlay;
    [SerializeField] GameObject credit;
    [SerializeField] GameObject mainMenu;

    private void Start()
    {
        mmOpen();
        Time.timeScale = 1f;
    }

    public void loadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the next scene index
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void howTo()
    {
        howtoPlay.SetActive(true);
        credit.SetActive(false);
        mainMenu.SetActive(false);
    }
    public void creditOpen()
    {
        howtoPlay.SetActive(false);
        credit.SetActive(true);
        mainMenu.SetActive(false);
    }public void mmOpen()
    {
        howtoPlay.SetActive(false);
        credit.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }
}
