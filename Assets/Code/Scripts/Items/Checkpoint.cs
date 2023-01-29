using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 checkpoint;

    private void Awake()
    {
        checkpoint = this.transform.position;
    }


    public void Interact(Interactor interactor)
    {
        interactor.SetCheckpoint(checkpoint);
    }


}
