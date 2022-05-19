using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{

    [SerializeField]
    DBManager dbManager;
    static ActiveItem activestats;
    static PassiveItem passivestats;
    static public Character character;

    public static void SetPassiveStats(PassiveItem stats)
    {
        passivestats = stats;
    }
    public static void SetActiveStats(ActiveItem stats)
    {
        activestats = stats;
    }
    [Header("Здоровье и броня")]
    public float health;
    public float Maxhealth;
    public float armor;

    [Header("Передвижение")]
    public float speed;

    [Header("Бой")]
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float damage;
    public float atackRange;

    [Header("Бафы")]
    public GameObject dagger;
    public Dagger daggerIcon;
    public GameObject bandage;
    public Bandage bandageIcon;

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private float timeBtwAttack;
    private bool isGameOver = false;
    private Rigidbody2D rb;
    private Animator anim;

    private bool facingRight = true;
    private bool isAct = false;
    [SerializeField]
    public InputActionAsset actions;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    private void Awake()
    {
        actions["Attack"].performed += context => Attack();
        if (character.hp != 0)
        {
            Maxhealth = health = character.hp;
            damage = character.damage;
            armor = character.armor;
            speed = character.speed;
        }
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        moveInput = new Vector2(actions["VerticalMove"].ReadValue<float>(), actions["HorizontalMove"].ReadValue<float>());
        moveVelocity = moveInput.normalized * speed;

        if (moveInput.x == 0 && moveInput.y == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }

        if (facingRight == false && moveInput.x > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput.x < 0)
        {
            Flip();
        }
        if (health <= 0)
        {

            isGameOver = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Игра окончена ");
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);





        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
        if (isAct && actions["Active"].IsPressed() && (daggerIcon.isCooldown == false || bandageIcon.isCooldown == false))
        {
            if (dagger.activeInHierarchy)
            {

                daggerIcon.isCooldown = true;
                ////////////////////////////////////////////////////////////
            }
            if (bandage.activeInHierarchy)
            {
                bandageIcon.isCooldown = true;
                ////////////////////////////////////////////////////////////
            }
            ChangeDamage(activestats.damage);
            ChangeHealth(activestats.hp);
        }
    }
    private void Attack()
    {

        Debug.Log("Attack");
        anim.SetTrigger("Attack");


        timeBtwAttack = startTimeBtwAttack;

    }
    public void OnPassive(Collider2D passive)
    {
        Destroy(passive.gameObject);
        ChangeArmor(passivestats.armor);
        ChangeSpeed(passivestats.speed);
        ChangeDamage(passivestats.damage);
        ChangeHealth(passivestats.hp);
        ChangeMaxHealth(passivestats.hpboost);

    }
    public void OnActive(Collider2D active)
    {
        if (active.name == "Dagger")
        {
            Destroy(active.gameObject);
            ActiveBandage();
            dagger.SetActive(true);
            daggerIcon.gameObject.SetActive(true);

        }
        if (active.name == "Bandage")
        {
            Destroy(active.gameObject);
            ActiveDagger();
            bandage.SetActive(true);
            bandageIcon.gameObject.SetActive(true);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Passive"))
        {
            dbManager.StartGetnSetPassiveItem(other);



        }

        else if (other.CompareTag("Active"))
        {
            dbManager.StartGetnSetActiveItem(other);


        }

    }
    public void ChangeHealth(float healthValue)
    {
        health += healthValue;
    }
    public void ChangeMaxHealth(float maxHealthValue)
    {
        Maxhealth += maxHealthValue;
    }
    public void ChangeDamage(float DamageValue)
    {
        damage += DamageValue;
    }
    public void ChangeArmor(float ArmorValue)
    {
        armor += ArmorValue;
    }
    public void ChangeSpeed(float SpeedValue)
    {
        speed += SpeedValue * speed;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, atackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    private void ActiveDagger()
    {
        isAct = true;
        if (dagger.activeInHierarchy)
        {
            dagger.SetActive(false);
            daggerIcon.gameObject.SetActive(false);
            Debug.Log("1");

        }


    }
    private void ActiveBandage()
    {
        if (bandage.activeInHierarchy)
        {
            bandage.SetActive(false);
            bandageIcon.gameObject.SetActive(false);
            Debug.Log("2");

        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, atackRange);
    }
}