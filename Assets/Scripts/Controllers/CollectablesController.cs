using UnityEngine;

public class CollectablesController
{
    private float _collectablesPosYOffset = 7;
    private float _heartsOffset = 2;
    private float _pigOffset = 2.5f;

    private ObjectsPool _pool;

    public void Init(ObjectsPool objectsPool)
    {
        _pool = objectsPool;
    }

    public void SpawnPig(Vector3 pos)
    {
        int chance = Random.Range(0, Semen.Instance.PigSpawnChance);

        if (chance == 0)
        {
            var newPig = _pool.GetPooledObject(_pool.Pigs, _pool.Pig, new Vector3(pos.x, pos.y + _pigOffset, pos.z));
            newPig.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void SpawnHeart(Vector3 pos)
    {
        int chance = Random.Range(0, Semen.Instance.HeartSpawnChance);

        if (chance == 0)
        {
            var newHeart = _pool.GetPooledObject(_pool.Hearts, _pool.Heart, new Vector3(pos.x, pos.y + _heartsOffset, pos.z));
        }
    }

    public void SpawnDiamond(Vector3 pos)
    {
        int chance = Random.Range(0, Semen.Instance.DiamondSpawnChance);

        if (chance == 0)
        {
            var newDiamond = _pool.GetPooledObject(_pool.Coins, _pool.CoinToPool, new Vector3(pos.x, pos.y + _collectablesPosYOffset, pos.z));
        }
    }
}