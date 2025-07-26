using UnityEngine;

public class PlatformsController : MonoBehaviour
{
    private Player _player;
    private CollectablesController _collectablesController;

    private Platform _firstPlatform;
    private Platform _secondPlatform;
    private Platform _defaultPlatform;
    private Platform _currentPlatform;

    private ObjectsPool _objectsPool;

    [SerializeField] private Transform _defaultPosition;

    private float _minOffset = 6;
    private float _maxOffset = 9;

    private float _minHeightOffset = 0;
    private float _maxHeightOffset = 6;

    private PlatformsScoreController _scoreController;
    public PlatformsScoreController ScoreController => _scoreController;

    public Platform CurrentPlatform => _currentPlatform;

    private void OnDisable()
    {
        _player.CollisionHandler.PlayerJumpedOnPlatform -= OnPlayerJumpedOnPlatform;
    }

    public void Init(ObjectsPool pool, Player player, CollectablesController collectablesController)
    {
        _defaultPosition = transform;
        _collectablesController = collectablesController;
        _scoreController = new PlatformsScoreController();
        _objectsPool = pool;
        _currentPlatform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool, new Vector3(0, 3.1f, 0)) as Platform;
        _player = player;
        _player.CollisionHandler.PlayerJumpedOnPlatform += OnPlayerJumpedOnPlatform;
        Spawn2Platforms();
    }

    private void OnPlayerJumpedOnPlatform(Platform platform)
    {
        InitPlatforms(platform);

        if (!_secondPlatform.Trampoline.gameObject.activeSelf)
        {
            int chance = Random.Range(0, 2);

            if (chance == 0) _collectablesController.SpawnPig(_secondPlatform.transform.position);
            else _collectablesController.SpawnHeart(_secondPlatform.transform.position);
        }

        _collectablesController.SpawnDiamond(CalculateCenterBetweenPlatforms(_firstPlatform, _secondPlatform));

        if (platform.CheckIfPlayerOnTrampoline(_player.transform.position.x, _player.transform.position.z))
        {
            if (platform.IsGreen) _player.JumpHandler.TrampolineJump(_firstPlatform);
            else _player.JumpHandler.RedTrampolineJump(_firstPlatform);
        }
    }

    private void InitPlatforms(Platform platform)
    {
        if (platform == _firstPlatform)
        {
            _defaultPlatform = _currentPlatform;
            _currentPlatform = _firstPlatform;
            _firstPlatform = _secondPlatform;
            _defaultPosition = _firstPlatform.transform;
            _secondPlatform = GetPlatformFromPool();
        }
        else if (platform == _secondPlatform)
        {
            _currentPlatform = _secondPlatform;
            _defaultPlatform = _firstPlatform;
            Spawn2Platforms();
        }

        if (_defaultPlatform != null) _defaultPlatform.gameObject.SetActive(false);
    }

    private Vector3 CalculateCenterBetweenPlatforms(Platform first, Platform second)
    {
        return (first.transform.position + second.transform.position) / 2;
    }

    private void Spawn2Platforms()
    {
        _firstPlatform = GetPlatformFromPool();
        _secondPlatform = GetPlatformFromPool();
    }

    private Platform GetPlatformFromPool()
    {
        var newPlatfrorm = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool,
            CalculatePlatformPos(_defaultPosition,
            CalculateRandomValue(_minOffset, _maxOffset), 0)) as Platform;
        _defaultPosition = newPlatfrorm.transform;
        newPlatfrorm.Init(_scoreController);
        return newPlatfrorm;
    }

    private Vector3 CalculatePlatformPos(Transform defaultPos, float offsetX, float offsetZ)
    {
        float randomHeight = CalculateRandomValue(_minHeightOffset, _maxHeightOffset);
        return new Vector3(defaultPos.position.x + offsetX, randomHeight, defaultPos.position.z + offsetZ);
    }

    private float CalculateRandomValue(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}