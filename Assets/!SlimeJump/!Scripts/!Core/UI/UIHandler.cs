using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    private const int _onePrefabValue = 1;
    private const string OpenWheel = nameof(OpenWheelTest);

    [SerializeField] private SoundController _soundController;

    [SerializeField] private ShopView _shopView;
    [SerializeField] private LuckyWheelPanel _luckyWheel;
    [SerializeField] private StatsView _statsView;
    [SerializeField] private JumpPowerView _jumpPowerView;
    [SerializeField] private EducationPanel _educationPanel;
    // на кнопку старт надо конечно  завести паузу и т д
    [SerializeField] private Button _deleteSaves;

    [SerializeField] private PlayerViewSO _kingViewSO;

    private Player _player;
    private GameController _gameController;

    private bool _isGamePlaying = false;

    public bool IsGamePlaying => _isGamePlaying;

    public SoundController SoundController => _soundController;

    private void OnDisable()
    {
        _gameController.ScoreController.ScoreChanged -= OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged -= OnPlayerJumpPowerChanged;
        _luckyWheel.PrizeRecieved -= OnPrizeRecieved;
    }

    public void DeleteSaves()
    {
        YG2.SetDefaultSaves();
        YG2.SaveProgress();
        _statsView.SetValues();
    }

    public void Init(Player player, Fabrica fabrica, GameController platformsController)
    {
        _player = player;
        _gameController = platformsController;
        _shopView.Init(fabrica);
        _statsView.Init(fabrica);
    }

    public void OpenWheelTest()
    {
        _luckyWheel.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SetStartButtonActive();
        _statsView.SetValues();
        _jumpPowerView.SetJumpPowerScaleAmount(0);
        

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

    public void OnStartButtonPressed()
    {
        _educationPanel.gameObject.SetActive(false);
        //_educationText.gameObject.SetActive(false);
        //_startButton.gameObject.SetActive(false);
        _isGamePlaying = true;
        _player.DisableCollider(true);
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

    private void SetStartButtonActive()
    {
        _player.DisableCollider(false);
        _isGamePlaying = false;
        _educationPanel.gameObject.SetActive(true);
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
}