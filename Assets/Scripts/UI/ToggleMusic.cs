using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{
    [Header("Sprites Setting")]
    [SerializeField]
    private Sprite musicOnSprite;
    [SerializeField]
    private Sprite musicOffSprite;

    [Header("References")]
    public Button musicButton;
    [SerializeField]
    private Image targetButton;

    private const string bgMusicString = "bgMusicString";

    private void Awake()
    {
        CheckPlayerPrefs();
        SetListener();
    }

    private void CheckPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(bgMusicString))
        {
            PlayerPrefs.SetInt(bgMusicString, 1); // true
            targetButton.sprite = musicOnSprite;
            EventsManager.eventInstance.backgroundMusic.Play();
        }
        else
        {
            if (PlayerPrefs.GetInt(bgMusicString, 1) == 1) // true
            {
                targetButton.sprite = musicOnSprite;
                EventsManager.eventInstance.backgroundMusic.Play();
            }
            else if (PlayerPrefs.GetInt(bgMusicString, 1) == 0) // false
            {
                targetButton.sprite = musicOffSprite;
                EventsManager.eventInstance.backgroundMusic.Pause();
            }
            else
            {
                Debug.LogError("Unplanned scenario occurred");
            }
        }
    }

    private void SetListener()
    {
        musicButton.onClick.AddListener(ChangeSprites);
    }

    private void ChangeSprites()
    {
        if (PlayerPrefs.GetInt(bgMusicString, 1) == 1)
        {
            targetButton.sprite = musicOffSprite;
            EventsManager.eventInstance.backgroundMusic.Pause();
            PlayerPrefs.SetInt(bgMusicString, 0); // true
            return;
        }
        else if(PlayerPrefs.GetInt(bgMusicString, 1) == 0)
        {
            targetButton.sprite = musicOnSprite;
            EventsManager.eventInstance.backgroundMusic.Play();
            PlayerPrefs.SetInt(bgMusicString, 1); // true
        }
        else
        {
            Debug.LogError("Unplanned scenario occurred");
        }
    }
}
