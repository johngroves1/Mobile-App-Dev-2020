using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBullet : MonoBehaviour
{
   public float speed = 5f;
   public float deactivate_timer = 3f;
   
   [HideInInspector]
   public bool is_PowerUpBullet = false;
   private Rigidbody2D rb;
   
   public void Start()
    {
        if(is_PowerUpBullet){
             speed *=1f;
        }
        
        rb = GetComponent<Rigidbody2D>();
        Invoke("DeactivateGameObject", deactivate_timer);

        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
    }

    void DeactivateGameObject(){
        gameObject.SetActive(false);
    }
}
