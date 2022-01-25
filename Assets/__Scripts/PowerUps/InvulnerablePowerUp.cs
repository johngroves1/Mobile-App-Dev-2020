using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulnerablePowerUp : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private float speed = 2f;

    private GameController gc;
    private PlayerBehaviour pb;

     void Start()
    {
        gc = FindObjectOfType<GameController>();
        pb = FindObjectOfType<PlayerBehaviour>();
    }

    void Update()
    {
        Move();
    }

    void Move()
   {    
    Vector3 temp = transform.position;
    temp.y -= speed * Time.deltaTime;
    transform.position = temp;
     
   }
        
    // Instantiates effects, destroys object and calls the power up behaviour in player behaviour
    public void PickUpInvul()
    {
        Debug.Log("Invinsible");
        Instantiate(pickupEffect, transform.position, transform.rotation);
        pb.Invulnerable();
        Destroy(gameObject);
    }
}
