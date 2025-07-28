using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableName _name;

    public CollectableName Name =>_name;
}
