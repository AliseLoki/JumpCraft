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
        if (transform.position.y < _deathPosY)
        {
            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        Debug.Log(YG2.saves.Health);


        if (YG2.saves.Health <= _minHealth)
        {
            Die();
        }
        else
        {
            ChangeHealth(-1);
            YG2.SaveProgress();
            Rise();
        }
    }

    public void ChangeHealth(int healthChangeValue)
    {
        YG2.saves.Health = Mathf.Clamp(YG2.saves.Health + healthChangeValue, 0, _maxHealth);
        HealthChanged?.Invoke();
    }

    private void Rise()
    {

        SceneManager.LoadScene(0);
    }

    private void Die()
    {

    }
}
