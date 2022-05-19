using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public Difficulty mult;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public GameObject death;
    public float speed = 10;
    public float agrDist = 5;
    public float health = 50;
    public float damage = 10;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;

    private Player player;
    private Animator anim;
    private Rigidbody2D physic;

    void Start()
    {
        physic = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
    }

    private void Awake()
    {
        if (mult.mobshpmult != 0 && mult.mobsdamagemult != 0)
        {
            damage *= mult.mobsdamagemult;
            health *= mult.mobshpmult;
        }
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer < agrDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            physic.velocity = new Vector2(0, 0);
        }
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            Instantiate(death, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }

    public void TakeDamage(float Damage)
    {
        stopTime = startStopTime;

        health -= Damage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                anim.SetTrigger("enemyAttack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    public void OnEnemyAttack()
    {

        player.health -= damage * 0.01f * player.armor;
        timeBtwAttack = startTimeBtwAttack;
    }
}
