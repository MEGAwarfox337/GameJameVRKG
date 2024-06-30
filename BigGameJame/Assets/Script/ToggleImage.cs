using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    public GameObject image; // Ссылка на объект изображения

    private bool isVisible = false; // Переменная для отслеживания видимости

    public void Toggle()
    {
        isVisible = !isVisible; // Переключаем состояние
        image.SetActive(isVisible); // Устанавливаем активность изображения
    }
}