using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject player;

    [Header("Button Reference")]
    public Button pauseButton;
    public Button resumeButton;

    private void Start()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        player.GetComponent<PlayerManager>().enabled = true;
        player.GetComponent<AimManager>().enabled = true;

        SetListener();
    }

    private void SetListener()
    {
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        player.GetComponent<PlayerManager>().enabled = false;
        player.GetComponent<AimManager>().enabled = false;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        player.GetComponent<PlayerManager>().enabled = true;
        player.GetComponent<AimManager>().enabled = true;
    }
}
