using UnityEngine;

public class GameController : MonoBehaviour
{
    // раздает задания другим контроллерам при приземлении игрока на платформу

    private Player _player;
    private CollectablesController _collectablesController;
    private PlatformsController _platformsController;

    private ObjectsPool _objectsPool;

    [SerializeField] private Transform _defaultPosition;

    private ScoreController _scoreController;
    public ScoreController ScoreController => _scoreController;

    private void OnDisable()
    {
        _player.CollisionHandler.PlayerJumpedOnPlatform -= OnPlayerJumpedOnPlatform;
    }

    public void Init(ObjectsPool pool, Player player, CollectablesController collectablesController)
    {
        _platformsController = new PlatformsController();

        _objectsPool = pool;
        _platformsController.InitDefaultPosition(transform,_objectsPool);
        _collectablesController = collectablesController;
        _scoreController = new ScoreController();
        _player = player;
        _player.CollisionHandler.PlayerJumpedOnPlatform += OnPlayerJumpedOnPlatform;

        _platformsController.SpawnFirstPlatforms();
    }

    private void OnPlayerJumpedOnPlatform(Platform platform)
    {

        // именно здесь решается какой модуль включить для следующих платформ, и проверяется какой модуль активен

        _platformsController.InitPlatforms(platform);        
        _scoreController.OnScoreChanged(platform.GetBonus(_player));
        var posCenter = _platformsController.CalculateCenterBetweenPlatforms();

        // переключать модули поатформы
        // спаунить коллектэблы в зависимости от шанса

        if (platform.Trampoline.gameObject.activeSelf)
        {
            if (platform.IsGreen) _player.JumpHandler.TrampolineJump(_platformsController.FirstPlatform);
            else _player.JumpHandler.RedTrampolineJump(_platformsController.FirstPlatform);
        }



        //if (!_secondPlatform.Trampoline.gameObject.activeSelf)
        //{
        //    int chance = Random.Range(0, 2);

        //    if (chance == 0) _collectablesController.SpawnPig(_secondPlatform.transform.position);
        //    else _collectablesController.SpawnHeart(_secondPlatform.transform.position);
        //}


        //    int chance1 = Random.Range(0, 2);

        //if (chance1 == 0) _collectablesController.SpawnDiamond(CalculateCenterBetweenPlatforms(_firstPlatform, _secondPlatform));
        //else _collectablesController.SpawnCoin(CalculateCenterBetweenPlatforms(_firstPlatform, _secondPlatform));
    }
}