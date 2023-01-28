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
    public bool isHidden = false;

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
        var player = GameObject.FindWithTag("Player");
        var playerMovement = player.GetComponent<PlayerMovement>();
        player.GetComponent<MeshRenderer>().enabled = !player.GetComponent<MeshRenderer>().enabled;
        playerMovement.enabled = !playerMovement.enabled;
        isHidden = !isHidden;

    }

    public void SetCheckpoint(Vector3 position)
    {
        Debug.Log("Checkpoint" + position);
        GameManager.instance.SetLastCheckPoint(position);
    }

    public void PickPhoto(GameObject photo)
    {
        Debug.Log("Photo" + photo);
    }
}
