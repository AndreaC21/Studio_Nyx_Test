using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image _playerHealthBar = default;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void UpdatePlayerHealthBar(float duration, float newAmount)
    {
        StartCoroutine(UpdateHealthBarAnimation(duration, newAmount));
    }

    private IEnumerator UpdateHealthBarAnimation(float duration,float newAmount)
    {
        float startTime = Time.realtimeSinceStartup;
        float endTime = startTime + duration;
        float previousAmount = _playerHealthBar.fillAmount;

        for (float i = startTime; i < endTime; i += Time.deltaTime)
        {
            yield return null;
            float amount = (i - startTime) / duration;
            _playerHealthBar.fillAmount = Mathf.Lerp(previousAmount, newAmount, amount);
        }
    }
}
