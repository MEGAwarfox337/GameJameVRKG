using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public Color hoverColor = Color.red; // Цвет при наведении, можно настроить через Inspector
    public AudioClip buttonClip;  // Аудиоклип для кнопки
    public AudioSource audioSource;  // Аудиоисточник для воспроизведения звука

    private Image buttonImage;
    private Color originalColor;

    void Start()
    {
        // Получаем компонент Image у кнопки
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color; // Запоминаем исходный цвет кнопки

        // Добавляем EventTrigger для отслеживания наведения и ухода курсора
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        // Событие наведения курсора
        EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
        entryPointerEnter.eventID = EventTriggerType.PointerEnter;
        entryPointerEnter.callback.AddListener((data) => { OnPointerEnter(); });
        trigger.triggers.Add(entryPointerEnter);

        // Событие ухода курсора
        EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
        entryPointerExit.eventID = EventTriggerType.PointerExit;
        entryPointerExit.callback.AddListener((data) => { OnPointerExit(); });
        trigger.triggers.Add(entryPointerExit);
    }

    void OnPointerEnter()
    {
        // Изменяем цвет Image при наведении курсора
        buttonImage.color = hoverColor;
    }

    void OnPointerExit()
    {
        // Возвращаем исходный цвет Image при уходе курсора
        buttonImage.color = originalColor;
    }

    // Метод для переключения сцены
    public void SwitchScene(string sceneName)
    {
        // Если аудиоисточник и аудиоклип назначены
        if (audioSource != null && buttonClip != null)
        {
            StartCoroutine(PlaySoundAndSwitchScene(sceneName));
        }
        else
        {
            // Если аудиоисточник или аудиоклип не назначены, сразу переходим на сцену
            SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator PlaySoundAndSwitchScene(string sceneName)
    {
        // Воспроизводим звук
        audioSource.PlayOneShot(buttonClip);
        // Ждем окончания звука
        yield return new WaitForSeconds(buttonClip.length);
        // Переходим на новую сцену
        SceneManager.LoadScene(sceneName);
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
        // Выводим сообщение в консоль, чтобы убедиться, что метод вызывается
        Debug.Log("QuitGame called");

        // Если аудиоисточник и аудиоклип назначены
        if (audioSource != null && buttonClip != null)
        {
            StartCoroutine(PlaySoundAndQuitGame());
        }
        else
        {
            // Если аудиоисточник или аудиоклип не назначены, сразу выходим из игры
            Application.Quit();
            Debug.Log("Application.Quit called");
        }
    }

    IEnumerator PlaySoundAndQuitGame()
    {
        // Воспроизводим звук
        audioSource.PlayOneShot(buttonClip);
        // Ждем окончания звука
        yield return new WaitForSeconds(buttonClip.length);
        // Выходим из игры
        Application.Quit();
        Debug.Log("Application.Quit called after sound");
    }
}