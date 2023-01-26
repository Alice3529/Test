using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas winCanvas;
    [SerializeField] Canvas loseCanvas;
    [SerializeField] Canvas pauseCanvas;

     public void Pause()
     {
        Time.timeScale = 0f;
        pauseCanvas.enabled = true;
     }

    public void Continue()
    {
        pauseCanvas.enabled = false;
        Time.timeScale = 1f;
    }

    public void Again()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Win()
    {
        winCanvas.enabled = true;
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        loseCanvas.enabled = true;
        Time.timeScale = 0f;
    }

}
