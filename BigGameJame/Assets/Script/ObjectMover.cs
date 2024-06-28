using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMover : MonoBehaviour
{
    public float liftHeight = 2f; // Указанное значение подъема по оси Y в мировых координатах
    public float moveSpeed = 5f; // Скорость перемещения по осям X и Z
    public float rotationDuration = 1f; // Длительность вращения в секундах
    public GameObject activatedObject; // GameObject для активации при поднятии объекта
    public GameObject rotatableObject; // GameObject, который можно крутить
    public float slowMoveSpeed = 2.5f; // Скорость перемещения при зажатой клавише Ctrl
    private Rigidbody rb;
    private bool isRaised = false; // Флаг для отслеживания поднятия объекта
    private bool isRotating = false; // Флаг для отслеживания вращения объекта
    private Quaternion originalRotation; // Исходное вращение объекта
    private float originalMoveSpeed; // Исходная скорость перемещения

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Начинаем с динамического режима
        originalRotation = transform.rotation; // Сохраняем исходное вращение объекта
        originalMoveSpeed = moveSpeed; // Сохраняем исходную скорость перемещения

        // Деактивируем объект, который будет активирован при поднятии
        if (activatedObject != null)
        {
            activatedObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isRaised && !isRotating && Input.GetMouseButtonDown(0))
        {
            // Проверяем, что нажат объект с текущим скриптом
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    StartCoroutine(RaiseObject());
                }
            }
        }
        else if (isRaised && !isRotating)
        {
            // Проверяем, зажата ли клавиша Shift
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            // Если зажата клавиша Shift, то производим плавное вращение на 90 градусов
            if (isShiftPressed && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                Vector3 rotationAxis = Vector3.zero;

                // Определяем ось вращения в зависимости от нажатой клавиши
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rotationAxis = Vector3.left; // Поворот вокруг оси X
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    rotationAxis = Vector3.down; // Поворот вокруг оси Y в отрицательном направлении
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    rotationAxis = Vector3.right; // Поворот вокруг оси X в отрицательном направлении
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    rotationAxis = Vector3.up; // Поворот вокруг оси Y
                }

                StartCoroutine(RotateObject(rotationAxis * 90f, rotationDuration));
            }
            else
            {
                // Если клавиша Shift не зажата, то выполняем перемещение объекта по осям X и Z
                if (!isShiftPressed)
                {
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");

                    // Проверяем, зажата ли клавиша Ctrl
                    bool isCtrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

                    // Устанавливаем скорость перемещения в зависимости от состояния клавиши Ctrl
                    float currentMoveSpeed = isCtrlPressed ? slowMoveSpeed : originalMoveSpeed;

                    Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
                    Vector3 moveAmount = moveDirection * currentMoveSpeed * Time.deltaTime;
                    transform.Translate(moveAmount);
                }
            }

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

    IEnumerator RaiseObject()
    {
        isRaised = true; // Устанавливаем флаг поднятия объекта
        rb.isKinematic = true; // Переводим Rigidbody в кинематический режим

        // Поднимаем объект на заданное значение liftHeight по оси Y в мировых координатах
        Vector3 newPosition = transform.position + new Vector3(0, liftHeight, 0);
        transform.position = newPosition;

        // Устанавливаем вращение объекта в исходное состояние
        transform.rotation = originalRotation;

        // Активируем объект, который должен быть активирован при поднятии
        if (activatedObject != null)
        {
            activatedObject.SetActive(true);
        }

        // Ждем завершения поднятия перед разрешением новых действий
        yield return null;
    }

    IEnumerator RotateObject(Vector3 angles, float duration)
    {
        isRotating = true; // Устанавливаем флаг вращения объекта

        Quaternion startRotation = rotatableObject.transform.rotation;
        Quaternion endRotation = rotatableObject.transform.rotation * Quaternion.Euler(angles);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            rotatableObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rotatableObject.transform.rotation = endRotation;

        isRotating = false; // Сбрасываем флаг вращения после завершения
    }
}
