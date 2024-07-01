using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcherV2 : MonoBehaviour
{
    public bool CanE = false; // ������� ����������, ������� ����� �������� � ����������
    public float delayNextScene = 2f; // �������� ����� ������������� �� ��������� �����
    public float delayPreviousScene = 2f; // �������� ����� ������������� �� ���������� �����
    public GameObject objectToActivate; // ������, ������� ����� ������������ ��� ������� E

    void Update()
    {
        // ���������, ������ �� ������� E � �������� �� ������� ���������� CanE
        if (CanE && Input.GetKeyDown(KeyCode.E))
        {
            // ���������� �������� ������
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            // �������� �������� ��� ������������ �� ��������� ����� � ���������
            StartCoroutine(SwitchSceneWithDelay(true));
        }

        // ���������, ������ �� ������� Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �������� �������� ��� ������������ �� ���������� ����� � ���������
            StartCoroutine(SwitchSceneWithDelay(false));
        }
    }

    // �������� ��� ������������ ����� � ���������
    IEnumerator SwitchSceneWithDelay(bool next)
    {
        // ���������� �������� � ����������� �� ����������� ������������
        float delay = next ? delayNextScene : delayPreviousScene;

        // ���� ��������� �����
        yield return new WaitForSeconds(delay);

        // �������� ������ ������� �����
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ���������� ������ ����� ����� � ����������� �� ����������� ������������
        int newSceneIndex = next ? currentSceneIndex + 1 : currentSceneIndex - 1;

        // ���������, ��� ������ ����� ����� ��������� � ���������� ���������
        if (newSceneIndex >= 0 && newSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // ������������� �� ����� �����
            SceneManager.LoadScene(newSceneIndex);
        }
    }
}
