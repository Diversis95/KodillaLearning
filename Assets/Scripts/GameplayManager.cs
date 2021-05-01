﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();

    public enum EGameState
    {
        Playing,
        Paused
    }
    public EGameState m_state;

    public delegate void GameStateCallBack();

    public static event GameStateCallBack onGamePaused;
    public static event GameStateCallBack onGamePlaying;

    private HUDController HUD;
    private int points = 0;

    public EGameState GameState
    {
        get { return m_state; }
        set 
        {
            m_state = value;
            switch (m_state)
            {
                case EGameState.Paused:
                    {
                        if (onGamePaused != null)
                            onGamePaused();
                    } break;
                case EGameState.Playing:
                    {
                        if (onGamePlaying != null)
                            onGamePlaying();
                    } break;
            }
        }
    }

    public int Points
    {
        get { return points; }
        set
        {
            points = value;
            HUD.UpdatePoints(points);
        }
    }

    private void Start()
    {
        m_state = EGameState.Playing;
        GetAllRestartableObjects();

        HUD = FindObjectOfType<HUDController>();
        points = 0;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            PlayPause();

        if (Input.GetKeyUp(KeyCode.R))
            Restart();
    }

    private void GetAllRestartableObjects()
    {
        m_restartableObjects.Clear();

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            IRestartableObject[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IRestartableObject>();

            foreach (var childInterface in childrenInterfaces)
                m_restartableObjects.Add(childInterface);
        }
    }

    private void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();

        points = 0;
    }

    public void PlayPause()
    {
        switch (GameState)
        {
            case EGameState.Paused: { GameState = EGameState.Playing; } break;

            case EGameState.Playing: { GameState = EGameState.Paused; } break;
        }
    }
}
