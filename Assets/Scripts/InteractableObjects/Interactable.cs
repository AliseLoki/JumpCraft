using UnityEngine;

public abstract class Interactable : MonoBehaviour 
{
    private void OnBecameInvisible()
    {
        //не работает
        this.gameObject.SetActive(false);
        Debug.Log("WORK");
    }
}
