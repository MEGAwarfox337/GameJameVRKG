using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int targetScore = 10; // Заданное количество для активации объекта
    public GameObject objectToActivate; // Объект для активации

    private int currentScore = 0; // Текущее значение счетчика

    // Метод для увеличения счетчика на 1
    public void IncrementScore()
    {
        currentScore++;
        CheckScore();
        Debug.Log("Current Score: " + currentScore); // Вывод текущего счета в консоль
    }

    // Метод для уменьшения счетчика на 1
    public void DecrementScore()
    {
        currentScore--;
        CheckScore();
        Debug.Log("Current Score: " + currentScore); // Вывод текущего счета в консоль
    }

    // Проверка текущего значения счетчика и активация объекта, если нужно
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
