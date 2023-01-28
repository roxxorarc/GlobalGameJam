using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PhotoScriptableObject photo;

    [SerializeField] private Image image;

    public void Start()
    {
        image.sprite = photo.photoSprite;
        var color = image.color;
        color.a = 0;
        image.color = color;
    }

    public void Interact(Interactor interactor)
    {
        interactor.PickPhoto(photo);
        var color = image.color;
        color.a = 1;
        image.color = color;
        Destroy(gameObject);
    }
}
