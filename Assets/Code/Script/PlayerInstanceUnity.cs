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

    public float DamageDuration
    {
        get => _playerOptions.DamageDuration;
    }

    public float HealDuration
    {
        get => _playerOptions.HealDuration;
    }

    private int StripTilling
    {
        get => _health / 10;
    }

    private void Awake()
    {
        _health = MAX_HEALTH;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
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

        if (amount < 0)
        {
            StartCoroutine(UpdatePlayerColorAnimation(_playerOptions.DamageDuration, _playerOptions.DamageColor));
        }
        else
        {
            StartCoroutine(UpdatePlayerColorAnimation(_playerOptions.HealDuration, _playerOptions.HealColor));
        }
    }

    private void PlayerDie()
    {
        OnPlayerDie();
        Destroy(gameObject);
    }

    private IEnumerator UpdatePlayerColorAnimation(float duration, Color temporaryColor)
    {
        float startTime = Time.realtimeSinceStartup;
        float endTime = startTime + duration;

        Color previousColor = _playerOptions.PlayerColor;
       
        for (float i = startTime; i < endTime; i += Time.deltaTime)
        {
            yield return null;
            Color lerpedColor = Color.Lerp(previousColor, temporaryColor, Mathf.PingPong(i * duration, 1));
            _playerOptions.UpdateStripColor(lerpedColor);
        }
    }

    public delegate void PlayerDieEvent();
    public event PlayerDieEvent OnPlayerDie;
}
