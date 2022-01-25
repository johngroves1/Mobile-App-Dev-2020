using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyS : MonoBehaviour
{
    public int ScoreValue
    {
        set { scoreValue = value; }
        get { return scoreValue; }
    }
    [SerializeField] private Transform attack_point;
    [SerializeField] private int scoreValue = 100;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private float speed = 5f;

    private float explosionDuration = 1.0f;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private bool canMove = true;

    public GameObject enemy_bullet;
    public delegate void EnemySKilled(EnemyS enemy); // Event type
    public static EnemySKilled EnemySKilledEvent; // Event handler

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Invoke("StartShooting", Random.Range(1f, 3f));         
    }
   void Update()
   {
       Move();
   }

   void Move()
   {
       if(canMove) 
       {
           Vector3 temp = transform.position;
           temp.y -= speed * Time.deltaTime;
           transform.position = temp;
       }
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
            PublishEnemySKilledEvent();

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
            PublishEnemySKilledEvent();

            // Show the explosion - has playOnAwake set to true, so it runs automatically
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);

            // destroy the enemy - the current game object
            Destroy(gameObject);
        }
   }

   private void PublishEnemySKilledEvent()
    {   
        // If some part of the system is listening, then publish
        if(EnemySKilledEvent != null)
        {
            EnemySKilledEvent(this);
        }  
    }
}
