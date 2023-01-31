using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    GameManager gm;

    CanvasManager cm;

    List<PhotoScriptableObject> inventory;

    [SerializeField]
    private Image[] images;

    public GameObject winText; //= "You finally found what you really wanted. All the important souvenirs about your family. Even if they are not with you, with those photos, you will easily remember them.";
    public GameObject LoseText; 

  //  public  string loseText = "You lost informations about your family, try again and collect all the pictures.";

    void Start()
    {
        gm = GameManager.instance;
        cm = CanvasManager.s_Instance;

    }

    private void Update()
    {
        inventory = gm.GetInventory();
        
        Debug.Log(inventory.Count);
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

        //inventory.Count == 6 ? winText : LoseText;


        //text.text = inventory.Count == 6 ? winText : LoseText;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory = gm.GetInventory();
            cm.SetEnding(true);
            displayEndGame();
            Time.timeScale = 0;
            if (inventory.Count == 6)
            {
                winText.SetActive(true);
                Debug.Log("win");
            }
            else if (inventory.Count < 6)
            {
                LoseText.SetActive(true);
            }
        }


    }


}
