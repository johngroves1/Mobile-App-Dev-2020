using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotate_speed = 2f;
    private GameController gc;
    private PlayerBehaviour pb;

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
    Vector3 temp = transform.position;
    temp.y -= speed * Time.deltaTime;
    transform.position = temp;     
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, rotate_speed * Time.deltaTime), Space.World);
    }
    
    // Instantiates effects, destroys object and calls the power up behaviour in player behaviour
    public void PickupDoubleShot()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        // Adds effect to Player Behaviour
        pb.powerUpDouble();

        Destroy(gameObject);
    }

    
}
