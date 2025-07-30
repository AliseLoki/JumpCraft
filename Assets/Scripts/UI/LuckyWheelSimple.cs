using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LuckyWheelSimple : MonoBehaviour
{
    private const string RewardId = "coin";

    private const float FullCircle = 360;
    private const float SectoreAngle = 45;

    [SerializeField] private UIHandler _ui;

    [SerializeField] Button _wheelButton;

    [SerializeField] Button _payCoin;
    [SerializeField] Button _payDiamond;
    [SerializeField] Button _whatchAdd;

    [SerializeField] private float _wheelSpeed = 800;
    [SerializeField] private float _timeBeforeCheating = 1;
    [SerializeField] private int _fullSpinningAmount = 3;

    [SerializeField] private List<WheelItem> _wheelItems;
    [SerializeField] private List<WheelItemSO> _wheelItemsSO;

    private int _addsViewing = 0;
    private PrizeName _currentPrizeName;

    public event Action<PrizeName> PrizeRecieved;

    private void OnEnable()
    {
        FillInWheelImagesData();
        SetInteractable(true);
    }

    public void OnWhatchAddButtonDown()
    {
        if(_addsViewing < 2)
        {
            YG2.RewardedAdvShow(RewardId, () =>
            {
                SetInteractable(true);
                _addsViewing++;
            });
        }
    }

    public void OnPayDiamondButtonDown()
    {
        if (_ui.Pay(ref YG2.saves.Diamond, -5, _ui.DiamondsAmountText))
        {
            SetInteractable(true);
        }
    }

    public void OnPayCoinButtonDown()
    {
        if (_ui.Pay(ref YG2.saves.Coin, -1, _ui.CoinAmountText))
        {
            SetInteractable(true);
        }
    }

    public void OnLuckyWheelButtonDown()
    {
        StartCoroutine(SpinningRoutine(_wheelButton.transform, _wheelSpeed));
    }

    private IEnumerator SpinningRoutine(Transform wheel, float defaultSpeed)
    {
        _ui.SoundController.PlaySound(SoundName.LuckyWheel.ToString());

        int index = GetCheatPrize();

        SetInteractable(false);

        yield return DefaultRotationRoutine(_timeBeforeCheating, 0, defaultSpeed, wheel);

        float distance = FullCircle * _fullSpinningAmount - wheel.transform.rotation.eulerAngles.z + index * SectoreAngle;
        float timeBeforeStop = (2 * distance) / defaultSpeed;

        yield return DefaultRotationRoutine(timeBeforeStop, defaultSpeed, 0, wheel);

        _ui.SoundController.StopSound();
        _ui.SoundController.PlaySound(SoundName.Win.ToString());

        PrizeRecieved?.Invoke(_currentPrizeName);
    }

    private IEnumerator DefaultRotationRoutine(float stopTime, float a, float b, Transform wheel)
    {
        float passedTime = 0;

        while (passedTime < stopTime)
        {
            float speed = Mathf.Lerp(a, b, passedTime / stopTime);
            wheel.rotation *= Quaternion.Euler(0, 0, speed * Time.deltaTime);
            passedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SetInteractable(bool isInteractable)
    {
        _wheelButton.interactable = isInteractable;
    }

    private void FillInWheelImagesData()
    {
        for (int i = 0; i < _wheelItems.Count; i++)
        {
            var wheelItemSO = GetWheelItemSO();
            _wheelItems[i].Init(wheelItemSO);
        }
    }

    private WheelItemSO GetWheelItemSO()
    {
        int randomIndex = UnityEngine.Random.Range(0, _wheelItemsSO.Count);

        if (_wheelItemsSO[randomIndex].Weight <= UnityEngine.Random.Range(0, 1))
        {
            return GetWheelItemSO();
        }

        return _wheelItemsSO[randomIndex];
    }

    private int GetCheatPrize()
    {
        int randomSector = UnityEngine.Random.Range(0, _wheelItems.Count);

        if (_wheelItems[randomSector].Weight <= UnityEngine.Random.Range(0f, 1f))
        {
            return GetCheatPrize();
        }

        _currentPrizeName = _wheelItems[randomSector].PrizeName;

        return randomSector;
    }
}
