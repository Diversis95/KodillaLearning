using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public Button pauseButton;
    public TMPro.TextMeshProUGUI pointsText;
    public Button restartButton;

    private void Start()
    {
        pauseButton.onClick.AddListener(delegate {
            GameplayManager.Instance.PlayPause();
        });

        restartButton.onClick.AddListener(delegate {
            GameplayManager.Instance.Restart();
        });
    }

    public void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points;
    }
}
