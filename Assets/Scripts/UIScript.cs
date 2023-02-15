using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public Player player;
    public TMP_Text hpText;
    public TMP_Text moneyText;

    private void Update()
    {
        // Update HP and money text
        hpText.text = "HP: " + player.CurrentHP;
        moneyText.text = "Money: " + player.Money;
    }
}