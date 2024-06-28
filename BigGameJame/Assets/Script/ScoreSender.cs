using UnityEngine;

public class ScoreSender : MonoBehaviour
{
    public string targetTag = "TargetObject"; // Тег целевого объекта
    private ScoreCounter scoreCounter; // Ссылка на скрипт системы подсчета

    private void Start()
    {
        // Находим компонент ScoreCounter на объекте с тегом "ScoreCounter"
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
        // Проверяем, коснулся ли триггер целевого объекта с нужным тегом
        if (other.CompareTag(targetTag))
        {
            // Увеличиваем счетчик в системе подсчета на 1
            scoreCounter.IncrementScore();
            Debug.Log("+1");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, если был контакт с целевым объектом и он ушел из триггера
        if (other.CompareTag(targetTag))
        {
            // Уменьшаем счетчик в системе подсчета на 1
            scoreCounter.DecrementScore();
            Debug.Log("-1");
        }
    }
}
