using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();

    public GameObject simpleAnimeProp;
    public GameSettingsDatabase gameDatabase;

    public bool isPlaying = false;

    public enum EGameState
    {
        Playing,
        Paused
    }
    public EGameState m_state;

    public delegate void GameStateCallBack();

    public static event GameStateCallBack onGamePaused;
    public static event GameStateCallBack onGamePlaying;
    public static event Action<int> PointsUpdated;

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
            PointsUpdated?.Invoke(points);
        }
    }

    private void Start()
    {
        m_state = EGameState.Playing;
        GetAllRestartableObjects();

        points = 0;
        GameObject.Instantiate(simpleAnimeProp, new Vector3(-4.6f, 0f, 0f), Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            PlayPause();
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

    public void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();

        points = 0;
    }

    public void PlayPause()
    {
        switch (GameState)
        {
            case EGameState.Playing: { GameState = EGameState.Paused; isPlaying = false; } break;
            case EGameState.Paused: { GameState = EGameState.Playing; isPlaying = true; } break;
        }
    }
}
