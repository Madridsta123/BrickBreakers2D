using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    public string sceneName;

    private void Awake()
    {
        SceneManager.LoadScene(sceneName);
    }
}
