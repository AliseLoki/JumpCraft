using System;
using UnityEngine;

public class PlatformsController : MonoBehaviour
{
    private Platform _firstPlatform;
    private Platform _currentPlatform;
    private Platform _previousPlatform;
    private Platform _platformToDeactivate;

    private ObjectsPool _objectsPool;

    [SerializeField] private float _minOffset = 5;
    [SerializeField] private float _maxOffset = 8;

    private Vector3 _centerBetweenCurrentAndPreviousPlatform;

    private PlatformsScoreController _scoreController;

    public event Action<bool> PlatformHasSpawnedOnAxisX;

    public PlatformsScoreController ScoreController => _scoreController;

    public Vector3 Center => _centerBetweenCurrentAndPreviousPlatform;

    private void OnDisable()
    {
        _currentPlatform.PlayerLandedOnPlatform -= OnPlayerHasLandedOnPlatform;
    }

    public void Init(ObjectsPool pool)
    {
        _scoreController = new PlatformsScoreController();
        _objectsPool = pool;
        _firstPlatform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool) as Platform;
        _firstPlatform.transform.position = new Vector3(0, 0, 0);
        _firstPlatform.gameObject.SetActive(true);
        _currentPlatform = _firstPlatform;
        TransformPlatformOnAxis(true, CalculateOffsetForPlatformsPosition(), 0);
    }

    private void OnPlayerHasLandedOnPlatform(int bonusScore)
    {
        _scoreController.OnScoreChanged(bonusScore);

        var random = UnityEngine.Random.Range(0, 2);

        if (random == 0) TransformPlatformOnAxis(true, CalculateOffsetForPlatformsPosition(), 0);
        else TransformPlatformOnAxis(false, 0, CalculateOffsetForPlatformsPosition());
    }

    private void TransformPlatformOnAxis(bool isAxisX, float x, float z)
    {
        Platform platform = TakePlatformFromPool(x, z);
        InitPlatforms(platform);
        _centerBetweenCurrentAndPreviousPlatform = CalculateDistanceBetweenPlatforms();
        PlatformHasSpawnedOnAxisX?.Invoke(isAxisX);
    }

    private Platform TakePlatformFromPool(float offsetX, float offsetZ)
    {
        Platform platform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool) as Platform;
        platform.transform.position = CalculatePlatformPosition(offsetX, offsetZ);
        platform.gameObject.SetActive(true);
        return platform;
    }

    private void InitPlatforms(Platform platform)
    {
        if (_platformToDeactivate != null) _platformToDeactivate.gameObject.SetActive(false);
        _platformToDeactivate = _previousPlatform;
        _previousPlatform = _currentPlatform;
        _currentPlatform.PlayerLandedOnPlatform -= OnPlayerHasLandedOnPlatform;
        _currentPlatform = platform;
        _currentPlatform.PlayerLandedOnPlatform += OnPlayerHasLandedOnPlatform;
    }

    private Vector3 CalculatePlatformPosition(float offsetX, float offsetZ)
    {
        // пока берем позицию по у предыдущей платформы
        return new Vector3(_currentPlatform.transform.position.x + offsetX,
           _currentPlatform.transform.position.y, _currentPlatform.transform.position.z + offsetZ);
    }

    private Vector3 CalculateDistanceBetweenPlatforms()
    {
        return (_currentPlatform.transform.position + _previousPlatform.transform.position) / 2;
    }

    private float CalculateOffsetForPlatformsPosition()
    {
        return UnityEngine.Random.Range(_minOffset, _maxOffset);
    }
}