using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour, IInteractable
{
    private GameObject photo; // plus tard modifier gameobject en scriptableobject pour traquer les différentes photos

    private void Awake()
    {
        photo = this.gameObject;
    }

    public void Interact(Interactor interactor)
    {
        interactor.PickPhoto(photo);
    }
}
