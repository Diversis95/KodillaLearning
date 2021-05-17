using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("MainMenuButtons")]
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    [Header("OptionsButtons")]
    public Button volumeUpButton;
    public Button volumeDownButton;

    [Header("MainMenuPanels")]
    public GameObject mainPanel;
    public GameObject mainMenuPanel;
    public GameObject exitPanel;

    [Header("OptionsPanels")]
    public GameObject optionsPanel;

    public static bool menuIsActive = true;

    void Start()
    {
        //---Main Menu Buttons---
        playButton.onClick.AddListener(delegate { OnPlay(); });
        optionsButton.onClick.AddListener(delegate { ShowOptions(true); });
        exitButton.onClick.AddListener(delegate { OnExit(); });

        //---Main Menu Panels Visibility---
        SetPanelVisible(true);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);

        GameplayManager.Instance.GameState = GameplayManager.EGameState.Paused;
    }

    //----------Main Menu Panel----------

    public void SetPanelVisible(bool visible)
    {
        mainPanel.SetActive(visible);
    }

    private void OnPlay()
    {
        SetPanelVisible(false);
        GameplayManager.Instance.GameState = GameplayManager.EGameState.Playing;
        menuIsActive = false;
    }

    public void ShowOptions(bool bShow)
    {
        optionsPanel.SetActive(bShow);
    }

    private void OnExit()
    {
        Application.Quit();
    }
}
