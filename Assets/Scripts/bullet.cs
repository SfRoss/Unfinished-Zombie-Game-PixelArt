using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public float pierce;
    public float cooldown;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<ZombieAi>().health -= 5f;
            if(pierce > 0f)
            {
                pierce--;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        print("hit");
        
    }
}
