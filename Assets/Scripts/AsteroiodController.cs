using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroiodController : MonoBehaviour
{
    private Rigidbody2D AsteroidObject;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    private Vector2 directionToPlayer;
    public float lifeTime = 10.0f;

    void Start()
    {
        AsteroidObject = GetComponent<Rigidbody2D>();

        float size = Random.Range(minSize, maxSize);
        this.transform.localScale = Vector3.one * size;
        AsteroidObject.mass = size;

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        {
            directionToPlayer = (player.transform.position - transform.position).normalized;
            AsteroidObject.AddForce(directionToPlayer * 100f); 
        }
        Destroy(gameObject, lifeTime);
    }
}
