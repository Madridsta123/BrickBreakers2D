using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger sceneInstance { get; private set; }

    [Header("Quit Button Settings")]
    public GameObject quitPanel;
    public Button quitButton;
    public Button yesButton;
    public Button noButton;

    private void Awake()
    {
        if (sceneInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        sceneInstance = this;
        quitPanel.SetActive(false);

        QuitManager();
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitManager()
    {
        quitButton.onClick.AddListener(() => quitPanel.SetActive(true));

        noButton.onClick.AddListener(() => quitPanel.SetActive(false));
        yesButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
