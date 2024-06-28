using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMover : MonoBehaviour
{
    public float liftHeight = 2f; // Указанное значение подъема по оси Y в мировых координатах
    public float moveSpeed = 5f; // Скорость перемещения по осям X и Z
    public GameObject activatedObject; // GameObject для активации при поднятии объекта
    private Rigidbody rb;
    private bool isRaised = false; // Флаг для отслеживания поднятия объекта
    private Quaternion originalRotation; // Исходное вращение объекта

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Начинаем с динамического режима
        originalRotation = transform.rotation; // Сохраняем исходное вращение объекта

        // Деактивируем объект, который будет активирован при поднятии
        if (activatedObject != null)
        {
            activatedObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isRaised && Input.GetMouseButtonDown(0))
        {
            // Проверяем, что нажат объект с текущим скриптом
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // Поднимаем объект на заданное значение liftHeight по оси Y в мировых координатах
                    Vector3 newPosition = transform.position + new Vector3(0, liftHeight, 0);
                    transform.position = newPosition;

                    // Устанавливаем вращение объекта в исходное состояние
                    transform.rotation = originalRotation;

                    isRaised = true; // Устанавливаем флаг поднятия объекта
                    rb.isKinematic = true; // Переводим Rigidbody в кинематический режим

                    // Активируем объект, который должен быть активирован при поднятии
                    if (activatedObject != null)
                    {
                        activatedObject.SetActive(true);
                    }
                }
            }
        }
        else if (isRaised)
        {
            // Перемещение объекта по осям X и Z при помощи клавиш W, A, S, D
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
            Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
            transform.Translate(moveAmount);

            // Если нажата кнопка ЛКМ, опускаем объект
            if (Input.GetMouseButtonDown(0))
            {
                rb.isKinematic = false; // Возвращаем Rigidbody в динамический режим
                isRaised = false; // Сбрасываем флаг поднятия объекта

                // Деактивируем объект, который был активирован при поднятии
                if (activatedObject != null)
                {
                    activatedObject.SetActive(false);
                }
            }
        }
    }
}
