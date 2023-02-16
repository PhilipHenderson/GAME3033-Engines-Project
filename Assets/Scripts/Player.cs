using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (!instance)
            {
                instance = new GameObject().AddComponent<Player>();
                instance.name = instance.GetType().ToString();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public float speed;
    public int maxHP = 100;
    public int currentHP;
    public float startingMoney = 0.0f;
    public float money = 0.0f;

    private Rigidbody2D rb;

    private void Start()
    {
        currentHP = maxHP;
        money = startingMoney;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // get the horizontal and vertical input axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // set the velocity of the rigidbody based on the input axes and the speed
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement * speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public int CurrentHP
    {
        get { return currentHP; }
    }

    public float Money
    {
        get { return money; }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death
    }

    public void AddMoney(float amount)
    {
        money += amount;
    }
}