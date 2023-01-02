using UnityEngine;

public class SkinLoader : SkinVariables
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedskin"))
        {
            selectedSkin = 0;
        }
        else
        {
            LoadSkin();
        }
        UpdateSkin(selectedSkin);
    }
    
    public void UpdateSkin(int skinNum)
    {
        Sprite ballSprite = ballDatabase.GetBall(skinNum);
        playerSprite.sprite = ballSprite;
    }

    public void LoadSkin()
    {
        selectedSkin = PlayerPrefs.GetInt("selectedskin");
    }
}
