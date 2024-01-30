using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMobile : MonoBehaviour
{
    [SerializeField] PauseMenu pause;
    public bool usedFromButtonPause = false;


    public void Pause()
    {
        usedFromButtonPause = !pause.isPaused;
        Debug.Log("pause button + " + !pause.isPaused);
    }
}
