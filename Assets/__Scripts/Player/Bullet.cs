using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
    // look after bullet movement
    public float BulletSpeed = 25.0f;

    // private fields
    private Rigidbody2D rb;
    [SerializeField] private float bulletLife = 0.5f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bulletLife);
    }

    private void Update()
    {
        //bullet travels from bottom to top
        rb.velocity = new Vector2(0f, BulletSpeed);
    }
}

