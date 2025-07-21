using System;
using UnityEngine;

public class PlatformsController : MonoBehaviour
{
    private Player _player;

    private Platform _firstPlatform;
    private Platform _secondPlatform;
    private Platform _defaultPlatform;
    private Platform _currentPlatform;

    private ObjectsPool _objectsPool;

    [SerializeField] private Transform _defaultPosition;

    //private float _minOffset = 5;
    //private float _maxOffset = 8;

    //private float _minHeightOffset = 0;
    //private float _maxHeightOffset = 3;

    //private Vector3 _centerBetweenCurrentAndPreviousPlatform;

    private PlatformsScoreController _scoreController;

    public event Action<bool> PlatformHasSpawnedOnAxisX;

    public PlatformsScoreController ScoreController => _scoreController;

    //public Vector3 Center => _centerBetweenCurrentAndPreviousPlatform;

    private void OnDisable()
    {
        _player.CollisionHandler.PlayerJumpedOnPlatform -= OnPlayerJumpedOnPlatform;
    }

    public void Init(ObjectsPool pool, Player player)
    {
        _defaultPosition = transform;
        _scoreController = new PlatformsScoreController();
        _objectsPool = pool;
        _currentPlatform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool, new Vector3(0, 3.1f, 0)) as Platform;
        _player = player;
        _player.CollisionHandler.PlayerJumpedOnPlatform += OnPlayerJumpedOnPlatform;
        Spawn2Platforms();
    }

    private void OnPlayerJumpedOnPlatform(Platform platform)
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

    private void Spawn2Platforms()
    {
        _firstPlatform = GetPlatformFromPool();
        _secondPlatform = GetPlatformFromPool();
    }

    private Platform GetPlatformFromPool()
    {
        var newPlatfrorm = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool,
            CalculatePlatformPos(_defaultPosition,
            CalculateRandomValue(Semen.Instance.MinOffsetForSpawnPlatform, Semen.Instance.MaxOffsetForSpawnPlatform), 0)) as Platform;
        _defaultPosition = newPlatfrorm.transform;
        newPlatfrorm.Init(_scoreController);
        return newPlatfrorm;
    }

    //private float CalculateOffsetForPlatformsPos()
    //{
    //    return UnityEngine.Random.Range(_minOffset, _maxOffset);
    //}

    private Vector3 CalculatePlatformPos(Transform defaultPos, float offsetX, float offsetZ)
    {
        float randomHeight = CalculateRandomValue(Semen.Instance.PlatformsMinHeight, Semen.Instance.PlatformsMaxHeight);
        return new Vector3(defaultPos.position.x + offsetX, randomHeight, defaultPos.position.z + offsetZ);
    }

    private float CalculateRandomValue(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}