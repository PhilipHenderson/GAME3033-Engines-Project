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
            if (other.CompareTag("Player"))
            {
                player.money -= minerCost;
                Instantiate(minerPrefab, transform.position, transform.rotation);
            }
        }
    }
}
