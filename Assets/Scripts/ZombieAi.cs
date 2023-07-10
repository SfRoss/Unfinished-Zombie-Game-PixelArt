using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAi : MonoBehaviour
{
    private bool following = false;
    private Vector3 playerPos = Vector3.zero;
    public float health = 10.0f;

    [SerializeField] float speed = 3.0f;


    [SerializeField] GameObject player;
    [SerializeField] GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<TopDownController>().gameObject;
        following = true;
        speed = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
         if (health <= 0)
        {
            Die();
        }
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        if(following)
        {
            playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            //Here, the zombie's will follow the player.
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            if(playerPos.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void Die()
    {
        int random = Random.Range(1, 8);
        if (random == 1)
        {
            Instantiate(drop, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

}
