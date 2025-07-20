using UnityEngine;

public class Diamond : Interactable
{
    [SerializeField] private Vector3 _rotateOffset = new Vector3(0, 45, 0);

    void Update()
    {
        Rotate(_rotateOffset);
    }

    private void Rotate(Vector3 vector3)
    {
        transform.Rotate(vector3 * Time.deltaTime);
    }
}
