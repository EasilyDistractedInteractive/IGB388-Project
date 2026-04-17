using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    Scene scene;
    public void nextScene()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
