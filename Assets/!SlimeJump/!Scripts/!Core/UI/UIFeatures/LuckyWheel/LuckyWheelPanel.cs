using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LuckyWheelPanel : MonoBehaviour
{
    private const string RewardId = "coin";

    [SerializeField] private UIHandler _ui;

    [SerializeField] Button _wheel;
    [SerializeField] Button _payCoin;
    [SerializeField] Button _payDiamond;
    [SerializeField] Button _whatchAdd;
    [SerializeField] Button _close;

    [SerializeField] private List<WheelItem> _wheelItems;
    [SerializeField] private List<WheelItemSO> _wheelItemsSO;

    private int _addsViewing = 0;
    private int _maxAddsViewing = 2;

    private int _spinPriceCoin = - 1;
    private int _spinPriceDiamond = - 5;

    private Spinning _spinning;

    public event Action<PrizeName> PrizeRecieved;

    private void Awake()
    {
        _spinning = new Spinning(_ui.SoundController);
    }

    private void OnEnable()
    {
        FillInWheelImagesData();
        SubscribeToButtonsEvents();
        _spinning.PrizeRecieved += OnPrizeRecieved;
    }

    private void OnDisable()
    {
        UnsubscribeFromButtonsEvents();
        _spinning.PrizeRecieved -= OnPrizeRecieved;
        StopAllCoroutines();
        _wheel.interactable = true;
    }

    private void OnWhatchAddButtonDown()
    {
        if (_addsViewing < _maxAddsViewing)
        {
            YG2.RewardedAdvShow(RewardId, () =>
            {
                _addsViewing++;
                SpinWheel();
            });
        }
    }

    private void OnPayDiamondButtonDown()
    {
        if (_ui.PayDiamond(_spinPriceDiamond)) SpinWheel();
    }

    private void OnPayCoinButtonDown()
    {
        if (_ui.PayCoin(_spinPriceCoin)) SpinWheel();
    }

    private void OnLuckyWheelButtonDown()
    {
        _wheel.interactable = false;
        SpinWheel();
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

    private void SubscribeToButtonsEvents()
    {
        _wheel.onClick.AddListener(OnLuckyWheelButtonDown);
        _whatchAdd.onClick.AddListener(OnWhatchAddButtonDown);
        _payDiamond.onClick.AddListener(OnPayDiamondButtonDown);
        _payCoin.onClick.AddListener(OnPayCoinButtonDown);
    }

    private void UnsubscribeFromButtonsEvents()
    {
        _wheel.onClick.RemoveAllListeners();
        _whatchAdd.onClick.RemoveAllListeners();
        _payDiamond.onClick.RemoveAllListeners();
        _payCoin.onClick.RemoveAllListeners();
    }

    private void SetAllButtonsInteractable(bool isTrue)
    {
        _payCoin.interactable = isTrue;
        _payDiamond.interactable = isTrue;
        _whatchAdd.interactable = isTrue;
        _close.interactable = isTrue;
    }

    private void OnPrizeRecieved(PrizeName prizeName)
    {
        SetAllButtonsInteractable(true);
        PrizeRecieved?.Invoke(prizeName);
    }

    private void SpinWheel()
    {
        SetAllButtonsInteractable(false);
        StartCoroutine(_spinning.SpinningRoutine(_wheel.transform, _wheelItems));
    }
}