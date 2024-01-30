using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public bool gameOver = false;
    [SerializeField]
    FallOuOfMap[] colliders;

    private void Update()
    {
        foreach (FallOuOfMap collider in colliders)
        {
            if (collider.gameOver)
            {
                gameOver = true;
                collider.gameOver= false;
            }
        }
    }
}
