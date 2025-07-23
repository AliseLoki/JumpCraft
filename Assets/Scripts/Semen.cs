using UnityEngine;

public class Semen : MonoBehaviour
{
    [Header("Jump")]
   // public int MinJumpPower = 3;
   // public int MaxJumpPower = 13;
   // public float JumpHeight = 3; // всегда должна быть выше чем максимальная высота платформы, иначе игрок будет через нее падать
   //public float JumpDuration = 1;
    //public float _pauseBetweenIncreasingJumpPower = 0.2f;

    [Header("Platform")]
    //public float PlatformsMaxHeight = 6;
    //public float PlatformsMinHeight = 0;
    //public float MinOffsetForSpawnPlatform = 6;
   // public float MaxOffsetForSpawnPlatform = 9;
    // физические границы платформы (5, 3, 5);

    [Header("ProbabilityOfEvent")]
    public int TrampolineSpawnChance = 3;
    public int ColorTrampolineSpawnChance = 2;
    public int DiamondSpawnChance = 3;
    public int RedTrampolineSuccessfullJump = 10;


    public static Semen Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
