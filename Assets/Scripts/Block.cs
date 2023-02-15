using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Material toBeMinedMaterial;
    public Material minedMaterial;
    public float mineTime = 2.0f;

    public Player player;
    private bool mined = false;

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
        if (!mined)
        {
            mined = true;
            GetComponent<Renderer>().material = minedMaterial;
            StartCoroutine(MineCoroutine()); // Start the mining coroutine
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Miner"))
        {
            Mine();
            player.AddMoney(1);
        }
    }

    private IEnumerator MineCoroutine()
    {
        yield return new WaitForSeconds(mineTime); // Wait for the mining time
        Destroy(gameObject);
    }
}