using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerformanceReview : MonoBehaviour
{
    public TMP_Text orderCountText;
    public TMP_Text scoreText;

    private void Awake()
    {
        orderCountText.text = $"Orders Completed: {GlobalInstanceManager.Instance.ordersCompleted[^1]}";
        scoreText.text = $"Score: {GlobalInstanceManager.Instance.playerScoresList[^1]}";
    }
}
