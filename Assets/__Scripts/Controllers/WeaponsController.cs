using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float firingRate = 0.25f;
    [SerializeField] private AudioClip shootSound;
    private AudioSource audioSource;
    private GameObject bulletParent;
    private Coroutine firingCoroutine;
    
    void Start()
    {
        bulletParent = GameObject.Find("BulletParent");
        if(!bulletParent){
            bulletParent = new GameObject("BulletParent");
        }
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        Fire();
    }

    // Fires a bullet on pressing down SpaceBar
    private void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            firingCoroutine = StartCoroutine(FireCoroutine());
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireCoroutine()
    {
        while(true)
        {
            FireBullet();
            yield return new WaitForSeconds(firingRate);
        }
        
    }

    private void FireBullet()
    {
        Bullet b = Instantiate(bulletPrefab, bulletParent.transform);
        b.transform.position = gameObject.transform.position;
         // play the sound
        if (audioSource) audioSource.PlayOneShot(shootSound);
    }
}

