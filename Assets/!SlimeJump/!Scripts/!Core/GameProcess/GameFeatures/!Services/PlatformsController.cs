using UnityEngine;

public class PlatformsController
{
    private Platform _firstPlatform;
    private Platform _secondPlatform;
    private Platform _defaultPlatform;
    private Platform _currentPlatform;

    private int _spawnedPlatformsAmount;

    private float _minOffset = 6;
    private float _maxOffset = 9;

    private float _minHeightOffset = 0;
    private float _maxHeightOffset = 6;

    private Transform _defaultPosition;
    private ObjectsPool _objectsPool;

    public Platform FirstPlatform => _firstPlatform;
    public Platform SecondPlatform => _secondPlatform;

    public int PassedPlatformsAmount => _spawnedPlatformsAmount; 

    public PlatformsController(Transform transform, ObjectsPool pool)
    {
        _defaultPosition = transform;
        _objectsPool = pool;
    }

    public void InitPlatforms(Platform platform)
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

    public void SpawnFirstPlatforms()
    {
        _currentPlatform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.Platform, new Vector3(0, 3.1f, 0)) as Platform;
        Spawn2Platforms();
    }

    public Vector3 CalculateCenterBetweenPlatforms()
    {
        return (_firstPlatform.transform.position + _secondPlatform.transform.position) / 2;
    }

    public void Spawn2Platforms()
    {
        _firstPlatform = GetPlatformFromPool();
        _secondPlatform = GetPlatformFromPool();
    }

    private void AddModule()
    {
        // модуль счета
        // модуль батута: красного или зеленого
        // модуль свинки
        // пустая платформа
    }

    private Platform GetPlatformFromPool()
    {
        var newPlatfrorm = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.Platform,
            CalculatePlatformPos(_defaultPosition,
            CalculateRandomValue(_minOffset, _maxOffset), 0)) as Platform;
        _defaultPosition = newPlatfrorm.transform;
        _spawnedPlatformsAmount++;
        return newPlatfrorm;
    }

    private Vector3 CalculatePlatformPos(Transform defaultPos, float offsetX, float offsetZ)
    {
        float randomHeight = CalculateRandomValue(_minHeightOffset, _maxHeightOffset);
        return new Vector3(defaultPos.position.x + offsetX, randomHeight, defaultPos.position.z + offsetZ);
    }

    private float CalculateRandomValue(float min, float max)
    {
        return Random.Range(min, max);
    }
}
