using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] List<GameObject> _interactableGOs;
    
    public void Interact()
    {
        foreach (GameObject interactableGO in _interactableGOs)
        {
            var interactable = interactableGO.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }
}
