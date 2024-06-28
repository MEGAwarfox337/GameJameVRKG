using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Import the EventSystems namespace

public class PlayButton : MonoBehaviour
{
    public Color hoverColor = Color.red; // Color when hovering, can be adjusted via Inspector

    private Color originalColor;
    private Image buttonImage;
    private LevelButton levelButton; // Reference to the selected level button

    void Start()
    {
        buttonImage = GetComponent<Image>(); // Get the Image component of the button
        originalColor = buttonImage.color; // Store the original color of the button

        // Add EventTrigger component if not already added
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
            SceneManager.LoadScene(levelName); // Load the level if a level button is selected
        }
        else
        {
            Debug.Log("Выберите уровень перед началом игры.");
        }
    }

    void OnPointerEnter()
    {
        // Change Image color on pointer enter
        buttonImage.color = hoverColor;
    }

    void OnPointerExit()
    {
        // Restore original Image color on pointer exit
        buttonImage.color = originalColor;
    }

    public void SetLevelButton(LevelButton button)
    {
        levelButton = button;
    }

    public void ResetColor()
    {
        buttonImage.color = originalColor; // Reset the button's color to the original color
    }
}