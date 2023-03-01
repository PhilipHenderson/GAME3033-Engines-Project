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

            // Save the player's money and health
            PlayerPrefs.SetFloat("Money", player.Money);
            PlayerPrefs.SetInt("CurrentHP", player.CurrentHP);

            // Load the new scene
            SceneManager.LoadScene(sceneName);
        }
    }

    // In the new scene, spawn the player at the portal endpoint
    private void Start()
    {
        if (PlayerPrefs.HasKey("Money") && PlayerPrefs.HasKey("CurrentHP"))
        {
            float money = PlayerPrefs.GetFloat("Money");
            int currentHP = PlayerPrefs.GetInt("CurrentHP");

            Player player = Player.Instance;
            player.AddMoney(money);
            player.currentHP = currentHP;

            GameObject spawnPoint = GameObject.Find("PortalEndpoint");
            if (spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
            else
            {
                player.transform.position = spawnPosition;
            }

            PlayerPrefs.DeleteKey("Money");
            PlayerPrefs.DeleteKey("CurrentHP");
        }
        // Print debug information
        Debug.Log("Portal script attached to: " + gameObject.name);
        Debug.Log("Scene name: " + sceneName);
    }
}
