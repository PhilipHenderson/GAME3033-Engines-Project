using UnityEngine;
using TMPro;

public enum WorkerType
{
    M, //(Minor)
    WC,//(WoodCutter)
    F  //(Farmer)
}

public class Miner : MonoBehaviour
{
    [Header("Miner Properties")]
    public float speed = 3.0f;
    public float miningTime = 2.0f;
    public float miningRange;

    [Header("Material Properties")]
    public Material highlightedMaterial;
    public Material gettingMinedMaterial;

    [Header("Inventory Properties")]
    public InventoryTextController inventoryTextController;
    public TextMeshProUGUI inventoryText;
    public float inventorySize = 0;
    public int maxInventorySize = 3;

    [Header("Selling Properties")]
    public Transform sellGoodsLocation;
    private bool sellingGoods;

    [Header("Health Properties")]
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI healthText;

    [Header("Worker Type Properties")]
    public WorkerType workerType;
    public TextMeshProUGUI workerTypeText;

    public int maxAngleOffset;
    public LayerMask blockLayer;
    public Player player;

    private Block currentBlock;
    private bool mining;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        inventoryText.SetText($"{inventorySize}\n{maxInventorySize}");
        currentHealth = maxHealth;
        UpdateHealthUI();
        SetWorkerType(WorkerType.M);
    }

    private void Update()
    {
        if (!mining)
        {
            if (sellingGoods)
            {
                // Move the miner to the sell goods location
                Vector2 direction = sellGoodsLocation.position - transform.position;
                if (direction.magnitude > 0.1f)
                {
                    direction.Normalize();
                    transform.position += (Vector3)direction * speed * Time.deltaTime;
                }
                else
                {
                    // Sell the goods and return to mining
                    player.AddMoney(inventorySize);
                    inventorySize = 0;
                    inventoryText.text = inventorySize.ToString();
                    sellingGoods = false;
                }
            }
            else
            {
                if (currentBlock != null && (currentBlock.IsMined() || currentBlock.beingMined))
                {
                    // Current block has been fully mined or is already being mined, find next closest block
                    currentBlock.GetComponent<Renderer>().material = null;
                    currentBlock = FindClosestBlock();
                }
                else if (currentBlock == null)
                {
                    // No current block, find closest block
                    currentBlock = FindClosestBlock();
                }
                else
                {
                    Vector2 direction = currentBlock.transform.position - transform.position;
                    if (direction.magnitude > miningRange)
                    {
                        // Move towards the current block
                        direction.Normalize();

                        // Add a random angle offset to the direction
                        float angle = Random.Range(-maxAngleOffset, maxAngleOffset);
                        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        Vector3 offsetDirection = rotation * direction;

                        transform.position += offsetDirection * speed * Time.deltaTime;
                    }
                    else
                    {
                        // Start mining the current block
                        mining = true;
                        currentBlock.GetComponent<Renderer>().material = gettingMinedMaterial;
                        currentBlock.Mine();

                    }
                }
            }
        }
        else
        {
            miningTime -= Time.deltaTime;
            if (miningTime <= 0)
            {
                MineBlock();
                miningTime = 2.0f;
            }
        }
    }

    private void UpdateHealthUI()
    {
        healthText.SetText($"{currentHealth}\n{maxHealth}");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateWorkerTypeUI()
    {
        workerTypeText.SetText(workerType.ToString());
    }

    public void SetWorkerType(WorkerType newWorkerType)
    {
        workerType = newWorkerType;
        UpdateWorkerTypeUI();
    }

    private Block FindClosestBlock()
    {
        Block[] blocks = FindObjectsOfType<Block>();
        Block closestBlock = null;
        float closestDistance = Mathf.Infinity;
        foreach (Block block in blocks)
        {
            if (!block.IsMined() && (block.miner == null || block.miner == this))
            {
                float distance = Vector2.Distance(transform.position, block.transform.position);
                if (distance < closestDistance)
                {
                    closestBlock = block;
                    closestDistance = distance;
                }
            }
        }
        if (closestBlock != null)
        {
            closestBlock.miner = this;
            closestBlock.GetComponent<Renderer>().material = highlightedMaterial;
        }
        else
        {
            Debug.Log("No unmined block found.");
        }
        return closestBlock;
    }

    private void MineBlock()
    {
        mining = false;
        inventorySize++;
        inventoryText.SetText($"{inventorySize}\n{maxInventorySize}");
        currentBlock.Mine();
        currentBlock.miner = null;
        currentBlock = null;

        if (inventorySize >= maxInventorySize)
        {
            sellingGoods = true;
        }
    }
}