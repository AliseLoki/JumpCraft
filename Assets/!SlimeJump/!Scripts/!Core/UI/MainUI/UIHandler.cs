using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    private const int _onePrefabValue = 1;
    private const string OpenWheel = nameof(OpenWheelOpen);

    [SerializeField] private SoundController _soundController;

    [Header("Panels")]
    [SerializeField] private ShopView _shopView;
    [SerializeField] private LuckyWheelPanel _luckyWheel;
    [SerializeField] private StatsView _statsView;
    [SerializeField] private JumpPowerView _jumpPowerView;
    [SerializeField] private EducationPanel _educationPanel;

    [Header("Buttons")]
    [SerializeField] private Button _closeWheelButton;
    [SerializeField] private Button _closeShopButton;
    [SerializeField] private Button _openShopButton;
    [SerializeField] private Button _startButton;
   
    [Header("TemporaryHere")]
    [SerializeField] private PlayerViewSO _kingViewSO;

    private Player _player;
    private GameController _gameController;

    private bool _isGamePlaying = false;

    public bool IsGamePlaying => _isGamePlaying;

    public SoundController SoundController => _soundController;

    private void OnDisable()
    {
        UnSubscribeFromButtonsEvents();
        _gameController.ScoreController.ScoreChanged -= OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged -= OnPlayerJumpPowerChanged;
        _luckyWheel.PrizeRecieved -= OnPrizeRecieved;
    }

    public void Init(Player player, Fabrica fabrica, GameController gameController)
    {
        _player = player;
        _gameController = gameController;
        _shopView.Init(fabrica);
        _statsView.Init(fabrica);
    }

    public void StartGame()
    {
        SubscribeOnButtonsEvents();

        SetStartButtonActive();
        _statsView.SetValues();
        _jumpPowerView.SetJumpPowerScaleAmount(0);

        _gameController.ScoreController.ScoreChanged += OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged += OnPlayerJumpPowerChanged;
        _luckyWheel.PrizeRecieved += OnPrizeRecieved;
    }

    public void OpenLuckyWheelPanel()
    {
        SetPause(false);
        _openShopButton.interactable = false;
        Invoke(OpenWheel, 0.5f);
    }

    public void ChangeCollectablesAmount(CollectableName name)
    {
        switch (name)
        {
            case CollectableName.Cake:
                _soundController.PlaySound(SoundName.Cake.ToString());
                _statsView.ChangeHealth(_onePrefabValue);
                break;

            case CollectableName.Diamond:
                _soundController.PlaySound(SoundName.Diamond.ToString());
                _statsView.ChangeDiamonds(_onePrefabValue);
                break;

            case CollectableName.Coin:
                _soundController.PlaySound(SoundName.Coin.ToString());
                _statsView.ChangeCoins(_onePrefabValue);
                break;

            default:

                break;
        }
    }

    public bool PayCoin(int price)
    {
        if (_statsView.PayCoins(price)) return true;
        return false;
    }

    public bool PayDiamond(int price)
    {
        if (_statsView.PayDiamonds(price)) return true;
        return false;
    }

    private void OnPlayerJumpPowerChanged(float jumpPower)
    {
        _jumpPowerView.ChangeJumpPowerImage(jumpPower);
    }

    private void OnPrizeRecieved(PrizeName prizeName)
    {
        switch (prizeName)
        {
            case PrizeName.Heart:
                _statsView.ChangeHealth(_onePrefabValue);
                break;

            case PrizeName.Pig:
                _player.OnPlayerViewChanged(_kingViewSO);
                break;

            case PrizeName.Diamond1:
                _statsView.ChangeDiamonds(_onePrefabValue);
                break;

            case PrizeName.Diamond2:
                _statsView.ChangeDiamonds(_onePrefabValue + 2);
                break;

            case PrizeName.Diamond3:
                _statsView.ChangeDiamonds(_onePrefabValue + 4);
                break;

            case PrizeName.Coin:
                _statsView.ChangeCoins(_onePrefabValue);
                break;

            default:
                break;
        }
    }

    private void OnScoreChanged(int score)
    {
        _statsView.ChangeScore(score);
    }

    private void SubscribeOnButtonsEvents()
    {
        _closeWheelButton.onClick.AddListener(OnCloseWheelButtonPressed);
        _closeShopButton.onClick.AddListener(OnCloseShopButtonPressed);
        _openShopButton.onClick.AddListener(OnOpenShopButtonPressed);
        _startButton.onClick.AddListener(OnStartButtonPressed);
    }

    private void UnSubscribeFromButtonsEvents()
    {
        _closeWheelButton.onClick.RemoveAllListeners();
        _closeShopButton.onClick.RemoveAllListeners();
        _openShopButton.onClick.RemoveAllListeners();
        _startButton.onClick.RemoveAllListeners();
    }

    public void OnCloseWheelButtonPressed()
    {
        _luckyWheel.gameObject.SetActive(false);
        _openShopButton.interactable = true;
        SetPause(true);
    }

    private void OnCloseShopButtonPressed()
    {
        _shopView.gameObject.SetActive(false);
        SetPause(true);
    }

    private void OnOpenShopButtonPressed()
    {
        SetPause(false);
        _shopView.gameObject.SetActive(true);
    }

    private void OnStartButtonPressed()
    {
        _educationPanel.gameObject.SetActive(false);
        _openShopButton.interactable = true;
        SetPause(true);
    }

    private void SetStartButtonActive()
    {
        SetPause(false);
        _openShopButton.interactable = false;
        _educationPanel.gameObject.SetActive(true);
    }

    private void OpenWheelOpen()
    {
        _luckyWheel.gameObject.SetActive(true);
    }

    private void SetPause(bool isPause)
    {
        _isGamePlaying = isPause;
        _player.DisableCollider(isPause);
    }
}