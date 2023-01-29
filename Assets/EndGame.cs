using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    GameManager gm;

    List<PhotoScriptableObject> inventory;

    [SerializeField]
    private Image[] images;


    void Start()
    {
        gm = GameManager.instance;
        inventory = gm.GetInventory();
        displayEndGame();
    }

    void displayEndGame()
    {
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
    }




}
