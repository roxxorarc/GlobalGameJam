using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PhotoScriptableObject photo;
    public void Interact(Interactor interactor)
    {
        interactor.PickPhoto(photo);
        Destroy(gameObject);
    }
}
