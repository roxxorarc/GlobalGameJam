using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGame : MonoBehaviour
{
    GameManager gm;

    List<PhotoScriptableObject> inventory;

    [SerializeField]
    private Image[] images;

    [SerializeField]
    private TextMeshPro text;

    private string winText = "You have found all the photos of your family. You can now go back to your room and sleep. You will be able to see your family again tomorrow. Good night.";

    private string loseText;

    void Start()
    {
        gm = GameManager.instance;

    }

    void displayEndGame()
    {
        inventory = gm.GetInventory();
        for (int i = 0; i < inventory.Count; i++)
        {
            switch (inventory[i].name)
            {
                case "GrandFather":
                    images[0].sprite = inventory[i].photoSprite;
                    break;
                case "GrandMother":
                    images[1].sprite = inventory[i].photoSprite;
                    break;
                case "GrandMother2":
                    images[2].sprite = inventory[i].photoSprite;
                    break;
                case "GrandFather2":
                    images[3].sprite = inventory[i].photoSprite;
                    break;
                case "Father":
                    images[4].sprite = inventory[i].photoSprite;
                    break;
                case "Mother":
                    images[5].sprite = inventory[i].photoSprite;
                    break;
            }

        }

        text.text = inventory.Count == 6 ? winText : loseText;
    }




}
