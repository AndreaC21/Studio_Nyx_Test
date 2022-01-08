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

    public void UpdateStripTiling( int newTiling)
    {
        _playerStripMaterial.SetInt("_Tiling", newTiling);
    }

    private void UpdateStripColor()
    {
        _playerStripMaterial.SetColor("_Color", _stripColor);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStripColor();
    }
}
