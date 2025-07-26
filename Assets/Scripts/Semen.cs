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
    public int TrampolineSpawnChance = 5;
    public int ColorTrampolineSpawnChance = 2;
    public int DiamondSpawnChance = 3;
    public int HeartSpawnChance = 3; // но шанс получается меньше так как платформ контроллер проверяет есть ли батут и только тогда спаунит
    public int RedTrampolineSuccessfullJump = 3;
    public int PigSpawnChance = 2;

    public static Semen Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
