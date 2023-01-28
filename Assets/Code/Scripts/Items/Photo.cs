using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour, IInteractable
{
    public GameObject photo;

    public void Interact(Interactor interactor)
    {
        interactor.PickPhoto(photo);
    }
}
