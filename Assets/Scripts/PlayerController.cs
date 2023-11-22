using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7.0f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Text livesText; 
    public int lives = 3;
    public Vector2 respawnPosition; 
    public float fallThreshold = -10f; 
    public AudioSource jumpSoundEffect;
    public AudioSource shootSoundEffect;
    public AudioSource damageSoundEffect;

    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Transform platformParent; 

    void Awake()                          
    {
        var players = FindObjectsOfType<PlayerController>();
        if (players.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        platformParent = transform.parent;
        UpdateLivesDisplay(); 
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleShooting();
        CheckForFall();
    }

    private void HandleMovement()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(dirX, 0);
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        anim.SetBool("walking", dirX != 0);
        if (dirX != 0) sprite.flipX = dirX < 0;
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpSoundEffect.Play();
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.SetBool("flying", true);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;
            sprite.flipX = mousePosition.x < transform.position.x;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;
            shootSoundEffect.Play();
        }
    }

    private void CheckForFall()
    {
        if (transform.position.y < fallThreshold) HandleFall();
    }

    void HandleFall()
    {
        transform.position = respawnPosition; 
        if (lives > 0) lives--;
        UpdateLivesDisplay();
        if (lives <= 0) SceneManager.LoadScene("YouLost"); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "MovingPlatform")
        {
            isGrounded = true;
            anim.SetBool("flying", false);
        }
        else if (collision.collider.tag == "Asteroid" || collision.collider.tag == "Enemy")
        {
            HandleCollisionWithKnockback(collision);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "MovingPlatform")
        {
            isGrounded = false;
        }
    }

    private void HandleCollisionWithKnockback(Collision2D collision)
    {
        if (lives > 0)
        {
            lives--;
            livesChanged = true; 
            UpdateLivesDisplay();
            damageSoundEffect.Play();

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            float knockbackForce = 15f; 
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);


            if (collision.collider.tag == "Asteroid")
            {
                Destroy(collision.gameObject);
            }
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene("YouLost");
        }
    }

    private bool livesChanged = true;

    private void UpdateLivesDisplay()
    {
        if (livesChanged && livesText != null)
        {
            livesText.text = "Lives: " + lives;
            livesChanged = false;
        }
    }

    public int GetLives() => lives;
}
