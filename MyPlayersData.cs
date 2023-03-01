using UnityEngine;

[System.Serializable]
public class MyPlayersData
{
    public float playerMoney;
    public int currentHP;
    public Vector3 playerPosition;
    public Quaternion rotation;
    public GameObject prefab;

    public MyPlayersData(float _playerMoney, int _currentHP, Vector3 _playerPosition, Quaternion _rotation, GameObject _prefab)
    {
        playerMoney = _playerMoney;
        currentHP = _currentHP;
        playerPosition = _playerPosition;
        rotation = _rotation;
        prefab = _prefab;
    }
}

