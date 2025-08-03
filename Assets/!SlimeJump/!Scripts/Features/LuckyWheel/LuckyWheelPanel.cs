using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LuckyWheelPanel : MonoBehaviour
{
    private const string RewardId = "coin";

    [SerializeField] private UIHandler _ui;

    [SerializeField] Button _wheelButton;

    [SerializeField] Button _payCoin;
    [SerializeField] Button _payDiamond;
    [SerializeField] Button _whatchAdd;

    [SerializeField] private List<WheelItem> _wheelItems;
    [SerializeField] private List<WheelItemSO> _wheelItemsSO;

    private int _addsViewing = 0;
   
    private Spinning _spinning;

    public event Action<PrizeName> PrizeRecieved;

    private void Awake()
    {
        _spinning = new Spinning(_ui.SoundController);
    }

    private void OnEnable()
    {
        FillInWheelImagesData();
        SetInteractable(true);

        _spinning.PrizeRecieved += OnPrizeRecieved;
    }

    private void OnDisable()
    {
        _ui.SoundController.StopSound();
        StopAllCoroutines();
    }

    public void OnWhatchAddButtonDown()
    {
        if (_addsViewing < 2)
        {
            YG2.RewardedAdvShow(RewardId, () =>
            {
               // StartCoroutine(SpinningRoutine(_wheelButton.transform, _wheelSpeed));
                _addsViewing++;
                SetAllButtonsInteractable(false);
            });
        }
    }

    public void OnPayDiamondButtonDown()
    {
        if (_ui.Pay(ref YG2.saves.Diamond, -5, _ui.DiamondsAmountText))
        {
            //StartCoroutine(SpinningRoutine(_wheelButton.transform, _wheelSpeed));
            SetAllButtonsInteractable(false);
        }
    }

    public void OnPayCoinButtonDown()
    {
        if (_ui.Pay(ref YG2.saves.Coin, -1, _ui.CoinAmountText))
        {
           // StartCoroutine(SpinningRoutine(_wheelButton.transform, _wheelSpeed));
            SetAllButtonsInteractable(false);
        }
    }

    public void OnLuckyWheelButtonDown()
    {
        SetInteractable(false);
        SetAllButtonsInteractable(false);
        StartCoroutine(_spinning.SpinningRoutine(_wheelButton.transform, _wheelItems));
        
       
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

    private void SetAllButtonsInteractable(bool isTrue)
    {
        _payCoin.interactable = isTrue;
        _payDiamond.interactable = isTrue;
        _whatchAdd.interactable = isTrue;
    }

    private void OnPrizeRecieved(PrizeName prizeName)
    {
        PrizeRecieved?.Invoke(prizeName);
    }
}
