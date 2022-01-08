using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstanceUnity : MonoBehaviour
{
    [SerializeField] private PlayerOptions _playerOptions;

    private const int MAX_HEALTH = 100;
    private const int MIN_HEALTH = 0;
    private int _health;

    public float HealthAmount
    {
        get =>(float) _health / MAX_HEALTH;
    }

    private int StripTilling
    {
        get => _health / 10;
    }

    private void Awake()
    {
        _health = MAX_HEALTH;
    }

    /// <summary>
    /// According to the amount, reduce or increase player's health
    /// </summary>
    /// <param name="amount"> It's the amount <b>earn/lose</b> for the player's health</param>
    public void UpdateHealth(int amount)
    {
        _health = Mathf.Clamp(_health + amount, MIN_HEALTH, MAX_HEALTH);
        _playerOptions.UpdateStripTiling(StripTilling);

        if (_health == MIN_HEALTH)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {

    }
}
