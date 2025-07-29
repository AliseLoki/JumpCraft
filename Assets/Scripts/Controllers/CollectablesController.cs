using UnityEngine;

public class CollectablesController
{
    private float _diamondPosYOffset = 7;
    private float _heartsOffset = 2;
    private float _pigOffset = 2.5f;

    private ObjectsPool _pool;

    public CollectablesController(ObjectsPool pool)
    {
        _pool = pool;
    }

    public void SpawnPig(Vector3 pos)
    {
        var newPig = _pool.GetPooledObject(_pool.Pigs, _pool.Pig, new Vector3(pos.x, pos.y + _pigOffset, pos.z));
        newPig.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void SpawnCoin(Vector3 pos)
    {
        var newCoin = _pool.GetPooledObject(_pool.Coins, _pool.Coin, new Vector3(pos.x, pos.y + _diamondPosYOffset, pos.z));
    }
    public void SpawnHeart(Vector3 pos)
    {
        var newHeart = _pool.GetPooledObject(_pool.Hearts, _pool.Heart, new Vector3(pos.x, pos.y + _heartsOffset, pos.z));
    }

    public void SpawnDiamond(Vector3 pos)
    {
        var newDiamond = _pool.GetPooledObject(_pool.Diamonds, _pool.Diamond, new Vector3(pos.x, pos.y + _diamondPosYOffset, pos.z));
    }
}