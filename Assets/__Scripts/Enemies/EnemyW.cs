using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyW : MonoBehaviour
{
     public int ScoreValue
    {
        set { scoreValue = value; }
        get { return scoreValue; }
    }
    public GameObject enemy_bullet;
    [SerializeField] private Transform attack_point;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private int scoreValue = 350;
    private float explosionDuration = 1.0f;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public delegate void EnemyWKilled(EnemyW enemy); // Event type
    public static EnemyWKilled EnemyWKilledEvent; // Event handler

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Invoke("StartShooting", Random.Range(1f, 3f));    
    }

    void StartShooting() 
    {
        GameObject bullet = Instantiate(enemy_bullet, attack_point.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().is_EnemyBullet = true;

        Invoke("StartShooting", Random.Range(1f, 3f));
    }

   private void OnTriggerEnter2D(Collider2D externalObject)
   {
        var whatHitMe = externalObject.GetComponent<Bullet>();
        // For Power Up bullets
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
            PublishEnemyWKilledEvent();

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
            PublishEnemyWKilledEvent();

            // Show the explosion - has playOnAwake set to true, so it runs automatically
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);

            // destroy the enemy - the current game object
            Destroy(gameObject);
        }
   }

   private void PublishEnemyWKilledEvent()
    {   
        // If some part of the system is listening, then publish
        if(EnemyWKilledEvent != null)
        {
            EnemyWKilledEvent(this);
        }
        
    }


}
