using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _spawnChance = 15;
    private int _moduleIndex = 5;

    private Player _player;
    private CollectablesController _collectablesController;
    private PlatformsController _platformsController;
    private ObjectsPool _objectsPool;
    private ScoreController _scoreController;
    public ScoreController ScoreController => _scoreController;

    private void OnDisable()
    {
        _player.CollisionHandler.PlayerJumpedOnPlatform -= OnPlayerJumpedOnPlatform;
    }

    public void Init(ObjectsPool pool, Player player)
    {
        _objectsPool = pool;
        _platformsController = new PlatformsController(transform, _objectsPool);
        _collectablesController = new CollectablesController(_objectsPool);
        _scoreController = new ScoreController();

        _player = player;
        _player.CollisionHandler.PlayerJumpedOnPlatform += OnPlayerJumpedOnPlatform;
        _platformsController.SpawnFirstPlatforms();
    }

    private void OnPlayerJumpedOnPlatform(Platform platform)
    {
        _platformsController.InitPlatforms(platform);
        _scoreController.OnScoreChanged(platform.GetBonus(_player));
        var posCenter = _platformsController.CalculateCenterBetweenPlatforms();

        // проверяем какой прыжок должен совершить игрок
        if (platform.Trampoline.gameObject.activeSelf)
        {
            if (platform.IsGreen) _player.JumpHandler.TrampolineJump(_platformsController.FirstPlatform);
            else _player.JumpHandler.RedTrampolineJump(_platformsController.FirstPlatform);
        }


        // проверяем какой модуль подключить
        if (_platformsController.SecondPlatform.IsEmpty) ChooseModule(_platformsController.SecondPlatform);

        ChooseCollectableToSpawn(posCenter);
    }

    private void ChooseCollectableToSpawn(Vector3 posCenter)
    {
        if (_platformsController.PassedPlatformsAmount >= _moduleIndex)
        {

            int chance = Random.Range(0, _spawnChance);

            if (chance < 2) _collectablesController.SpawnDiamond(posCenter);
            else if (chance == 2) _collectablesController.SpawnCoin(posCenter);

            _moduleIndex = 0;
        }
    }

    private void ChooseModule(Platform platform)
    {
        if (_platformsController.PassedPlatformsAmount >= _moduleIndex)
        {
            int chance = Random.Range(0, _spawnChance);

            if (chance < 3) platform.SetTrampolineActive();
            else if (chance == 3) _collectablesController.SpawnPig(_platformsController.SecondPlatform.transform.position);
            else if (chance == 4) _collectablesController.SpawnHeart(_platformsController.SecondPlatform.transform.position);
            platform.SetEmpty(false);
            _moduleIndex = 0;
        }
    }
}