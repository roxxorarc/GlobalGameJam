using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRange = 0.5f;
    [SerializeField] private LayerMask interactionMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int colliderCount;

    private void Update()
    {

        colliderCount = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRange, colliders, interactionMask);

        if (colliderCount > 0)
        {

            if (colliders[0].TryGetComponent(out IInteractable interactable))
            {
                if (Input.GetButtonDown("Fire1")) // changer le nom du bouton.  Fire1 = E (ctrl gauche par défaut)
                {
                    interactable.Interact(this);
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }

    public void Hide()
    {
        Debug.Log("Hide");
    }

    public void SetCheckpoint(Vector3 position)
    {
        Debug.Log("Checkpoint" + position);
    }

    public void PickPhoto(GameObject photo)
    {
        Debug.Log("Photo" + photo);
    }
}
