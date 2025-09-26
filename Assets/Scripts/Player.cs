using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int coins;
    public int health = 100;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Image healthImage;

    public AudioClip jumpClip;
    public AudioClip hurtClip;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    public int extraJumpsValue = 1;
    private int extraJumps;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        extraJumps = extraJumpsValue;
    }

    
    void Update()
    {
        //move left and right
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // flip player theo hướng di chuyển
        if (moveInput > 0.01f)
            spriteRenderer.flipX = false; // quay phải
        else if (moveInput < -0.01f)
            spriteRenderer.flipX = true;  // quay trái

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                PlaySFX(jumpClip);
            }
            else if (extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
                PlaySFX(jumpClip);
            }
        }

        SetAnimation(moveInput);

        healthImage.fillAmount = health / 100f;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    //Player animation
    private void SetAnimation(float moveInput)
    {
        
        if (isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_Run");
            }
        }
        else
        {
            if(rb.linearVelocityY >0)
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_Fall");
            }
        }
    }

    // get damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            PlaySFX(hurtClip);
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }
        else if (collision.gameObject.tag == "BouncePad")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 2);
        }
        else if (collision.gameObject.CompareTag("FlyEnemy"))
        {
            // player nhảy lên khi chạm enemy
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            PlaySFX(jumpClip);
            StartCoroutine(BlinkRed());
            health -= 25;
        }
    }
    //red color when get damage
    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void PlaySFX(AudioClip audioClip, float volume = 1f) 
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
    }

}
