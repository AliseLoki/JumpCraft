using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Health : MonoBehaviour
{
    private int _minHealth = 1;
    private int _maxHealth = 3;
    private float _deathPosY = 0;

    public event Action HealthChanged;

    private void Update()
    {
        if (transform.position.y < _deathPosY) CheckHealth();
    }

    private void CheckHealth()
    {
        if (YG2.saves.Heart <= _minHealth) Die();
        else Rise();
    }

    public void ChangeHealth(int healthChangeValue)
    {
        YG2.saves.Heart = Mathf.Clamp(YG2.saves.Heart + healthChangeValue, 0, _maxHealth);
        HealthChanged?.Invoke();
    }

    private void Rise()
    {
        ChangeHealth(-1);
        YG2.SaveProgress();
        SceneManager.LoadScene(0);
    }

    private void Die()
    {
        YG2.SetDefaultSaves();
        YG2.InterstitialAdvShow();
        YG2.SaveProgress();
        SceneManager.LoadScene(0);
    }
}
