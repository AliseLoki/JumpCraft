using UnityEngine;

public class CollectablesController : MonoBehaviour
{
    [SerializeField] private float _collectablesPosYOffset = 7;

    private ObjectsPool _pool;
    private PlatformsController _platformsController;

    private void OnDisable()
    {
        _platformsController.CenterChanged -= OnCenterChanged;
    }

    public void Init(PlatformsController platformsController, ObjectsPool objectsPool)
    {
        _platformsController = platformsController;
        _pool = objectsPool;
    }

    public void StartGame()
    {
        _platformsController.CenterChanged += OnCenterChanged;
    }

    private void OnCenterChanged(Vector3 pos)
    {
        int chance = Random.Range(0, Semen.Instance.DiamondSpawnChance);
       
        if (chance == 0)
        {
            var newDiamond = _pool.GetPooledObject(_pool.Coins, _pool.CoinToPool, new Vector3(pos.x, pos.y + _collectablesPosYOffset, pos.z));
        }
    }
}