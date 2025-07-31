using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    private const string OpenWheel = nameof(OpenWheelTest);

    [SerializeField] private SoundController _soundController;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private LuckyWheelSimple _luckyWheel;

    [SerializeField] private TMP_Text _educationText;
    [SerializeField] private TMP_Text _diamondsAmountText;
    [SerializeField] private TMP_Text _coinAmountText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _jumpPowerScaleImage;
    [SerializeField] private Button _startButton;
    [SerializeField] private Transform _healthContainer;
    [SerializeField] private HeartView _heartView;

    [SerializeField] private PlayerViewSO _kingViewSO;

    private int _maxHeartAmount = 3;
    private Fabrica _fabrica;
    private Player _player;
    private GameController _gameController;

    private bool _isGamePlaying = false;

    [Header("JumpPower Values")]
    [SerializeField] float _minValue = 3;
    [SerializeField] float _divider = 10;
    // вот здесь подправить в зависимости от установленных в тесте

    public TMP_Text DiamondsAmountText => _diamondsAmountText;

    public TMP_Text CoinAmountText => _coinAmountText;

    public bool IsGamePlaying => _isGamePlaying;

    public SoundController SoundController => _soundController;

    private void OnDisable()
    {
        _gameController.ScoreController.ScoreChanged -= OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged -= OnPlayerJumpPowerChanged;
        _luckyWheel.PrizeRecieved -= OnPrizeRecieved;
    }

    public void Init(Player player, Fabrica fabrica, GameController platformsController)
    {
        _player = player;
        _gameController = platformsController;
        _shopView.Init(fabrica);
        _fabrica = fabrica;
    }

    public void OpenWheelTest()
    {
        _luckyWheel.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SetStartButtonActive();
        ShowNewValue(_diamondsAmountText, YG2.saves.Diamond);
        ShowNewValue(_coinAmountText, YG2.saves.Coin);
        FillInHealth();
        SetJumpPowerScaleAmount(0);

        _gameController.ScoreController.ScoreChanged += OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged += OnPlayerJumpPowerChanged;
        _luckyWheel.PrizeRecieved += OnPrizeRecieved;
    }


    public void ChangeCollectablesAmount(CollectableName name)
    {
        switch (name)
        {
            case CollectableName.Cake:
                _soundController.PlaySound(SoundName.Cake.ToString());
              
                if (YG2.saves.Heart < _maxHeartAmount)
                {
                    ChangeValue(ref YG2.saves.Heart, 1);
                    ChangeHealthView();
                }

                break;

            case CollectableName.Diamond:
                _soundController.PlaySound(SoundName.Diamond.ToString());
                ChangeValue(ref YG2.saves.Diamond, 1);
                ShowNewValue(_diamondsAmountText, YG2.saves.Diamond);
                break;

            case CollectableName.Coin:
                _soundController.PlaySound(SoundName.Coin.ToString());
                ChangeValue(ref YG2.saves.Coin, 1);
                ShowNewValue(_coinAmountText, YG2.saves.Coin);
                break;

            default:

                break;
        }
    }

    //убрать потом

    public void TestDeleteSavesButton()
    {
        YG2.SetDefaultSaves();
        ShowNewValue(_diamondsAmountText, YG2.saves.Diamond);
        YG2.SaveProgress();
    }

    public void OnStartButtonPressed()
    {
        _isGamePlaying = true;
        _educationText.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);
    }

    public void OpenLuckyWheelPanel()
    {
        _isGamePlaying = false;

        Invoke(OpenWheel, 0.5f);
    }

    public void OpenShop()
    {
        _shopView.gameObject.SetActive(true);
        _isGamePlaying = false;
    }

    public void CloseShop()
    {
        _shopView.gameObject.SetActive(false);
        _isGamePlaying = true;
    }

    public void CloseWheel()
    {
        _luckyWheel.gameObject.SetActive(false);
        _isGamePlaying = true;
    }

    public bool Pay(ref int value, int difference, TMP_Text text)
    {
        if (CheckIfCanPay(value, difference))
        {
            ChangeValue(ref value, difference);
            ShowNewValue(text, value);
            return true;
        }

        return false;
    }

    private bool CheckIfCanPay(int value, int difference)
    {
        if (value + difference >= 0) return true;
        else return false;
    }

    private void SetStartButtonActive()
    {
        _isGamePlaying = false;
        _startButton.gameObject.SetActive(true);
        _educationText.gameObject.SetActive(true);
    }

    private void FillInHealth()
    {
        for (int i = 0; i < YG2.saves.Heart; i++)
        {
            var newHeart = _fabrica.CreatePrefab(_heartView, Quaternion.identity, _healthContainer);
        }
    }

    private void OnPlayerJumpPowerChanged(float jumpPower)
    {
        SetJumpPowerScaleAmount((jumpPower - _minValue) / _divider);
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

    private void OnPrizeRecieved(PrizeName prizeName)
    {
        switch (prizeName)
        {
            case PrizeName.Heart:

                if (YG2.saves.Heart < _maxHeartAmount)
                {
                    ChangeValue(ref YG2.saves.Heart, 1);
                    ChangeHealthView();
                }

                break;

            case PrizeName.Pig:
                _player.OnPlayerViewChanged(_kingViewSO);
                break;

            case PrizeName.Diamond1:
                ChangeValue(ref YG2.saves.Diamond, 1);
                ShowNewValue(_diamondsAmountText, YG2.saves.Diamond);
                break;

            case PrizeName.Diamond2:
                ChangeValue(ref YG2.saves.Diamond, 2);
                ShowNewValue(_diamondsAmountText, YG2.saves.Diamond);
                break;

            case PrizeName.Diamond3:
                ChangeValue(ref YG2.saves.Diamond, 5);
                ShowNewValue(_diamondsAmountText, YG2.saves.Diamond);
                break;

            case PrizeName.Coin:
                ChangeValue(ref YG2.saves.Coin, 1);
                ShowNewValue(_coinAmountText, YG2.saves.Coin);
                break;

            default:

                break;
        }
    }

    private void OnScoreChanged(int score)
    {
        YG2.SetLeaderboard("leaderboard", score);
        ShowNewValue(_scoreText, score);
    }

    private void SetJumpPowerScaleAmount(float jumpPower)
    {
        _jumpPowerScaleImage.fillAmount = jumpPower;
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
}