using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField] private GameObject player;

    public enum GameState
    {
        Playing,
        Paused,
        Spotted,
        EndGame
    }

    [SerializeField]
    private GameObject lastCheckPoint;

    [SerializeField]
    private List<PhotoScriptableObject> inventory = new List<PhotoScriptableObject>();

    public GameState m_BaseState = GameState.Playing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Start()
    {
        SwitchState(GameState.Playing);
    }

    private void Update()
    {

        UpdateState();

    }

    public void SwitchState(GameState NewState)
    {
        m_BaseState = NewState;
    }

    public void UpdateState()
    {


        switch (m_BaseState)
        {
            case GameState.Playing:
                if (!player.GetComponent<Interactor>().isHidden)
                player.GetComponent<PlayerMovement>().enabled = true;
                

                
                CanvasManager.s_Instance.SetPauseMenu(!enabled);
                Time.timeScale = 1f;
                if (Input.GetButtonDown("Start") || Input.GetKey(KeyCode.Escape))
                {
                    SwitchState(GameState.Paused);
                }
                break;
            case GameState.Paused:
                Pause();

                break;
            case GameState.Spotted:
                PlayerSpotted();
                break;
            case GameState.EndGame:
                CanvasManager.s_Instance.SetEnding(enabled);
                CanvasManager.s_Instance.SetHUDMenu(!enabled);
                Time.timeScale = 0;
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(m_BaseState), m_BaseState, null);
        }
    }



    private void PlayerSpotted()
    {
        Debug.Log("Player Spotted");
        player.GetComponent<PlayerMovement>().enabled = false;
        RespawnPlayer();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        CanvasManager.s_Instance.SetPauseMenu(enabled);
        Debug.Log("Game Paused");
        if (Input.GetButtonDown("Start"))
        {
            SwitchState(GameState.Playing);
        }
    }

    private void RespawnPlayer()
    {

        player.transform.position = lastCheckPoint.transform.position;
      //  player.GetComponent<PlayerMovement>().speed = 0;
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);
        StartCoroutine(wait);
        SwitchState(GameState.Playing);

    }

    public void SetLastCheckPoint(Vector3 checkPoint)
    {
        lastCheckPoint.transform.position = checkPoint;
    }

    public void AddPhoto(PhotoScriptableObject photo)
    {
        inventory.Add(photo);
    }

    public List<PhotoScriptableObject> GetInventory()
    {
        return inventory;
    }
}
