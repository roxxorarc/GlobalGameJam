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
        MainMenu,
        Playing,
        Paused,
        Spotted
    }

    [SerializeField]
    private Vector3 lastCheckPoint;

    [SerializeField]
    private static GameState state;


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

    private void Update()
    {


    }

    void Start()
    {
        SetState(GameState.MainMenu);
    }

    public void SetState(GameState newState)
    {

        state = newState;

        switch (state)
        {
            case GameState.MainMenu:
                LoadMainMenu();
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                Pause();
                break;
            case GameState.Spotted:
                PlayerSpotted();
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }



    private void PlayerSpotted()
    {
        Debug.Log("Player Spotted");
        RespawnPlayer();
    }

    private void Pause()
    {
        Debug.Log("Game Paused");
    }

    private void LoadMainMenu()
    {
        Debug.Log("Main Menu Loaded");
    }


    private void RespawnPlayer()
    {
        player.transform.position = lastCheckPoint;
    }

    public void SetLastCheckPoint(Vector3 checkPoint)
    {
        lastCheckPoint = checkPoint;
    }
}
