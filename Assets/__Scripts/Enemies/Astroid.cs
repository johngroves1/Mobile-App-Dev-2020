using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    public int ScoreValue
    {
        set { scoreValue = value; }
        get { return scoreValue; }
    }
    [SerializeField] private float speed;
    [SerializeField] private float rotate_speed = 50f;
    [SerializeField] private int scoreValue = 100;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject explosionFX;

    private int death;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private float explosionDuration = 1.0f;

    public delegate void AstroidKilled(Astroid enemy); // Event type
    public static AstroidKilled AstroidKilledEvent; // Event handler

    void Start(){

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        speed = Random.Range(5f, 10f);

        if(Random.Range(0,2) > 0)
        {   
            rotate_speed = Random.Range(rotate_speed, rotate_speed + 20f);
            rotate_speed *= -1f;
        } 
        else 
        {
            rotate_speed = Random.Range(rotate_speed, rotate_speed + 20f);
        }
        }

    void Update()
    {
        Move();
        RotateEnemy();
    }

    void Move()
    {       
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            transform.position = temp;
    }

    // Rotates Astroid in randon direction
    void RotateEnemy()
    {
        transform.Rotate(new Vector3(0f, 0f, rotate_speed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter2D(Collider2D externalObject)
    {   
        // Hit by player bullets
        var whatHitMe = externalObject.GetComponent<Bullet>();
        // For Power Up bullets
        var whatHitMe2 = externalObject.GetComponent<PowerUpBullet>();
        if(whatHitMe)
        {
            // Counter for Hp
            death ++;

            Destroy(whatHitMe.gameObject);

            // Astroid has 3 HP
            if(death == 3)
            {
            // Play the sound using the Sound Controller
            SoundController sc = FindObjectOfType<SoundController>();
            if(sc)
            {              
                sc.PlayOneShot(hitSound);
            }
            PublishAstroidKilledEvent();

            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);

            Destroy(gameObject);
            } 
        }
         else if(whatHitMe2)
            {
 
                // Play the sound using the Sound Controller
                SoundController sc = FindObjectOfType<SoundController>();
                if(sc)
                {
                
                    sc.PlayOneShot(hitSound);
                }
                // destroy the bullet
                Destroy(whatHitMe2.gameObject);
                
                // publis this event (enemy died) to the system
                PublishAstroidKilledEvent();

                // Show the explosion - has playOnAwake set to true, so it runs automatically
                GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
                Destroy(explosion, explosionDuration);

                // destroy the enemy - the current game object
                Destroy(gameObject);
            }
    }
    
    private void PublishAstroidKilledEvent(){

         // If some part of the system is listening, then publish
        if(AstroidKilledEvent != null)
        {
            AstroidKilledEvent(this);
        }
    }

}


