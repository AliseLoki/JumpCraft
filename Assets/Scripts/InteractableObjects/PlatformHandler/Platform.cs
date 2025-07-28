using System.Collections;
using UnityEngine;

public class Platform : Collectable
{
    [SerializeField] private PlatformScoreBonusView _scoreBonusView;
    [SerializeField] private Trampoline _trampoline;

    // в будущем сделать просчитывание через коллайдер платформы
    private float _sector2 = 2f; //1f  размер 5, условно по 1.5
    private float _sector3 = 1f; //0.5f

    private bool _isGreen = true;
    private int _bonusScore;

    private PlatformsScoreController _scoreController;

    public bool IsGreen =>_isGreen;

    public Trampoline Trampoline => _trampoline;

    private void OnEnable()
    {
        _bonusScore = 0;
        SetTrampolineActive();
    }

    private void OnDisable()
    {       
        _trampoline.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            _bonusScore = CalculateBonus(player.transform.position.x, player.transform.position.z);
            _scoreBonusView.ShowScore(_bonusScore);
            if (_scoreController != null) _scoreController.OnScoreChanged(_bonusScore);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            _scoreBonusView.SetScoreBonusActive(false);
        }
    }

    public void Init(PlatformsScoreController scoreController)
    {
        _scoreController = scoreController;
    }

    public bool CheckIfPlayerOnTrampoline(float x, float z)
    {
        if (CalculateBonus(x, z) == _scoreBonusView.BonusMax && _trampoline.gameObject.activeSelf) return true;
        return false;
    }

    private void SetTrampolineActive()
    {
        int random = Random.Range(0, Semen.Instance.TrampolineSpawnChance);
        int randomColor = Random.Range(0, Semen.Instance.ColorTrampolineSpawnChance);
        
        if ( random == 0)
        {           
            if(randomColor == 0)
            {
                _isGreen = false;
                _trampoline.SetMaterial(_isGreen);
            }
            else
            {
                _isGreen = true;
                _trampoline.SetMaterial(_isGreen);
            }

            _trampoline.gameObject.SetActive(true);
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