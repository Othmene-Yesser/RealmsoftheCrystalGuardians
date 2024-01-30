using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeChase : MonoBehaviour
{
    [SerializeField] private FlyingEye[] enemyArray;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FlyingEye enemy in enemyArray)
            {
                enemy.chaseEye = true;
            }
        }
    }
}
