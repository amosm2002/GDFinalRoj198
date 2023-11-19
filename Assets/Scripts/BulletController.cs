using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float maxLifetime = 10.0f;

    void Start()
    {
        Destroy(gameObject, maxLifetime);
    }
}
