using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    // Get and return score value
    public int ScoreValue
    {
        set { scoreValue = value; }
        get { return scoreValue; }
    }

    [SerializeField] private int scoreValue = 500;
    [SerializeField] private float enemySpeed = 10.0f;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private Transform attack_point;
    [SerializeField] private Transform attack_point2;

    private int death;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private float explosionDuration = 1.0f;
    public GameObject enemy_bullet;

    public delegate void EnemyTankKilled(EnemyTank enemy); // Event type
    public static EnemyTankKilled EnemyTankKilledEvent; // Event handler

    private void Start()
    {
        // get the rigidbody
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        Invoke("StartShooting", Random.Range(1f, 3f));
    }

    private void Update()
    {
        // apply velocity to the rigidbody
        rb.velocity = new Vector2(0f, -1 * enemySpeed);
    }

    void StartShooting() 
    {   
        GameObject bullet = Instantiate(enemy_bullet, attack_point.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(enemy_bullet, attack_point2.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().is_EnemyBullet = true;
        bullet2.GetComponent<EnemyBullet>().is_EnemyBullet = true;

        Invoke("StartShooting", Random.Range(3f, 5f));  
   }

     private void OnTriggerEnter2D(Collider2D externalObject)
    {   
        // Hit by player bullets
        var whatHitMe = externalObject.GetComponent<Bullet>();
        // For Power Up bullets
        var whatHitMe2 = externalObject.GetComponent<PowerUpBullet>();

        if(whatHitMe)
        {
            // Counter for HP
            death++;
            // destroy the bullet
            Destroy(whatHitMe.gameObject);

            // Tank has 5 HP
            if(death == 5)
            {
                // Play the sound using the Sound Controller
                SoundController sc = FindObjectOfType<SoundController>();
                if(sc)
                {
                    sc.PlayOneShot(hitSound);
                }
                // publis this event (enemy died) to the system
                PublishEnemyTankKilledEvent();

                // Show the explosion - has playOnAwake set to true, so it runs automatically
                GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
                Destroy(explosion, explosionDuration);

                // destroy the enemy - the current game object
                Destroy(gameObject);
            
            }
        }
        else if(whatHitMe2)
            {
                death++;

                if(death == 5)
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
                PublishEnemyTankKilledEvent();

                // Show the explosion - has playOnAwake set to true, so it runs automatically
                GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
                Destroy(explosion, explosionDuration);

                // destroy the enemy - the current game object
                Destroy(gameObject);
                }
            }
        
    }

    private void PublishEnemyTankKilledEvent()
    {   
        // If some part of the system is listening, then publish
        if(EnemyTankKilledEvent != null)
        {
            EnemyTankKilledEvent(this);
        }
    }
}
