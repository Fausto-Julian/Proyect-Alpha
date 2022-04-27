using System;
using System.Collections;
using System.Collections.Generic;
using _main.Scripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")] 
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    
    [Space]
    
    [Header("Refence")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D _body;
    private bool _isGround;
    private float _move;
    private bool _isHit;

    private bool _isLeft = false;
    private bool _isRight = false;
    private bool _isUp = false;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _move = 0;
        animator.SetBool("isRun", false);
        if (_isRight)
        {
            _move += 1;
            animator.SetBool("isRun", true);
            spriteRenderer.flipX = false;
        }
        
        if(_isLeft)
        {
            _move -= 1;
            animator.SetBool("isRun", true);
            spriteRenderer.flipX = true;
        }

        #region eliminar al buildear
        
        _move = Input.GetAxisRaw("Horizontal");

        if (_move < 0)
        {
            animator.SetBool("isRun", true);
            spriteRenderer.flipX = true; 
        }

        if (_move > 0)
        {
            animator.SetBool("isRun", true);
            spriteRenderer.flipX = false; 
        }
        
        #endregion
        
        
        _move *= speed * Time.deltaTime;
        if (!_isGround)
        {
            _move *= 0.75f;
        }

        if (!_isHit)
        {
            transform.position += new Vector3(_move, 0f);
        }

        _isGround = Physics2D.OverlapCircle(checkGround.position, 0.2f, whatIsGround);

        animator.SetBool("isGrounded", _isGround);
        
        if (_isUp && _isGround && !_isHit)
        {
            _isUp = false;
            _body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
            Invoke(nameof(DisableAnimationJump), 0.5f);
        }
    }

    private void DisableAnimationJump()
    {
        animator.SetBool("isJump", false); 
    }

    public void DownButtonLeftHandler()
    {
        _isLeft = true;
    }
    
    public void UpButtonLeftHandler()
    {
        _isLeft = false;
    }
    
    public void DownButtonRightHandler()
    {
        _isRight = true;
    }
    
    public void UpButtonRightHandler()
    {
        _isRight = false;
    }
    
    public void DownButtonUpHandler()
    {
        _isUp = true;
    }
    
    public void UpButtonUpHandler()
    {
        _isUp = false;
    }
    
    public void ActiveHitAnim()
    {
        animator.SetBool("isHit", true);
        Invoke(nameof(DisableAnimationHit), 0.5f);
    }

    //Todo: Mejorar el get damage
    public void GetDamage()
    {
        animator.SetBool("isHit", true);
        _isHit = true;
        _body.gravityScale = 0;
        _body.velocity = Vector2.zero;
        Invoke(nameof(DisableAnimationHit), 0.5f);
        Invoke(nameof(ActiveBody), 0.5f);
    }
    
    private void DisableAnimationHit()
    {
        _isHit = false;
        animator.SetBool("isHit", false); 
    }

    private void ActiveBody()
    {
        _body.gravityScale = 1;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("HeadEnemy"))
        {
            _body.velocity = new Vector2(_body.velocity.x, 0f);
            _body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            col.GetComponentInParent<HealthController>().GetDamage(50);
            animator.SetBool("isJump", true);
            Invoke(nameof(DisableAnimationJump), 0.5f);
        }
    }
}
