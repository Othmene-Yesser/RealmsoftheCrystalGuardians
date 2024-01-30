using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileInput : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] TouchingDirections touchingDirections;
    [SerializeField] PlayerController p;

    public void Right()
    {
        p.movement = Vector2.right;
        if (p.IsAlive)
        {
            p.IsMoving = p.movement != Vector2.zero;
            SetFacingDirection(p.movement);
        }
        else
        {
            p.IsMoving = false;
        }

    }
    public void Left()
    {
        p.movement = Vector2.left;
        if (p.IsAlive)
        {
            p.IsMoving = p.movement != Vector2.zero;
            SetFacingDirection(p.movement);
        }
        else
        {
            p.IsMoving = false;
        }
    }
    public void Stop()
    {
        p.movement = Vector2.zero;
        if (p.IsAlive)
        {
            p.IsMoving = p.movement != Vector2.zero;
            SetFacingDirection(p.movement);
        }
        else
        {
            p.IsMoving = false;
        }
    }
    public void Run()
    {
        p.IsRunning = !p.IsRunning;
    }
    public void Jump()
    {
        //to do if no alive no jump
        if (touchingDirections.IsGrounded && p.CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, p.jumpImpulse);
        }
    }

    public void Attack()
    {
        animator.SetTrigger(AnimationStrings.attackTrigger);
    }

    public void Fire()
    {
        animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
    }


    private void SetFacingDirection(Vector2 movement)
    {
        if (movement.x > 0 && !p.IsFacingRight)
        {
            //face right 
            p.IsFacingRight = true;
        }
        else if (movement.x < 0 && p.IsFacingRight)
        {
            //face left
            p.IsFacingRight = false;
        }
    }
}
