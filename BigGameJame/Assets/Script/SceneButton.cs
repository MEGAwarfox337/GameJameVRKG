using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public int sceneIndex; // »ндекс сцены дл€ перехода
    public Button button;  //  нопка, на которую повесим этот скрипт

    void Start()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
