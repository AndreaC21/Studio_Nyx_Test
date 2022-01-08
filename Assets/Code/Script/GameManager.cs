using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private GameUI _gameUI = default;
    [SerializeField] private PlayerInstanceUnity _playerInstance = default;


    private void Awake()
    {
        _inputManager.OnIncreaseHealth += IncreasePlayerHealth;
        _inputManager.OnReduceHealth += ReducePlayerHealth;

        _playerInstance.OnPlayerDie += PlayerDie;
    }

    private void Start()
    {
        _gameUI.UpdatePlayerHealthBar(_playerInstance.HealthAmount);
    }

    private void OnDestroy()
    {
        _inputManager.OnIncreaseHealth -= IncreasePlayerHealth;
        _inputManager.OnReduceHealth -= ReducePlayerHealth;
    }

    private void ReducePlayerHealth()
    {
        UpdatePlayerHealth(-30);
    }

    private void IncreasePlayerHealth()
    {
        UpdatePlayerHealth(20);
    }


    private void UpdatePlayerHealth(int amount)
    {
        _playerInstance.UpdateHealth(amount);
        _gameUI.UpdatePlayerHealthBar(_playerInstance.HealthAmount);
    }

    private void PlayerDie()
    {
        _inputManager.OnIncreaseHealth -= IncreasePlayerHealth;
        _inputManager.OnReduceHealth -= ReducePlayerHealth;
    }
}
