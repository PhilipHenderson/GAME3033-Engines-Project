using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MyPlayersData currentPD;
    public GameObject playerPrefab;
    private GameObject playerObj;

    private void Awake()
    {
        // Check if there's already an instance of GameManager
        if (Instance == null)
        {
            // If not, set the instance to this
            Instance = this;

            // Make sure the GameManager persists between scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If there is, destroy this instance of GameManager
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load the player prefab from the Resources folder
        playerPrefab = Resources.Load<GameObject>("PlayerPrefab");
    }

    public void SetPlayerData(Player player)
    {
        Vector3 position = player.transform.position;
        Quaternion rotation = player.transform.rotation;
        GameObject prefab = player.gameObject;

        MyPlayersData data = new MyPlayersData(player.Money, player.CurrentHP, position, rotation, prefab, SceneManager.GetActiveScene().name);
        currentPD = data;
        Debug.Log("Saved player data: " + currentPD.prefab.name);
    }


    public void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            string json = PlayerPrefs.GetString("PlayerData");
            MyPlayersData currentPlayerData = JsonUtility.FromJson<MyPlayersData>(json);

            if (playerObj == null)
            {
                playerObj = Instantiate(playerPrefab);
            }

            Player player = playerObj.GetComponent<Player>();
            player.SetData(currentPlayerData);

            if (SceneManager.GetActiveScene().name != currentPlayerData.sceneName)
            {
                SceneManager.LoadScene(currentPlayerData.sceneName);
            }
            else
            {
                playerObj.transform.position = currentPlayerData.playerPosition;
            }

            PlayerPrefs.DeleteKey("PlayerData");
        }
    }
}
