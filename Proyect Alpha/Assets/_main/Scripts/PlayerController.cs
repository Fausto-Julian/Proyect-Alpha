using System;
using _main.Scripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")] 
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private bool betterJump;
    
    [Space]
    
    [Header("Refence")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D _body;
    private bool _isGround;
    private bool _isHit;
    private bool _canJump;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _isGround = Physics2D.OverlapCircle(checkGround.position, 0.2f, whatIsGround);
        
        if (Input.GetKey(KeyCode.Space) && _isGround && !_isHit)
        {
            animator.SetBool("isJump", true);
            _body.velocity += new Vector2(0f, jumpSpeed);
            _body.velocity = new Vector2(_body.velocity.x, Mathf.Clamp(_body.velocity.y, 0f, 8f));
            _canJump = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _canJump && !_isGround)
        {
            animator.SetBool("isDoubleJump", true);
            animator.SetBool("isJump", false);
            animator.SetBool("isFalling", false);
            _body.velocity = new Vector2(_body.velocity.x, doubleJumpForce);
            _canJump = false;
        }
        
        if (betterJump)
        {
            if (_body.velocity.y < 0)
            {
                _body.velocity += Vector2.up * Physics.gravity.y * 0.5f * Time.deltaTime;
            }

            if (_body.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                _body.velocity += Vector2.up * Physics.gravity.y * 1 * Time.deltaTime;
            }
        }
        
        if (_body.velocity.x > 0.5 || _body.velocity.x < -0.5)
        {
            animator.SetBool("isRun", true);
        }

        if (_isGround)
        {
            animator.SetBool("isFalling", false);
        }
        
        if (_body.velocity.y < -1)
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isDoubleJump", false);
            animator.SetBool("isFalling", true);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _body.velocity = new Vector2(-speed, _body.velocity.y);
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _body.velocity = new Vector2(speed, _body.velocity.y);
            spriteRenderer.flipX = false; 
        }
        else
        {
            _body.velocity = new Vector2(0f, _body.velocity.y);
            animator.SetBool("isRun", false);
        }
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
            _body.velocity = new Vector2(_body.velocity.x, jumpSpeed);
            col.GetComponentInParent<HealthController>().GetDamage(50);
            animator.SetBool("isJump", true);
        }
    }
}
