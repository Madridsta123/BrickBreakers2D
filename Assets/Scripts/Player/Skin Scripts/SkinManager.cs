using UnityEngine;
using UnityEngine.UI;

public class SkinManager : SkinVariables
{
    [Header("Button References")]
    public Button nextButton;
    public Button previousButton;
    public Button selectButton;

    private const string sceneName = "LevelScene";
    private const string playerPrefsSkin = "selectedskin";

    private void Start()
    {
        CheckPlayerPrefs();
        SetListener();
    }

    private void CheckPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(playerPrefsSkin))
        {
            selectedSkin = 0;
        }
        else
        {
            LoadSkin();
        }
    }

    private void SetListener()
    {
        nextButton.onClick.AddListener(NextSkinOption);
        previousButton.onClick.AddListener(PreviousSkinOption);

        selectButton.onClick.AddListener(LoadLevelScene);
    }

    private void NextSkinOption()
    {
        selectedSkin++;
        if (selectedSkin >= ballDatabase.ballCount)
        {
            selectedSkin = 0;
        }
        else
        {
            Debug.Log("ballCount > selectedSkin");
        }
        UpdateSkin(selectedSkin);
    }

    private void PreviousSkinOption()
    {
        selectedSkin--;
        if (selectedSkin < 0)
        {
            selectedSkin = ballDatabase.ballCount - 1;
        }
        else
        {
            Debug.Log("selectedSkin = 0");
        }
        UpdateSkin(selectedSkin);
    }

    private void UpdateSkin(int skinNum)
    {
        Sprite ball = ballDatabase.GetBall(skinNum);
        playerSprite.sprite = ball;
    }

    private void LoadSkin()
    {
        selectedSkin = PlayerPrefs.GetInt(playerPrefsSkin);
    }

    public void LoadLevelScene()
    {
        PlayerPrefs.SetInt(playerPrefsSkin, selectedSkin);
        SceneChanger.sceneInstance.LoadScene(sceneName);
    }
}
