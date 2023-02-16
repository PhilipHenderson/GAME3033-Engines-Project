using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Material toBeMinedMaterial;
    public Material minedMaterial;

    public Player player;
    public Miner miner;

    public float mineTime = 2.0f;

    private bool mined = false;
    public bool beingMined = false;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public bool IsMined()
    {
        return mined;
    }

    public void Mine()
    {
        if (beingMined)
        {
            return;
        }
        beingMined = true;
        GetComponent<Renderer>().material = minedMaterial;
        StartCoroutine(MineCoroutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Miner"))
        {
            // Get the distance between the miner and the block
            float distance = Vector2.Distance(transform.position, collision.transform.position);
    
            // Check if the miner is within mining distance
            if (distance <= miner.miningRange)
            {
                // Start the mining coroutine
                Mine();
            }
        }
    }

    private IEnumerator MineCoroutine()
    {
        yield return new WaitForSeconds(mineTime); // Wait for the mining time
        mined = true;
        beingMined = false;
        Destroy(gameObject);
    }
}