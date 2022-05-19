using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class move : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public bool facingRight = true;
    public float runSpeed = 20.0f;
    public int maxHealth;
    public int health;
    public Image bar;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        maxHealth = 100;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        bar.fillAmount = maxHealth;
        
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        if (facingRight == false && horizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && horizontal < 0)
        {
            Flip();
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Death()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
