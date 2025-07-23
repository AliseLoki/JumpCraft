using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private SoundController _soundController;
    [SerializeField] private ShopView _shopView;

    [SerializeField] private TMP_Text _diamondsAmountText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _jumpPowerScaleImage;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _startButton;

    [SerializeField] private Transform _healthContainer;
    [SerializeField] private HeartView _heartView;

    private Fabrica _fabrica;
    private Player _player;
    private PlatformsController _platformsController;

    [Header("JumpPower Values")]
    [SerializeField] float _minValue = 3;
    [SerializeField] float _divider = 10;
    // вот здесь подправить в зависимости от установленных в тесте

    private void OnDisable()
    {
        _player.CollectablesAmountChanged -= OnCollectablesAmountChanged;
        _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
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
        FillInHealth();

        ShowNewValue(_diamondsAmountText, YG2.saves.DiamondsAmount);
        SetJumpPowerScaleAmount(0);
        _restartButton.onClick.AddListener(OnRestartButtonPressed);
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

    private void FillInHealth()
    {
        for (int i = 0; i < YG2.saves.Health; i++)
        {
            var newHeart = _fabrica.CreatePrefab(_heartView, Quaternion.identity, _healthContainer);
        }
    }

    private void OnRestartButtonPressed()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(0);
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