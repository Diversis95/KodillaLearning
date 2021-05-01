using System.Collections.Generic;
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

    private void Start()
    {
        m_state = EGameState.Playing;
        GetAllRestartableObjects();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            switch (GameState)
            {
                case EGameState.Paused:
                    {
                        GameState = EGameState.Playing;
                    } break;

                case EGameState.Playing:
                    {
                        GameState = EGameState.Paused;
                    } break;
            }
        }

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
    }
}
