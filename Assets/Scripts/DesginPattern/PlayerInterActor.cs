using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInterActor : MonoBehaviour
{
    [SerializeField] Transform point;
    [SerializeField] float range;

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(point.position, range);
        foreach(Collider collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }

    void OnInteract(InputValue inputValue)
    {
        Interact();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(point.position, range);
    }
}
