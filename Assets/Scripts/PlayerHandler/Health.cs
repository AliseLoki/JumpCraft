using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Health : MonoBehaviour
{
    private int _minHealth = 1;
    private int _maxHealth = 3;

    private float _deathPosY = 0;

    private void Update()
    {
        if (transform.position.y < _deathPosY)
        {
            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        if (YG2.saves.Health <= _minHealth)
        {
            Die();
        }
        else
        {
            Rise();
            ChangeHealth(-_minHealth);
        }
    }

    private void ChangeHealth(int healthChangeValue)
    {
        YG2.saves.Health = Mathf.Clamp(YG2.saves.Health + healthChangeValue, _minHealth, _maxHealth);
    }

    private void Rise()
    {
        SceneManager.LoadScene(0);
    }

    private void Die()
    {

    }
}
