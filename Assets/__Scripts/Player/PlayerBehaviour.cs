using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip hitSound;
    // == Attack points for powerUps ==
    [SerializeField] private Transform attack_point;
    [SerializeField] private Transform attack_point2;
    [SerializeField] private Transform attack_point3;
    [SerializeField] private Transform attack_point4;

    // == Border Constraints == 
    private float xMin = -4f;
    private float xMax = 4f;
    private float yMin = -7.1f;
    private float yMax = 7.1f;

    // == References ==
    private float explosionDuration = 1.0f;
    private SpriteRenderer sr;
    private WeaponsController weapons;
    private PlayerBehaviour behaviour;
    private PolygonCollider2D pc2d;
    private GameController gc;
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    // == Calling powerUps ==
    private PowerUp pu;
    private FourTimesShot fs;
    private InvulnerablePowerUp ip;

    private bool powerUpShoot = false;
    private bool powerUpShootFour = false;
    
    public GameObject powerUpBullet;

    // == Invulnerable variables
    private float startTime;
    public float timer;
    Renderer rend;
    Color c;

    void Start() //Runs once at startup
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        weapons = GetComponent<WeaponsController>();
        behaviour = GetComponent<PlayerBehaviour>();
        pc2d = GetComponent<PolygonCollider2D>();
        gc = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
        pu =FindObjectOfType<PowerUp>();
        fs =FindObjectOfType<FourTimesShot>();
        ip =FindObjectOfType<InvulnerablePowerUp>();
        rend = GetComponent<Renderer>();
        
        c = rend.material.color;

        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        PlayerMove(); 
    }

    // Player Movement
    private void PlayerMove()
    {
        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");

        //give the player a velocity - done using a vector
        rb.velocity = new Vector2(hMovement * speed, vMovement * speed);

        // constrain the player to the screen
        float xValue = Mathf.Clamp(rb.position.x, xMin, xMax);
        float yValue = Mathf.Clamp(rb.position.y, yMin, yMax);

        rb.position = new Vector2(xValue, yValue);
    }

    // Shoot Double PowerUp
    public void powerUpDouble()
    {
        powerUpShoot = true;
        Invoke("powerupShot", 1f);
        startTime = Time.time;
    }

    void powerupShot()
    {
        // Timer for how long PowerUp is active
        float t = Time.time - startTime;
        float seconds = (t%60);
        timer = seconds;
        
        if(timer < 15)
        {
            GameObject bullet = Instantiate(powerUpBullet, attack_point.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(powerUpBullet, attack_point2.position, Quaternion.identity);
            bullet.GetComponent<PowerUpBullet>().is_PowerUpBullet = true;
            bullet2.GetComponent<PowerUpBullet>().is_PowerUpBullet = true;

        if(powerUpShoot)
            {
                Invoke("powerupShot", Random.Range(.5f, 1f));
            }
        }
    }

    // Shoot FourX PowerUp
    public void powerUpFour()
    {
        // Timer for how long PowerUp is active
        powerUpShootFour = true;
        Invoke("powerupShotFour", 1f);
        startTime = Time.time;
    }

    void powerupShotFour(){

        float t = Time.time - startTime;
        float seconds = (t%60);
        timer = seconds;

        if(timer < 10)
        {
        GameObject bullet = Instantiate(powerUpBullet, attack_point.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(powerUpBullet, attack_point2.position, Quaternion.identity);
        GameObject bullet3 = Instantiate(powerUpBullet, attack_point3.position, Quaternion.identity);
        GameObject bullet4 = Instantiate(powerUpBullet, attack_point4.position, Quaternion.identity);
        bullet.GetComponent<PowerUpBullet>().is_PowerUpBullet = true;
        bullet2.GetComponent<PowerUpBullet>().is_PowerUpBullet = true;
        bullet3.GetComponent<PowerUpBullet>().is_PowerUpBullet = true;
        bullet4.GetComponent<PowerUpBullet>().is_PowerUpBullet = true;

       if(powerUpShootFour){
        Invoke("powerupShotFour", Random.Range(.5f, 1f));
       }
        }
    }

    public void Invulnerable()
    {
        StartCoroutine("GetInvulnerable");
    }

    IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision (8, 9, true);
        c.a = 0.2f;
        rend.material.color = c;
        yield return new WaitForSeconds (10f);
        Physics2D.IgnoreLayerCollision (8, 9, false);
        c.a = 1f;
        rend.material.color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        // Tags so player wont die on colliding with powerUps
        if (collision.CompareTag("PowerUp2x"))
        {
            pu.PickupDoubleShot();
            
        }
        else if(collision.CompareTag("PowerUp4x"))
        {
            fs.Pickup4Shot();

        }
        else if(collision.CompareTag("PlayerBullet"))
        {
        }
        else if(collision.CompareTag("PowerUpInvul"))
        {
            ip.PickUpInvul();
        }
        else
        {
        // Player looses one life
        LoseLife();

         SoundController sc = FindObjectOfType<SoundController>();
            if(sc)
            {
                sc.PlayOneShot(hitSound);
            }

        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        }
    }

    private void LoseLife()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        setComponentStatus(false);
        // Instantiate the explosion at the current position
        gc.ProcessPlayerDeath();
        yield return new WaitForSeconds(1.5f); // Length of explosion
        if(gc.LivesLeft > 0)
        {
            Respawn();
        }
    }

    // Player Respawns and gains temporary invulnability
    private void Respawn()
    {
        transform.position = startPosition;
        setComponentStatus(true);
        StartCoroutine("RespawnInvulnerable");
    }

    IEnumerator RespawnInvulnerable()
    {
        Physics2D.IgnoreLayerCollision (8, 9, true);
        c.a = 0.2f;
        rend.material.color = c;
        yield return new WaitForSeconds (3f);
        Physics2D.IgnoreLayerCollision (8, 9, false);
        c.a = 1f;
        rend.material.color = c;
    }

    private void setComponentStatus(bool status)
    {
        sr.enabled = status;
        weapons.enabled = status;
        behaviour.enabled = status;
        pc2d.enabled = status;
    }
}
