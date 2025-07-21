using UnityEngine;

public class Semen : MonoBehaviour
{
    [Header("Jump")]
    public int MinJumpPower = 3;
    public int MaxJumpPower = 13;
    public float JumpHeight = 3; // ������ ������ ���� ���� ��� ������������ ������ ���������, ����� ����� ����� ����� ��� ������
    public float JumpDuration = 1;
    public float _pauseBetweenIncreasingJumpPower = 0.2f;

    [Header("Platform")]
    public float PlatformsMaxHeight = 6;
    public float PlatformsMinHeight = 0;
    public float MinOffsetForSpawnPlatform = 6;
    public float MaxOffsetForSpawnPlatform = 9;
    // ���������� ������� ��������� (5, 3, 5);


    public static Semen Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
