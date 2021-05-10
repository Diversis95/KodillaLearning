using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class MenuOptionsController : MonoBehaviour
{
    public Button acceptButton;
    public Button cancelButton;

    public MainMenuController mainMenu;

    private float initialVolume = 0.0f;

    private void Start()
    {
        acceptButton.onClick.AddListener(delegate { OnAccept(); });
        cancelButton.onClick.AddListener(delegate { OnCancel(); });
    }

    private void OnEnable()
    {
        initialVolume = AudioListener.volume;
    }

    private void OnAccept()
    {
        SaveManager.Instance.SaveData.masterVolume = AudioListener.volume;
        SaveManager.Instance.SaveSettings();
        mainMenu.showOptions(false);
    }

    private void OnCancel()
    {
        AudioListener.volume = initialVolume;
        mainMenu.showOptions(false);
    }
}
