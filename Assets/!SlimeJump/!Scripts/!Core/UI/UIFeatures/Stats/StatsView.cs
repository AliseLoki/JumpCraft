using TMPro;
using UnityEngine;
using YG;

public class StatsView : MonoBehaviour
{
    private const int _maxHeartAmount = 3;
    private const string Leaderboard = nameof(Leaderboard);

    [SerializeField] private TMP_Text _diamondsAmountText;
    [SerializeField] private TMP_Text _coinAmountText;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private Transform _healthContainer;
    [SerializeField] private HeartView _heartView;

    private Fabrica _fabrica;

    public void Init(Fabrica fabrica)
    {
        _fabrica = fabrica;
    }

    public void SetValues()
    {
        ChangeHealthView();
        ShowNewValue(_diamondsAmountText, YG2.saves.Diamond);
        ShowNewValue(_coinAmountText, YG2.saves.Coin);
    }

    public void ChangeHealth(int plusAmount)
    {
        if (YG2.saves.Heart < _maxHeartAmount)
        {
            ChangeValue(ref YG2.saves.Heart, plusAmount);
            ChangeHealthView();
        }
    }

    public void ChangeDiamonds(int plusAmount)
    {
        ChangeValue(ref YG2.saves.Diamond, plusAmount);
        ShowNewValue(_diamondsAmountText, YG2.saves.Diamond);
    }

    public void ChangeCoins(int plusAmount)
    {
        ChangeValue(ref YG2.saves.Coin, plusAmount);
        ShowNewValue(_coinAmountText, YG2.saves.Coin);
    }

    public void ChangeScore(int score)
    {
        YG2.SetLeaderboard(Leaderboard, score);
        ShowNewValue(_scoreText, score);
    }

    public bool PayDiamonds(int price)
    {
        if(TryToPay(ref YG2.saves.Diamond,price,_diamondsAmountText)) return true;
        return false;
    }

    public bool PayCoins(int price)
    {
        if (TryToPay(ref YG2.saves.Coin, price, _coinAmountText)) return true;
        return false;
    }

    private bool TryToPay(ref int value, int difference, TMP_Text text)
    {
        if (value + difference >= 0)
        {
            ChangeValue(ref value, difference);
            ShowNewValue(text, value);
            return true;
        }
        else return false;
    }

    private void ShowNewValue(TMP_Text text, int newValue)
    {
        text.text = newValue.ToString();
    }

    private void ChangeValue(ref int value, int plusValue)
    {
        value += plusValue;
        YG2.SaveProgress();
    }

    private void ChangeHealthView()
    {
        if (_healthContainer.childCount > YG2.saves.Heart)
        {
            Destroy(_healthContainer.GetChild(0).gameObject);
        }
        else if (_healthContainer.childCount < YG2.saves.Heart)
        {
            var newHeart = _fabrica.CreatePrefab(_heartView, Quaternion.identity, _healthContainer);
        }
    }
}