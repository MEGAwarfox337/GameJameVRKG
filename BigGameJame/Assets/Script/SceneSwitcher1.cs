using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcherV2 : MonoBehaviour
{
    public bool CanE = false; // Булевая переменная, которую можно включить в инспекторе
    public float delayNextScene = 2f; // Задержка перед переключением на следующую сцену
    public float delayPreviousScene = 2f; // Задержка перед переключением на предыдущую сцену
    public GameObject objectToActivate; // Объект, который нужно активировать при нажатии E

    void Update()
    {
        // Проверяем, нажата ли клавиша E и включена ли булевая переменная CanE
        if (CanE && Input.GetKeyDown(KeyCode.E))
        {
            // Активируем заданный объект
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            // Начинаем корутину для переключения на следующую сцену с задержкой
            StartCoroutine(SwitchSceneWithDelay(true));
        }

        // Проверяем, нажата ли клавиша Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Начинаем корутину для переключения на предыдущую сцену с задержкой
            StartCoroutine(SwitchSceneWithDelay(false));
        }
    }

    // Корутина для переключения сцены с задержкой
    IEnumerator SwitchSceneWithDelay(bool next)
    {
        // Определяем задержку в зависимости от направления переключения
        float delay = next ? delayNextScene : delayPreviousScene;

        // Ждем указанное время
        yield return new WaitForSeconds(delay);

        // Получаем индекс текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Определяем индекс новой сцены в зависимости от направления переключения
        int newSceneIndex = next ? currentSceneIndex + 1 : currentSceneIndex - 1;

        // Проверяем, что индекс новой сцены находится в допустимом диапазоне
        if (newSceneIndex >= 0 && newSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Переключаемся на новую сцену
            SceneManager.LoadScene(newSceneIndex);
        }
    }
}
