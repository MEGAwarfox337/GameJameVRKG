using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement; 
 
public class ButtonComplete : MonoBehaviour 
{ 
    public Color hoverColor = Color.red; // Цвет при наведении, можно настроить через Inspector 
    public int levelIndex; // Индекс уровня для этой кнопки 
    public Button button; // Кнопка для управления взаимодействием 
    public string sceneName; // Имя сцены уровня 
 
    private Image buttonImage; 
    private Color originalColor; 
 
    void Start() 
    { 
        // Разблокируем первый уровень, если это первый запуск игры 
        if (levelIndex == 1 && PlayerPrefs.GetInt("Level_1", 0) == 0) 
        { 
            UnlockLevel(1); 
        } 
 
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
 
        // Проверяем, разблокирован ли уровень 
        if (!IsLevelUnlocked(levelIndex)) 
        { 
            button.interactable = false; // Блокируем кнопку, если уровень не разблокирован 
        } 
 
        // Добавляем слушатель нажатия кнопки 
        button.onClick.AddListener(() => SwitchScene(sceneName)); 
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
        Debug.Log("SwitchScene called for scene: " + sceneName); 
 
        if (IsLevelUnlocked(levelIndex)) 
        { 
            Debug.Log("Level " + levelIndex + " is unlocked. Switching to scene: " + sceneName); 
            SceneManager.LoadScene(sceneName); 
        } 
        else 
        { 
            Debug.Log("Level " + levelIndex + " is locked."); 
        } 
    } 
 
    // Проверяем, разблокирован ли уровень 
    public bool IsLevelUnlocked(int levelIndex) 
    { 
        bool unlocked = PlayerPrefs.GetInt("Level_" + levelIndex, 0) == 1; 
        Debug.Log("IsLevelUnlocked(" + levelIndex + "): " + unlocked); 
        return unlocked; 
    } 
 
    // Разблокируем уровень 
    public void UnlockLevel(int levelIndex) 
    { 
        PlayerPrefs.SetInt("Level_" + levelIndex, 1); 
        Debug.Log("UnlockLevel(" + levelIndex + "): now unlocked"); 
    } 
}