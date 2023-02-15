using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int maxHP = 100;
    public int startingMoney = 0;

    private int currentHP;
    public float money;
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