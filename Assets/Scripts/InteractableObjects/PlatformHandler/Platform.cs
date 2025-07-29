using UnityEngine;

public class Platform : Collectable
{
    [SerializeField] private PlatformScoreBonusView _scoreBonusView;
    [SerializeField] private Trampoline _trampoline;

    // ���� �������� �����
    //private int _trampolineSpawnChance = 4;
    private int _colorSpawnChance = 2;

    // � ������� ������� ������������� ����� ��������� ���������
    private float _sector2 = 1f; //1f  ������ 5, ������� �� 1.5
    private float _sector3 = 0.5f; //0.5f

    private bool _isEmpty = true;
    private bool _isGreen = true;
    private int _bonusScore;

    public bool IsGreen => _isGreen;

    public bool IsEmpty => _isEmpty;

    public Trampoline Trampoline => _trampoline;

    private void OnEnable()
    {
        _bonusScore = 0;
    }

    private void OnDisable()
    {
        _trampoline.gameObject.SetActive(false);
        _isEmpty = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            _bonusScore = CalculateBonus(player.transform.position.x, player.transform.position.z);
            _scoreBonusView.ShowScore(_bonusScore);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            _scoreBonusView.SetScoreBonusActive(false);
        }
    }

    public int GetBonus(Player player)
    {
        return CalculateBonus(player.transform.position.x, player.transform.position.z);
    }

    public void SetEmpty(bool isEmpty)
    {
        _isEmpty = isEmpty;
    }

    public void SetTrampolineActive()
    {
       // int random = Random.Range(0, _trampolineSpawnChance);
        int randomColor = Random.Range(0, _colorSpawnChance);

       // if (random == 0)
       // {
            if (randomColor == 0)
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
      //  }
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