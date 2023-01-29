using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject checkpoint;


    public void Interact(Interactor interactor)
    {
        interactor.SetCheckpoint(checkpoint.transform.position);
    }



}
