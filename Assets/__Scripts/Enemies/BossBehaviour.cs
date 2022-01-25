using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{

    public int ScoreValue
    {
        set { scoreValue = value; }
        get { return scoreValue; }
    }
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private int scoreValue = 5000;
    [SerializeField] private Transform attack_point;
    [SerializeField] private Transform attack_point2;
    [SerializeField] private Transform attack_point3;
    [SerializeField] private Transform attack_point4;
    [SerializeField] private GameObject endGameMenuUI;

    private bool canShoot;
    private int counter = 0;
    private bool pauseShoot = false;
    private float shootSpeed = 1f;
    private float shootSpeed2 = 1.5f;
    private AudioSource audioSource;
    private float explosionDuration = 1.0f;
    private Rigidbody2D rb;
    
    Renderer rend;
    Color c;

    public GameObject enemy_bullet;
    public Slider healthBar;
    public int health;

    public delegate void BossKilled(BossBehaviour enemy); // Event type
    public static BossKilled BossKilledEvent; // Event handler

private void Start()
{
    rb = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();
    rend = GetComponent<Renderer>();
        
        c = rend.material.color;

    Invoke("StartShooting", 2f);
    Invoke("StartShooting2", 2f);
}
   private void Update()
   {
        Move();
        healthBar.value = health;

        // If health is lowered to 40 Boss gains temporary invulnerability
        if(health == 40)
        {
            StartCoroutine("TempInvulnerable");    
        }
        // If health is lowered to 39 Boss shoots 4x instead of 2x
        else if(health == 39)
        {
            canShoot = true;
        }
        // Bullet speed increased
        else if(health == 35)
        {
            shootSpeed = .5f;
        }
        else if(health == 20)
        {
             StartCoroutine("TempInvulnerable");
        }
        // Bullet speed increased
        else if(health == 15)
        {
            shootSpeed2 = .5f;
        }
   }

    // Boss moves from left to right of the screen
   void Move()
   {
      Debug.Log(rb.transform.position);
      Vector3 temp = transform.position;
      
      if(counter == 0)
      {
        temp.x -= 0.5f * Time.deltaTime;
        transform.position = temp;

        if(temp.x < -1.2)
        {
            counter++;
        }
      }
      else if(counter == 1)
      {
        temp.x += 0.5f * Time.deltaTime;
        transform.position = temp;

          if(temp.x > 1.2)
        {
            counter--;
        }
      }
     
   }

    // Temporary invulnerability for 3seconds if boss hp is lowered
    IEnumerator TempInvulnerable()
    {
        Physics2D.IgnoreLayerCollision (9, 8, true);
        c.a = 0.2f;
        rend.material.color = c;
        yield return new WaitForSeconds (3f);
        Physics2D.IgnoreLayerCollision (9, 8, false);
        c.a = 1f;
        rend.material.color = c;
    }
    
    // Boss initially shoots twice
      void StartShooting2() 
      {


        GameObject bullet3 = Instantiate(enemy_bullet, attack_point3.position, Quaternion.identity);
        GameObject bullet4 = Instantiate(enemy_bullet, attack_point4.position, Quaternion.identity);
        bullet3.GetComponent<EnemyBullet>().is_EnemyBullet = true;
        bullet4.GetComponent<EnemyBullet>().is_EnemyBullet = true;

      if(pauseShoot == false)
      {
        Invoke("StartShooting2", shootSpeed2);
      }  
   }

    // When HP is lowered Boss shoots 4x
   void StartShooting()
   {
        if(canShoot == true)
       {
      
       GameObject bullet = Instantiate(enemy_bullet, attack_point.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(enemy_bullet, attack_point2.position, Quaternion.identity);
       
       bullet.GetComponent<EnemyBullet>().is_EnemyBullet = true;
       bullet2.GetComponent<EnemyBullet>().is_EnemyBullet = true;
        
       }
       Invoke("StartShooting", shootSpeed);
       
   }

private void OnTriggerEnter2D(Collider2D externalObject)
    {
        var whatHitMe = externalObject.GetComponent<Bullet>();
        if(whatHitMe)
        {

            health--;
            // destroy the bullet
            Destroy(whatHitMe.gameObject);

            if(health == 0)
            {
            // Play the sound using the Sound Controller
            SoundController sc = FindObjectOfType<SoundController>();
            if(sc)
            {
               
               sc.PlayOneShot(hitSound);
            }
             // publis this event (enemy died) to the system
            PublishBossKilledEvent();

            // Show the explosion - has playOnAwake set to true, so it runs automatically
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);

            // destroy the enemy - the current game object
            Destroy(gameObject);

            // Call EndGameMenu
            ActivateMenu();
            }

        }
    }
    
    // EndGame Menu shows
    void ActivateMenu()
    {
        endGameMenuUI.SetActive(true);
    }

    private void PublishBossKilledEvent(){

         // If some part of the system is listening, then publish
        if(BossKilledEvent != null)
        {
            BossKilledEvent(this);
        }
    }

}
