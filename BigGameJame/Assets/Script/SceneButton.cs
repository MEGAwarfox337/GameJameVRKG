using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public int sceneIndex; // ������ ����� ��� ��������
    public Button button;  // ������, �� ������� ������� ���� ������

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
