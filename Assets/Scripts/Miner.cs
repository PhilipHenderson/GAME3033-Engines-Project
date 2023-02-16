using UnityEngine;
using TMPro;

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
    public int inventorySize = 0;
    public int maxInventorySize = 3;

    [Header("Selling Properties")]
    public Transform sellGoodsLocation;
    private bool sellingGoods;

    public LayerMask blockLayer;
    public Player player;

    private Block currentBlock;
    private bool mining;

    private void Start()
    {
        inventoryText.SetText(inventorySize.ToString());
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
                    Debug.Log("Going to sell goods");
                    direction.Normalize();
                    transform.position += (Vector3)direction * speed * Time.deltaTime;
                }
                else
                {
                    // Sell the goods and return to mining
                    Debug.Log("selling goods, Please Wait");
                    player.AddMoney(inventorySize);
                    inventorySize = 0;
                    inventoryText.text = inventorySize.ToString();
                    Debug.Log("Going to Mine");
                    sellingGoods = false;
                }
            }
            else
            {
                if (currentBlock != null && (currentBlock.IsMined() || currentBlock.beingMined))
                {
                    // Current block has been fully mined or is already being mined, find next closest block
                    Debug.Log("Finding closest block");
                    currentBlock.GetComponent<Renderer>().material = null;
                    currentBlock = FindClosestBlock();
                }
                else if (currentBlock == null)
                {
                    // No current block, find closest block
                    Debug.Log("Finding closest block");
                    currentBlock = FindClosestBlock();
                }
                else
                {
                    Vector2 direction = currentBlock.transform.position - transform.position;
                    if (direction.magnitude > miningRange)
                    {
                        // Move towards the current block
                        Debug.Log("Moving toward current block");
                        direction.Normalize();
                        transform.position += (Vector3)direction * speed * Time.deltaTime;
                    }
                    else
                    {
                        // Start mining the current block
                        Debug.Log("Mining current block");
                        mining = true;
                        currentBlock.GetComponent<Renderer>().material = gettingMinedMaterial;
                        currentBlock.Mine();
                        Invoke("MineBlock", miningTime);
                    }
                }
            }
        }
    }

    private Block FindClosestBlock()
    {
        Block[] blocks = FindObjectsOfType<Block>();
        Block closestBlock = null;
        float closestDistance = Mathf.Infinity;
        foreach (Block block in blocks)
        {
            if (!block.IsMined())
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
            closestBlock.GetComponent<Renderer>().material = highlightedMaterial;
        }
        return closestBlock;
    }

    private void MineBlock()
    {
        mining = false;
        inventorySize++;
        inventoryText.SetText(inventorySize.ToString());
        currentBlock.Mine();
        currentBlock = null;

        if (inventorySize >= maxInventorySize)
        {
            sellingGoods = true;
        }
    }
}