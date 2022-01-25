using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourTimesShot : MonoBehaviour
{
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotate_speed = 2f;
    private GameController gc;
    private PlayerBehaviour pb;
    private bool canMove = true;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        pb = FindObjectOfType<PlayerBehaviour>();

        if(Random.Range(0,2) > 0)
        {   
            rotate_speed = Random.Range(rotate_speed, rotate_speed + 10f);
            rotate_speed *= -1f;
        } 
        else 
        {
            rotate_speed = Random.Range(rotate_speed, rotate_speed + 10f);
        }
    }

    void Update()
    {
         Move();
         Rotate();
    }

    void Move()
   {
       if(canMove) {
           Vector3 temp = transform.position;
           temp.y -= speed * Time.deltaTime;
           transform.position = temp;
       }
   }

   void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, rotate_speed * Time.deltaTime), Space.World);
    }
     void onTriggerEnter(Collider2D other)
    {
       
    }
    
    // Instantiates effects, destroys object and calls the power up behaviour in player behaviour
    public void Pickup4Shot()
    {
        //Debug.Log("4x");
        Instantiate(pickupEffect, transform.position, transform.rotation);
        pb.powerUpFour();
        Destroy(gameObject);
    }
}
