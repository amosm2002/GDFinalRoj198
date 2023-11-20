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
    private SpriteRenderer sprite;

    private Animator anim;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Transform platformParent; 

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
        float dirX = Input.GetAxisRaw("Horizontal");
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }

        if (dirX > 0f)
        {
            anim.SetBool("walking", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("walking", true);  
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        anim.SetBool("flying", true);
    }

    void ShootBullet()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        
        if (mousePosition.x > transform.position.x)
        {
            sprite.flipX = false; 
        }
        else if (mousePosition.x < transform.position.x)
        {
            sprite.flipX = true;  
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "MovingPlatform")
        {
            isGrounded = true;
            anim.SetBool("flying", false);
            if (collision.collider.tag == "MovingPlatform")
            {
                this.transform.SetParent(collision.transform);
            }
        }
        else if (collision.collider.tag == "Asteroid")
        {
            HandleAsteroidCollision(collision);
        }
        if (collision.collider.tag == "Enemy")
        {
            HandleEnemyCollision();
        }

        if (collision.collider.tag == "EnemyHead")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(3);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);
            }
    }
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "MovingPlatform")
        {
            isGrounded = false;
            if (collision.collider.tag == "MovingPlatform" && this.transform.parent == collision.transform)
            {
                this.transform.SetParent(platformParent); 
            }
        }
    }

    private void HandleEnemyCollision()
    {
        if (lives > 0)
        {
            lives--;
            UpdateLivesDisplay();
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene("StartMenu");
        }
}
    void HandleAsteroidCollision(Collision2D collision)
    {
        if (lives > 0)
        {
            lives--;
            UpdateLivesDisplay();
            Destroy(collision.gameObject);
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }  

    void UpdateLivesDisplay()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    public int GetLives()
    {
        return lives;
    }

}
