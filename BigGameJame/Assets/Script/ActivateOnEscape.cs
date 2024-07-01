using UnityEngine;

public class ActivateOnEscape : MonoBehaviour
{
    public GameObject objectToActivate; // Объект, который нужно активировать

    void Update()
    {
        // Проверяем, нажата ли клавиша Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Проверяем, указан ли объект для активации
            if (objectToActivate != null)
            {
                // Активируем объект
                objectToActivate.SetActive(true);
            }
        }
    }
}
