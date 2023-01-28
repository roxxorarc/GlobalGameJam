using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour, IInteractable
{
    public void Interact(Interactor interactor)
    {
        interactor.Hide();
    }

}
