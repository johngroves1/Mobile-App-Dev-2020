using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   public float speed = 5f;
   public float deactivate_timer = 3f;

   [HideInInspector]
   public bool is_EnemyBullet = false;
   public void Start()
    {
        if(is_EnemyBullet)
        {
            speed *=-1f;
        }
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
