using UnityEngine;
using TMPro;

public class InventoryTextController : MonoBehaviour
{
    public Miner miner;
    private TextMeshProUGUI inventoryText;
    internal Transform minerTransform;

    private void Start()
    {
        inventoryText = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        if (miner != null && inventoryText != null)
        {
            inventoryText.text = miner.inventorySize.ToString();

            // Set the position of the text to follow the miner's position
            transform.position = Camera.main.WorldToScreenPoint(miner.transform.position + Vector3.up * 2f);
        }
    }
}