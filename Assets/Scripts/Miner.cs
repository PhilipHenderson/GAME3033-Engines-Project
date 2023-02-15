using UnityEngine;

public class Miner : MonoBehaviour
{
    public float speed = 3.0f;
    public float miningTime = 2.0f;
    public LayerMask blockLayer;
    public Material highlightedMaterial;
    public Material minedMaterial;

    private Block currentBlock;
    private bool mining;

    private void Update()
    {
        if (!mining)
        {
            if (currentBlock == null)
            {
                currentBlock = FindClosestBlock();
            }
            else
            {
                Vector2 direction = currentBlock.transform.position - transform.position;
                if (direction.magnitude > 0.1f)
                {
                    direction.Normalize();
                    transform.position += (Vector3)direction * speed * Time.deltaTime;
                }
                else
                {
                    mining = true;
                    currentBlock.GetComponent<Renderer>().material = minedMaterial;
                    Invoke("MineBlock", miningTime);
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
}