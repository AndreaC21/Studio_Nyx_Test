using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI = default;
    [SerializeField] private PlayerInstanceUnity _playerInstance = default;

    public void Start()
    {
        UpdatePlayerHealth(-50);
    }

    private void UpdatePlayerHealth(int amount)
    {
        _playerInstance.UpdateHealth(amount);
        _gameUI.UpdatePlayerHealthBar(_playerInstance.HealthAmount);
    }
}
