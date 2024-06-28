using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Color hoverColor = Color.red; // Цвет при наведении, можно настроить через Inspector

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

    // Метод, который будет вызываться при нажатии кнопки
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}