using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    public Vector3 spawnLocation;
    public bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            triggered = true;
            SceneManager.LoadScene(sceneName);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = spawnLocation;
        }
    }
    private void OnTriggerexit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
