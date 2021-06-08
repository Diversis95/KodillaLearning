using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Button volumeUp;
    public Button volumeDown;
    public Image bar;

    void Start()
    {
        volumeUp.onClick.AddListener(delegate { OnChangeVolume(true); });
        volumeDown.onClick.AddListener(delegate { OnChangeVolume(false); });
    }

    private void UpdateBar()
    {
        bar.fillAmount = AudioListener.volume;
    }

    private void OnEnable()
    {
        UpdateBar();
    }

    private void OnChangeVolume(bool bUp)
    {
        float newValue = AudioListener.volume;

        if(bUp)
        {
            newValue += 0.1f;
        }
        else
        {
            newValue -= 0.1f;
        }

        newValue = Mathf.Clamp01(newValue);
        AudioListener.volume = newValue;
        UpdateBar();
    }
}
