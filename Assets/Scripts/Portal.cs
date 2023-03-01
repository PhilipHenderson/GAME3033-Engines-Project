using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    public Vector3 spawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            // Save the player's data in GameManager
            GameManager.Instance.SetPlayerData(player);

            // Load the new scene
            SceneManager.LoadScene(sceneName);
        }
    }

    // In the new scene, spawn the player at the portal endpoint
    private void Start()
    {
        if (GameManager.Instance.currentPD != null)
        {
            MyPlayersData playerData = GameManager.Instance.currentPD;

            GameObject newPlayer = Instantiate(playerData.prefab, playerData.playerPosition, playerData.rotation);
            Player player = newPlayer.GetComponent<Player>();
            player.currentHP = playerData.currentHP;
            player.money = playerData.playerMoney;

            GameManager.Instance.currentPD = null;
        }
    }
}
