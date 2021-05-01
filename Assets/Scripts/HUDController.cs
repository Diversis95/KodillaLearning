using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public Button pauseButton;
    public TMPro.TextMeshProUGUI pointsText;

    private void Start()
    {
        pauseButton.onClick.AddListener(delegate {
            GameplayManager.Instance.PlayPause();
        });
    }

    public void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points;
    }
}
