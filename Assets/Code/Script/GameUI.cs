using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image _playerHealthBar = default;

    public void UpdatePlayerHealthBar(float newAmount)
    {
        _playerHealthBar.fillAmount = newAmount;
    }
}
