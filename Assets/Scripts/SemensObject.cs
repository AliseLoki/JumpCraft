using UnityEngine;

public class SemensObject : MonoBehaviour
{
    [Header("Jump")]
    public int MinJumpPower = 3;
    public int MaxJumpPower = 13;
    public float JumpHeight = 7; // всегда должна быть выше чем максимальная высота платформы, иначе игрок будет через нее падать
    public float JumpDuration = 1;
    public float _pauseBetweenIncreasingJumpPower = 0.2f;

    [Header("Platform")]
    public float PlatformsMaxHeight = 6;
    public float PlatformsMinHeight = 0;
    public float MinOffsetForSpawnPlatform = 5;
    public float MaxOffsetForSpawnPlatform = 8;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           
        }
    }
}
