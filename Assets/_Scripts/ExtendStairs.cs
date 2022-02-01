using UnityEngine;

public class ExtendStairs : MonoBehaviour, IInteractable
{
    void Extend()
    {
        transform.localScale = new Vector3(1, 1, 5);
    }

    public void Interact()
    {
        Extend();
    }
}
