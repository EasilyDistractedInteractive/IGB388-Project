using System.Collections.Generic;
using UnityEngine;

public class GlobalInstanceManager : MonoBehaviour
{
    public static GlobalInstanceManager Instance;

    public Dictionary<string, int> playerScores; //Not in use, could be used for a local leaderboard if we want, we'll just need to add some form of name handling
    public List<int> playerScoresList;
    public List<int> ordersCompleted;

    public bool replay;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes

            // Bump up the framerate
            Application.targetFrameRate = 72;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
}