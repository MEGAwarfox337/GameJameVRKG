using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int targetScore = 10; // �������� ���������� ��� ��������� �������
    public GameObject objectToActivate; // ������ ��� ���������

    private int currentScore = 0; // ������� �������� ��������

    // ����� ��� ���������� �������� �� 1
    public void IncrementScore()
    {
        currentScore++;
        CheckScore();
        Debug.Log("Current Score: " + currentScore); // ����� �������� ����� � �������
    }

    // ����� ��� ���������� �������� �� 1
    public void DecrementScore()
    {
        currentScore--;
        CheckScore();
        Debug.Log("Current Score: " + currentScore); // ����� �������� ����� � �������
    }

    // �������� �������� �������� �������� � ��������� �������, ���� �����
    private void CheckScore()
    {
        if (currentScore >= targetScore)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
        else
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(false);
            }
        }
    }
}
