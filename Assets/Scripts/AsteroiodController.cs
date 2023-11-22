using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroiodController : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D AsteroidObject;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float size;
    public GameObject asteroidPrefab;
    public bool isSplit = false; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AsteroidObject = GetComponent<Rigidbody2D>();

        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }


        if (!isSplit)
        {
            size = Random.Range(minSize, maxSize);
            transform.localScale = Vector3.one * size;
            AsteroidObject.mass = size;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            AsteroidObject.AddForce(directionToPlayer * 100f); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            if ((size * 0.5f) >= minSize)
            {
                CreateSplitAsteroid(size * 0.5f);
                CreateSplitAsteroid(size * 0.5f);
            }

            Destroy(gameObject); 
        }
    }

    void CreateSplitAsteroid(float newSize)
    {
        Vector2 positionOffset = Random.insideUnitCircle * 0.5f; 
        Vector3 newPosition = transform.position + new Vector3(positionOffset.x, positionOffset.y, 0);

        GameObject newAsteroid = Instantiate(asteroidPrefab, newPosition, Quaternion.identity);
        AsteroiodController newAsteroidController = newAsteroid.GetComponent<AsteroiodController>();

        if (newAsteroidController != null)
        {
            newAsteroidController.size = newSize;
            newAsteroidController.isSplit = true; 
            newAsteroid.transform.localScale = Vector3.one * newSize;
            Rigidbody2D rb = newAsteroid.GetComponent<Rigidbody2D>();
            rb.mass = newSize;
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.AddForce(randomDirection * rb.mass * 10f); 
        }

    }
}
