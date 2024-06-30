using UnityEngine; 
 
public class ScoreCounter : MonoBehaviour 
{ 
    public int targetScore = 10; // Целевой счет для активации объектов 
    public GameObject objectToActivate1; // Первый объект для активации 
    public GameObject objectToActivate2; // Второй объект для активации 
    public GameObject objectToActivate3; // Третий объект для активации 
    public GameObject objectToActivate4; // Четвертый объект для активации
    public GameObject objectToActivate5; // Пятый объект для активации
    public GameObject buttonToDeactivate; // Кнопка, которую нужно деактивировать
    public GameObject imageToDeactivate; // Изображение, которое нужно деактивировать
    public GameObject objectToDeactivate; // Объект, который нужно деактивировать

    private int currentScore = 0; // Текущий счет 
    
    private void Start() 
    { 
        // Начальные настройки, если нужно
    } 
 
    // Увеличиваем счет на 1 
    public void IncrementScore() 
    { 
        currentScore++; 
        CheckScore(); 
        Debug.Log("Current Score: " + currentScore); // Выводим текущий счет в консоль 
    } 
 
    // Уменьшаем счет на 1 
    public void DecrementScore() 
    { 
        currentScore--; 
        CheckScore(); 
        Debug.Log("Current Score: " + currentScore); // Выводим текущий счет в консоль 
    } 
 
    // Проверяем счет и активируем/деактивируем объекты соответственно 
    private void CheckScore() 
    { 
        if (currentScore >= targetScore) 
        { 
            // Активируем объекты 
            if (objectToActivate1 != null) 
                objectToActivate1.SetActive(true); 
             
            if (objectToActivate2 != null) 
                objectToActivate2.SetActive(true); 
             
            if (objectToActivate3 != null) 
                objectToActivate3.SetActive(true);

            if (objectToActivate4 != null) 
                objectToActivate4.SetActive(true);

            if (objectToActivate5 != null) 
                objectToActivate5.SetActive(true); 
            
            // Деактивируем кнопку
            if (buttonToDeactivate != null)
                buttonToDeactivate.SetActive(false);

            // Деактивируем изображение
            if (imageToDeactivate != null)
                imageToDeactivate.SetActive(false);

            if (objectToDeactivate != null) 
                objectToDeactivate.SetActive(false);
        } 
        else 
        { 
            // Деактивируем объекты 
            if (objectToActivate1 != null) 
                objectToActivate1.SetActive(false); 
             
            if (objectToActivate2 != null) 
                objectToActivate2.SetActive(false); 
             
            if (objectToActivate3 != null) 
                objectToActivate3.SetActive(false);

            if (objectToActivate4 != null) 
                objectToActivate4.SetActive(false);

            if (objectToActivate5 != null) 
                objectToActivate5.SetActive(false); 

            // Активируем кнопку (если нужно, можно удалить эту часть)
            if (buttonToDeactivate != null)
                buttonToDeactivate.SetActive(true);

            // Активируем изображение (если нужно, можно удалить эту часть)
            if (imageToDeactivate != null)
                imageToDeactivate.SetActive(true);

            if (objectToDeactivate != null) 
                objectToDeactivate.SetActive(true);
        } 
    } 
}