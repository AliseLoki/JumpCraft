using System;
using UnityEngine;

public class Platform : Interactable
{
    [SerializeField] private PlatformScoreBonusView _scoreBonusView;
    [SerializeField] private Transform _viewContainer;

    // в будущем сделать просчитывание через коллайдер платформы
    private float _sector2 = 1f;
    private float _sector3 = 0.5f;

    private int _bonusScore;

    public event Action<int> PlayerLandedOnPlatform;
    public Transform ViewContainer => _viewContainer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            _bonusScore = CalculateBonus(player.transform.position.x, player.transform.position.z);
            _scoreBonusView.ShowScore(_bonusScore);
            PlayerLandedOnPlatform?.Invoke(_bonusScore);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            _scoreBonusView.SetScoreBonusActive(false);
        }
    }

    private int CalculateBonus(float x, float z)
    {
        float positiveX = CalculatePlayerLandingZone(x, transform.position.x);
        float positiveZ = CalculatePlayerLandingZone(z, transform.position.z);

        if (positiveX < _sector3 && positiveZ < _sector3) return _scoreBonusView.BonusMax;
        else if (positiveX < _sector2 && positiveZ < _sector2) return _scoreBonusView.BonusMid;
        else return _scoreBonusView.BonusMin;
    }

    private float CalculatePlayerLandingZone(float playerPos, float platformPos)
    {
        float difference = playerPos - platformPos;
        float positivePos = difference < 0 ? -difference : difference;

        return positivePos;
    }
}