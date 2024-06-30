using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Color hoverColor = Color.red; // Цвет при наведении, можно настроить через Inspector

    private Color originalColor;
    private LevelButton levelButton; // Ссылка на выбранную кнопку уровня

    void Start()
    {
        originalColor = GetComponent<Image>().color; // Получаем исходный цвет кнопки "Играть"

        // Добавляем EventTrigger компонент, если его еще нет
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }

        // Pointer Enter event
        EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
        entryPointerEnter.eventID = EventTriggerType.PointerEnter;
        entryPointerEnter.callback.AddListener((data) => { OnPointerEnter(); });
        trigger.triggers.Add(entryPointerEnter);

        // Pointer Exit event
        EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
        entryPointerExit.eventID = EventTriggerType.PointerExit;
        entryPointerExit.callback.AddListener((data) => { OnPointerExit(); });
        trigger.triggers.Add(entryPointerExit);
    }

    public void OnPlayButtonClick()
    {
        if (levelButton != null && levelButton.isSelected)
        {
            string levelName = levelButton.gameObject.name;
            SceneManager.LoadScene(levelName); // Загружаем уровень, если кнопка уровня выбрана
        }
        else
        {
            Debug.Log("Выберите уровень перед игрой.");
        }
    }

    void OnPointerEnter()
    {
        GetComponent<Image>().color = hoverColor; // Устанавливаем цвет при наведении
    }

    void OnPointerExit()
    {
        ResetColor(); // Возвращаем исходный цвет кнопки "Играть" при выходе курсора
    }

    public void SetLevelButton(LevelButton button)
    {
        levelButton = button;
    }

    public void ResetColor()
    {
        GetComponent<Image>().color = originalColor; // Возвращаем цвет кнопки "Играть" к исходному
    }
}