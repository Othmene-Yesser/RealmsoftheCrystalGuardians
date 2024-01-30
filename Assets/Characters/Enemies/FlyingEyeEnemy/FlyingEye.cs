using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Goblin;

public class FlyingEye : MonoBehaviour
{
    public DetectionZone biteDetectionZone;
    public Collider2D deathCollider;

    Animator animator;
    Rigidbody2D rb;
    Damagable damageable;
    GameObject player;

    public bool _hasTarget = false;
    public bool chaseEye = false;

    [SerializeField] float speed = 0f;

    public enum WalkDirections {Right,Left};
    private WalkDirections _walkDirection;

    public WalkDirections WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        private set
        {
            if (_walkDirection != value)
            {
                //Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }

            _walkDirection= value;
        }
    }

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        animator= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damagable>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //private void OnEnable()
    //{
    //    damageable.damageableDeath += OnDeath();
    //}

    private void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (player == null)
            {
                return;
            }
            if (chaseEye)
            {
                Chase();
                Flip();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }


    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position ,speed * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        if (gameObject.transform.position.x > player.transform.position.x )
        {
            WalkDirection = WalkDirections.Right;
        }
        else
        {
            WalkDirection = WalkDirections.Left;
        }
    }

    public void OnDeath()
    {
        //die then fall to the ground destroy object then 
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }

}
