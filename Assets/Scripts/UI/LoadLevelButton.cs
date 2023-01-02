using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class LoadLevelButton : MonoBehaviour
{
    public string sceneName;

    [SerializeField] 
    private Button myButton;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        myButton.onClick.AddListener(ButtonAction);
    }

    private void OnDisable()
    {
        myButton.onClick.RemoveListener(ButtonAction);
    }

    private void ButtonAction()
    {
        SceneChanger.sceneInstance.LoadScene(sceneName);
    }
}
