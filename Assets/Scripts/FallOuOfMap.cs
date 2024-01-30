using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOuOfMap : MonoBehaviour
{
    public bool gameOver = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameOver = true;
        }
    }
}
