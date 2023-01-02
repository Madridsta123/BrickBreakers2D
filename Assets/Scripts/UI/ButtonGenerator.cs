using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject stageButtonPrefab;
    public GameObject buttonHolder;

    public const string jsonName = "*.json";
    public const string sceneName = "GameScene";
    public const int addStageNum = 1;

    //private string path;
    private Object[] jsonFileName;
    //private List<string> jsonFileName = new();
    private GameObject instantiatedButton;
    private int stageNum;

    private void Awake()
    {
        GetJsonFiles();
        GenerateButtons();
    }

    private void GetJsonFiles()
    {
        //path = Application.streamingAssetsPath + "/StageFiles";
        //jsonFileName = Directory.GetFiles(path, jsonName);

        jsonFileName = Resources.LoadAll("StageFiles", typeof(TextAsset));
    }

    private void GenerateButtons()
    {
        for(int i=0; i< jsonFileName.Length; i++)
        {
            stageNum = i + addStageNum;

            instantiatedButton = Instantiate(stageButtonPrefab, buttonHolder.transform.position, Quaternion.identity);
            instantiatedButton.transform.parent = buttonHolder.transform;

            instantiatedButton.GetComponentInChildren<TextMeshProUGUI>().text = stageNum.ToString();
            instantiatedButton.name = stageNum.ToString();

            int btnNum = stageNum;
            instantiatedButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                PlayerPrefs.SetString("stageNumber", btnNum.ToString());
                LoadGameScene();
            });
        }
    }

    private void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
