using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private int gameScene = 1;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject highScoreButton;
    [SerializeField] GameObject highScoreExitButton;
    [SerializeField] GameObject highScorePanel;
   
    public void ExitGame()
    {
        exitButton.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void StartGameScene()
    {
        startButton.GetComponent<AudioSource>().Play();
        StartCoroutine(StartGame());
        
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadSceneAsync(gameScene);
    }
   public void HighScorePanel()
    {
        highScoreButton.GetComponent<AudioSource>().Play();
        highScorePanel.SetActive(true); 
    }
    public void CloseHighScorePanel()
    {
        highScoreExitButton.GetComponent<AudioSource>().Play();
        highScorePanel.SetActive(false);
    }
}
