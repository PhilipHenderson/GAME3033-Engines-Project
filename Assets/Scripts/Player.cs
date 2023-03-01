using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public float startingMoney = 0.0f;
    public float speed;
    public float money = 0.0f;

    public static MyPlayersData currentPlayerData;

    private Rigidbody2D rb;

    private void Start()
    {
        currentHP = maxHP;
        money = startingMoney;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Make the player object persistent between scene loads
        DontDestroyOnLoad(gameObject);
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
            // TODO: create player action
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

    public void SetPlayerData(MyPlayersData data, string currentSceneName)
    {
        Vector3 position = data.playerPosition;
        Quaternion rotation = data.rotation;
        GameObject prefab = data.prefab;
        float money = data.playerMoney;
        int hp = data.currentHP;

        // Create a new player object with the saved data
        GameObject playerObj = Instantiate(prefab, position, rotation);
        Player player = playerObj.GetComponent<Player>();
        player.currentHP = hp;
        player.money = money;

        // Set the current player data to the newly created player
        currentPlayerData = new MyPlayersData(money, hp, position, rotation, prefab, currentSceneName);
    }

    public MyPlayersData GetData()
    {
        return new MyPlayersData(money, currentHP, transform.position, transform.rotation, gameObject, SceneManager.GetActiveScene().name);
    }

    public void SetData(MyPlayersData data)
    {
        transform.position = data.playerPosition;
        transform.rotation = data.rotation;
        money = data.playerMoney;
        currentHP = data.currentHP;
    }
}
