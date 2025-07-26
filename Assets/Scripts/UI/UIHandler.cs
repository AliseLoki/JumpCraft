using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private SoundController _soundController;
    [SerializeField] private ShopView _shopView;

    [SerializeField] private TMP_Text _educationText;
    [SerializeField] private TMP_Text _diamondsAmountText;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private Image _jumpPowerScaleImage;

    [SerializeField] private Button _startButton;

    [SerializeField] private Transform _healthContainer;
    [SerializeField] private HeartView _heartView;

    private Fabrica _fabrica;
    private Player _player;
    private PlatformsController _platformsController;

    private bool _isGamePlaying = false;

    [Header("JumpPower Values")]
    [SerializeField] float _minValue = 3;
    [SerializeField] float _divider = 10;
    // вот здесь подправить в зависимости от установленных в тесте

    public bool IsGamePlaying => _isGamePlaying;

    private void OnDisable()
    {
        _player.CollectablesAmountChanged -= OnCollectablesAmountChanged;
        _platformsController.ScoreController.ScoreChanged -= OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged -= OnPlayerJumpPowerChanged;
        _player.Health.HealthChanged -= OnHealthChanged;
    }

    public void Init(Player player, Fabrica fabrica, PlatformsController platformsController)
    {
        _player = player;
        _platformsController = platformsController;
        _shopView.Init(fabrica);
        _fabrica = fabrica;
    }

    public void StartGame()
    {
        SetStartButtonActive();
        FillInHealth();

        ShowNewValue(_diamondsAmountText, YG2.saves.DiamondsAmount);
        SetJumpPowerScaleAmount(0);

        _player.CollectablesAmountChanged += OnCollectablesAmountChanged;
        _platformsController.ScoreController.ScoreChanged += OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged += OnPlayerJumpPowerChanged;
        _player.Health.HealthChanged += OnHealthChanged;
    }
    //убрать потом
    public void TestDeleteSavesButton()
    {
        YG2.SetDefaultSaves();
        ShowNewValue(_diamondsAmountText, YG2.saves.DiamondsAmount);
        YG2.SaveProgress();
    }

    public void OnStartButtonPressed()
    {
        _isGamePlaying = true;
        _educationText.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);
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

    private void SetStartButtonActive()
    {
        _isGamePlaying = false;
        _startButton.gameObject.SetActive(true);
        _educationText.gameObject.SetActive(true);
    }

    private void FillInHealth()
    {
        for (int i = 0; i < YG2.saves.Health; i++)
        {
            var newHeart = _fabrica.CreatePrefab(_heartView, Quaternion.identity, _healthContainer);
        }
    }

    private void OnPlayerJumpPowerChanged(float jumpPower)
    {
        SetJumpPowerScaleAmount((jumpPower - _minValue) / _divider);
    }

    private void OnCollectablesAmountChanged()
    {
        YG2.saves.DiamondsAmount++;
        YG2.SaveProgress();
        ShowNewValue(_diamondsAmountText, YG2.saves.DiamondsAmount);
    }

    private void OnHealthChanged()
    {
        if (_healthContainer.childCount > YG2.saves.Health)
        {
            Destroy(_healthContainer.GetChild(0).gameObject);
        }
        else if (_healthContainer.childCount < YG2.saves.Health)
        {
            var newHeart = _fabrica.CreatePrefab(_heartView, Quaternion.identity, _healthContainer);
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
}