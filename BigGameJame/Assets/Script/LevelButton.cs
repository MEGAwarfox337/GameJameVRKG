using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement; 
 
public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
{ 
    public Color hoverColor = Color.red; // Цвет при наведении, можно настроить через Inspector 
    private Color originalColor; 
    public bool isSelected { get; private set; } 
 
    private Button button; 
 
    public static LevelButton selectedButton; // Статическое поле для хранения выбранной кнопки уровня 
 
    void Start() 
    { 
        button = GetComponent<Button>(); 
        originalColor = button.image.color; 
    } 
 
    public void OnPointerEnter(PointerEventData eventData) 
    { 
        if (!isSelected) 
            button.image.color = hoverColor; 
    } 
 
    public void OnPointerExit(PointerEventData eventData) 
    { 
        if (!isSelected) 
            button.image.color = originalColor; 
    } 
 
    public void OnPointerClick(PointerEventData eventData) 
    { 
        if (isSelected) 
        { 
            Deselect(); 
            FindObjectOfType<PlayButton>()?.ResetColor(); // Сбрасываем цвет кнопки "Играть" 
        } 
        else 
        { 
            Select(); 
            FindObjectOfType<PlayButton>()?.SetLevelButton(this); // Устанавливаем ссылку на выбранную кнопку уровня 
        } 
    } 
 
    void Select() 
    { 
        if (selectedButton != null && selectedButton != this) 
        { 
            selectedButton.Deselect(); // Снимаем выделение с предыдущей выбранной кнопки 
        } 
 
        isSelected = true; 
        button.image.color = hoverColor; 
        selectedButton = this; 
    } 
 
    public void Deselect() 
    { 
        isSelected = false; 
        button.image.color = originalColor; 
    } 
 
    // Метод, который будет вызываться при нажатии кнопки уровня 
    public void LoadScene() 
    { 
        SceneManager.LoadScene(gameObject.name); // Предполагается, что имя сцены совпадает с именем кнопки 
    } 
}