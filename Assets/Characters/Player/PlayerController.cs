using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damagable))]
public class PlayerController : MonoBehaviour
{
    public Vector2 movement;
    [SerializeField] Transform Player;
    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float runningSpeed = 8f;

    private bool _isMoving = false;
    private bool _isRunning = false;
    private bool _isFacingRight = true;

    public float jumpImpulse = 5f;
    
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    [SerializeField] private Animator animator;
    Damagable damageable;
    

    public bool IsFacingRight { get 
        {
            return _isFacingRight;
        }
        set
        {
            if (_isFacingRight != value) 
            {
                //flip the local scale because the calue is new 
                Player.transform.localScale *= new Vector2(-1, 1); 
            }
            _isFacingRight = value;
        }
    }

    public bool IsMoving{
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving= value;
            animator.SetBool(AnimationStrings.isMoving,value);
        }
    }
    
    public bool IsRunning
    {
        get
        {
            return _isRunning; ;
        }
        set
        {
            _isRunning= value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    public float CurrentMoveSpeed
    {get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (IsRunning)
                    {
                        return runningSpeed;
                    }
                    else
                    {
                        return walkingSpeed;
                    }
                }
                else
                {
                    //idle
                    return 0;
                }
            }
            else
            {
                //Movement locked
                return 0;
            }
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damagable>();
    }
    private void FixedUpdate()
    {
        if (!damageable.IsHit)
        {
            rb.velocity = new Vector2(movement.x * CurrentMoveSpeed, rb.velocity.y);
        }
        
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = movement != Vector2.zero;
            SetFacingDirection(movement);
        }
        else
        {
            IsMoving = false;
        }
        
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning= false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        //to do if no alive no jump
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }

    }

    private void SetFacingDirection(Vector2 movement)
    {
        if (movement.x > 0 && !IsFacingRight)
        {
            //face right 
            IsFacingRight= true;
        }
        else if (movement.x < 0 && IsFacingRight)
        {
            //face left
            IsFacingRight= false;
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }

    }

}
