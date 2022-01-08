using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerOptions : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] private Material _playerStripMaterial = default;
    [Header("Options")]
    [SerializeField] private Color _stripColor = Color.grey;
    [Header("Damage")]
    [SerializeField] private Color _damageColor = Color.red;
    [SerializeField] private float _damageDurationSeconds = 2.0f;
    [Header("Heal")]
    [SerializeField] private Color _healColor = Color.green;
    [SerializeField] private float _healDurationSeconds = 2.0f;

    public Color PlayerColor
    {
        get => _stripColor;
    }

    public float DamageDuration
    {
        get => _damageDurationSeconds;
    }

    public Color DamageColor
    {
        get => _damageColor;
    }

    public float HealDuration
    {
        get => _healDurationSeconds;
    }

    public Color HealColor
    {
        get => _healColor;
    }

    public void UpdateStripTiling( int newTiling)
    {
        _playerStripMaterial.SetInt("_Tiling", newTiling);
    }

    private void UpdateStripColor()
    {
        _playerStripMaterial.SetColor("_Color", _stripColor);
    }

    public void UpdateStripColor(Color newColor)
    {
        _playerStripMaterial.SetColor("_Color", newColor);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStripColor();
    }
}
