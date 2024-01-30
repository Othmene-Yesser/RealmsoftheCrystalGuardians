using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GobackToGameMenu : MonoBehaviour
{
    Animator animator;
    bool isEnded;

    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        isEnded= false;
    }

    // Update is called once per frame
    void Update()
    {
        isEnded = animator.GetBool("CreditsEnd");
        if (isEnded)
        {
            SceneManager.LoadScene("GameMenu");
        }
    }
}
