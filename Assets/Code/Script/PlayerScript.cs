using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerScript : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] private Material _playerStripMaterial = default;
    [Header("Options")]
    [SerializeField] private Color _stripColor = Color.grey;

    // Start is called before the first frame update
    void Start()
    {
        
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
