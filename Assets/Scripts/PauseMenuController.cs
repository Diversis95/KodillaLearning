using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button restartButton;
    public Button resumeButton;
    public Button quitButton;
    public GameObject panel;
    public GameObject exitPanel;
    public Button yesButton;
    public Button noButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener( delegate { OnResume(); });
        quitButton.onClick.AddListener(delegate { OnQuit(); });
        restartButton.onClick.AddListener(delegate
        { 
            GameplayManager.Instance.Restart();
            OnResume();
        });
        yesButton.onClick.AddListener(delegate { OnYesQuit(); });
        noButton.onClick.AddListener(delegate { OnNoQuit(); });

        SetPanelVisible(false);
        SetQuitPanelVisible(false);

        GameplayManager.onGamePaused += OnPause;
    }

    public void SetPanelVisible(bool visible)
    {
        panel.SetActive(visible);
    }

    public void SetQuitPanelVisible(bool visible)
    {
        exitPanel.SetActive(visible);
    }

    private void OnPause()
    {
        SetPanelVisible(true);
    }
    private void OnResume()
    {
        GameplayManager.Instance.GameState = GameplayManager.EGameState.Playing;
        SetPanelVisible(false);
    }

    private void OnQuit()
    {
        SetPanelVisible(false);
        SetQuitPanelVisible(true);
    }

    private void OnYesQuit()
    {
        Application.Quit();
    }

    private void OnNoQuit()
    {
        SetPanelVisible(true);
        SetQuitPanelVisible(false);
    }
}
