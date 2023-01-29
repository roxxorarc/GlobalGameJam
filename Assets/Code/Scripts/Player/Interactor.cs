using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRange = 0.5f;
    [SerializeField] private LayerMask interactionMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int colliderCount;
    public bool isHidden = false;
    private float currentTimeElapsed = 0f;

    private float buttonPressDuration = 1.5f;

    [SerializeField]
    private GameObject progressImage;

    [SerializeField]
    private GameObject tooltip;

    private void Update()
    {

        colliderCount = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRange, colliders, interactionMask);
        bool collidersFound = colliderCount > 0;
        DisplayTooltip(collidersFound);
        DisplayProgress(false);
        if (colliderCount > 0)
        {

            if (colliders[0].TryGetComponent(out IInteractable interactable))
            {
                if (Input.GetButtonDown("Fire1") && !(interactable is Checkpoint)) // changer le nom du bouton.  Fire1 = E (ctrl gauche par défaut)
                {
                    interactable.Interact(this);
                }
                else if (Input.GetButton("Fire1") && interactable is Checkpoint)
                {
                    IncrementProgress(interactable);
                }
                else
                {
                    currentTimeElapsed = 0f;
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

        Debug.Log("Checkpoint set to " + position);
        GameManager.instance.SetLastCheckPoint(position);
    }

    public void PickPhoto(PhotoScriptableObject photo)
    {
        Debug.Log("Photo" + photo);
        GameManager.instance.AddPhoto(photo);
    }


    private void IncrementProgress(IInteractable checkpoint)
    {
        currentTimeElapsed += Time.deltaTime;
        UpdateProgressImage();
        if (currentTimeElapsed >= buttonPressDuration)
        {
            checkpoint.Interact(this);
        }
    }

    private void UpdateProgressImage()
    {
        var progress = currentTimeElapsed / buttonPressDuration;
        if (progress > 0) progressImage.SetActive(true);
        progressImage.GetComponent<Image>().fillAmount = progress;
    }

    private void DisplayTooltip(bool collidersFound)
    {
        tooltip.SetActive(collidersFound);
    }

    private void DisplayProgress(bool active)
    {
        progressImage.SetActive(active);
    }

}
