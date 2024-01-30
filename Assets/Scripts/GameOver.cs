using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameOverTrigger gameOverTrigger;
    [SerializeField] Damagable damageable;
    [SerializeField] PauseMenu pauseNotBug;
    bool gameOverUiUpdate = false;
    [SerializeField] bool hasUpdatedIsAlive = false;
    [SerializeField] bool isAliveLocal = true;
    bool bugHotFix;

    private void Start()
    {
        bugHotFix = true;
    }

    private void Update()
    {   
        gameOverUiUpdate = gameOverTrigger.gameOver;

        if (!hasUpdatedIsAlive)
        {
            isAliveLocal = damageable.IsAlive;
            if (!isAliveLocal)
            {
                gameOverUiUpdate = true;
                hasUpdatedIsAlive= true;
            }
        }
        if (isAliveLocal && Time.timeScale == 0f && !gameOverUiUpdate && bugHotFix && !pauseNotBug.isPaused)
        {
            Time.timeScale = 1f;
            Debug.Log("I love hotFixing Bugs ^_~ ^_____^ ^_+ ");
        }
        if (gameOverUiUpdate == true)
        {
            Debug.Log("GameOver");
            healthBar.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Timescale = " + Time.timeScale + " f");
            gameOverTrigger.gameOver = false;
            bugHotFix = false;
        }
    }

    public void Restart()
    {
        healthBar.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            Debug.Log("Time has resumed timescale = "+ Time.timeScale + " f");
        }
        hasUpdatedIsAlive = false;
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        healthBar.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
        if (Time.timeScale == 0)
        {
            Debug.Log("Time has resumed");
            Time.timeScale = 1f;
        }
        hasUpdatedIsAlive = false;
        SceneManager.LoadScene("GameMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
