using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int ScoreValue
    {
        set { scoreValue = value; }
        get { return scoreValue; }
    }

    [SerializeField] private int scoreValue = 100;
    [SerializeField] private float enemySpeed = 10.0f;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject explosionFX;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private float explosionDuration = 1.0f; // Time to wait before destroying the explosionFX 

    public delegate void EnemyKilled(EnemyBehaviour enemy); // Event type
    public static EnemyKilled EnemyKilledEvent; // Event handler

    // == private methods ==
    private void Start()
    {
        // get the rigidbody
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // apply velocity to the rigidbody
        rb.velocity = new Vector2(0f, -1 * enemySpeed);
    }
   private void OnTriggerEnter2D(Collider2D externalObject)
    {
        var whatHitMe = externalObject.GetComponent<Bullet>();
        var whatHitMe2 = externalObject.GetComponent<PowerUpBullet>();
        
        if(whatHitMe)
        {
           
            // Play the sound using the Sound Controller
            SoundController sc = FindObjectOfType<SoundController>();
            if(sc)
            {
               
                sc.PlayOneShot(hitSound);
            }
            
            // destroy the bullet
            Destroy(whatHitMe.gameObject);
            

             // publis this event (enemy died) to the system
            PublishEnemyKilledEvent();

            // Show the explosion - has playOnAwake set to true, so it runs automatically
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);

            // destroy the enemy - the current game object
            Destroy(gameObject);
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
            PublishEnemyKilledEvent();

            // Show the explosion - has playOnAwake set to true, so it runs automatically
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);

            // destroy the enemy - the current game object
            Destroy(gameObject);
        }
    }
    private void PublishEnemyKilledEvent()
    {   
        // If some part of the system is listening, then publish
        if(EnemyKilledEvent != null)
        {
            EnemyKilledEvent(this);
        }
        
    }
}
