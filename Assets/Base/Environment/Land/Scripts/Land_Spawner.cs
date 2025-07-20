using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Land_Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject landPrefab;
    [SerializeField] private JumpController controller;

    private Vector3 _currentPos;

    private void Start()
    {
        SpawnIsland();
    }

    private void OnEnable()
    {
        controller.OnLand += SpawnIsland;
    }

    private void OnDisable()
    {
        controller.OnLand -= SpawnIsland;
    }

    private void SpawnIsland()
    {
        _currentPos = new Vector3(_currentPos.x + Random.Range(controller.GetLength / 3, controller.GetLength), _currentPos.y, _currentPos.z);
        Instantiate(landPrefab, _currentPos, Quaternion.identity);
    }
}
