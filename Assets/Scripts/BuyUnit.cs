using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUnit : MonoBehaviour
{
    public GameObject minerPrefab;
    public Player player;
    public float minerCost;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered.");
        if (player.Money >= minerCost)
        {
            player.money -= minerCost;
            if (other.CompareTag("Player"))
            {
                Instantiate(minerPrefab, transform.position, transform.rotation);
            }
        }
    }
}
