using UnityEngine;

public class ScoreSender : MonoBehaviour
{
    public string targetTag = "TargetObject"; // ��� �������� �������
    private ScoreCounter scoreCounter; // ������ �� ������ ������� ��������

    private void Start()
    {
        // ������� ��������� ScoreCounter �� ������� � ����� "ScoreCounter"
        GameObject scoreCounterObject = GameObject.FindGameObjectWithTag("ScoreCounter");
        if (scoreCounterObject != null)
        {
            scoreCounter = scoreCounterObject.GetComponent<ScoreCounter>();
        }
        else
        {
            Debug.LogError("ScoreCounter object not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������� �������� ������� � ������ �����
        if (other.CompareTag(targetTag))
        {
            // ����������� ������� � ������� �������� �� 1
            scoreCounter.IncrementScore();
            Debug.Log("+1");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���������, ���� ��� ������� � ������� �������� � �� ���� �� ��������
        if (other.CompareTag(targetTag))
        {
            // ��������� ������� � ������� �������� �� 1
            scoreCounter.DecrementScore();
            Debug.Log("-1");
        }
    }
}
