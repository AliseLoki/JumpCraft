using UnityEngine;

[CreateAssetMenu(fileName = "PlayerViewSO", menuName = "Scriptable Objects/PlayerViewSO")]
public class PlayerViewSO : ScriptableObject
{
    [SerializeField] private int _price;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _view;
    [SerializeField] private Sprite _background;
    [SerializeField] private PlayerView _prefab;

    public int Price =>_price;
    public string Name => _name;
    public Sprite View => _view;
    public Sprite Background => _background;
    public PlayerView Prefab => _prefab;
}
