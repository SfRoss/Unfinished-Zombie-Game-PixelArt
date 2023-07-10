using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownController : MonoBehaviour
{
    [Range(0.5f, 10f)]
    [SerializeField] private float speed;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform meleePoint;
    [SerializeField] private Transform SpawnSlicePoint;
    [SerializeField] private GameObject Slice;
    [SerializeField] private float startSwingTime;
    public float health;
    [SerializeField] private float damageFromZombie;
    [SerializeField] private Slider healthSlider;
    private float swingTime;

    [SerializeField] private int resCount = 0;

    private Vector3 move;
    private Rigidbody2D rb;
    private Animator animator;
    private bool sprinting;
    private float speeddoubled;
    private bool wantstoshoot;
    private float originalCooldown;
    private float cooldown;
    private bool autoshoot = false;
    private bool tryingToSwing;
    private bool tryingToSwingLeft;
    private float startHealth;

    private void Awake()
    {
        Screen.SetResolution(256, 144, false);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speeddoubled = speed * 2.5f;
        originalCooldown = bullet.GetComponent<bullet>().cooldown;
        cooldown = bullet.GetComponent<bullet>().cooldown;
        swingTime = startSwingTime;
        startHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        health -= Time.deltaTime;
        healthSlider.value = health;
        if(health > startHealth)
        {
            health = startHealth;
        }
        //input for movement
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move.z = 0;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dif = mousePos - transform.position;
        float angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        if (mousePos.x > firePoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            firePoint.eulerAngles = new Vector3(0, 0, angle);
            if(angle >= 0f)
            {
                tryingToSwing = true;
            }
        }
        else if (mousePos.x < firePoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            firePoint.eulerAngles = new Vector3(180, 0, angle*-1);
            if (angle >= 120f)
            {
                tryingToSwingLeft = true;
            }
        }
        if(tryingToSwing)
        {
            swingTime -= Time.deltaTime;
            if (angle <= -10f && swingTime > 0f)
            {
                GameObject sliceObject = Instantiate(Slice, SpawnSlicePoint.position, Quaternion.Euler(0, 0, 0));
                StartCoroutine(Melee(sliceObject));
                swingTime = startSwingTime;
                FindObjectOfType<AudioManager>().Play("Swing");
                tryingToSwing = false;
            }
            else if(swingTime < 0f)
            {
                swingTime = startSwingTime;
                tryingToSwing = false;
            }
        }
        if (tryingToSwingLeft)
        {
            swingTime -= Time.deltaTime;
            if (angle <= -140f && swingTime > 0f)
            {
                GameObject sliceObject = Instantiate(Slice, SpawnSlicePoint.position, Quaternion.Euler(0, 180, 0));
                StartCoroutine(Melee(sliceObject));
                swingTime = startSwingTime;
                FindObjectOfType<AudioManager>().Play("Swing");
                tryingToSwing = false;
            }
            else if (swingTime < 0f)
            {
                swingTime = startSwingTime;
                tryingToSwingLeft = false;
            }
        }

        if(move != new Vector3(0, 0, 0))
        {
            animator.SetBool("Running", true);

        }
        else
        {
            animator.SetBool("Running", false);
            FindObjectOfType<AudioManager>().Play("Walk");
        }



        if (Input.GetMouseButton(0))
        {
            wantstoshoot = true;
        }
        else if(!autoshoot)
        {
            wantstoshoot = false;
        }
        if(wantstoshoot)
        {
            if(cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                cooldown = originalCooldown;
                Shoot(bullet);
                wantstoshoot = false;
            }
            
        }

       
        if(Input.GetKeyDown(KeyCode.E))
        {
            autoshoot = !autoshoot;
            print("Shooting Automatic");
        }
        if(autoshoot)
        {
            wantstoshoot = true;
        }
        rb.velocity = new Vector3(0, 0, 0);
        print(health);
        
    }
    private void FixedUpdate()
    {
        //Moves the player
        if (sprinting)
        {
            transform.position += move * speeddoubled * Time.deltaTime;
        }
        else if (!sprinting)
        {
            transform.position += move * speed * Time.deltaTime;
        }
        
    }

    public void Shoot(GameObject b)
    {
        FindObjectOfType<AudioManager>().Play("Shoot");
        GameObject instantiated = Instantiate(b, firePoint.position, firePoint.rotation);
        instantiated.GetComponent<Rigidbody2D>().AddForce(firePoint.up * instantiated.GetComponent<bullet>().speed, ForceMode2D.Impulse);
    }

    private IEnumerator Melee(GameObject slice)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(meleePoint.position, 0.7f);

        if (hitCollider.gameObject != null)
        {
            if (hitCollider.tag == "Zombie")
            {
                hitCollider.gameObject.GetComponent<ZombieAi>().health -= 10f;
            }else if (hitCollider.tag == "Car")
            {
                FindObjectOfType<AudioManager>().Play("HitCar");
                print("hit");
            }
        }
        yield return new WaitForSeconds(0.14f);
        Destroy(slice);

            
    }

    public void store()
    {
        resCount++;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            health -= damageFromZombie;
            FindObjectOfType<AudioManager>().Play("Hurt");
        }
    }
}
