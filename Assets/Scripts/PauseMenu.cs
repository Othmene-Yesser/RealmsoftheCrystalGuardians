using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField]
    Damagable damagable;
    bool isAlive;
    [SerializeField]
    //PauseMobile mobileInput;

    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        isAlive = damagable.IsAlive;
        if (Input.GetKeyUp(KeyCode.Escape) /*|| mobileInput.usedFromButtonPause*/)
        {
            /*mobileInput.usedFromButtonPause = !mobileInput.usedFromButtonPause;*/
            if (isAlive == true)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale= 0f;
        isPaused= true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused= false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
