using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMover : MonoBehaviour
{
    public float liftHeight = 2f; // Высота подъема объекта по оси Y при активации
    public float moveSpeed = 5f; // Скорость перемещения по осям X и Z
    public float rotationDuration = 1f; // Длительность поворота в секундах
    public GameObject activatedObject; // GameObject для активации при поднятии объекта
    public GameObject rotatableObject; // GameObject, который будет вращаться
    public float slowMoveSpeed = 2.5f; // Скорость перемещения при удерживании Ctrl
    public GraphicRaycaster raycaster; // GraphicRaycaster для Canvas
    public EventSystem eventSystem; // EventSystem для проверки UI

    private Rigidbody rb;
    private bool isRaised = false; // Флаг для проверки, поднят ли объект
    private bool isRotating = false; // Флаг для проверки, происходит ли вращение
    private Quaternion originalRotation; // Исходная ротация объекта
    private float originalMoveSpeed; // Исходная скорость перемещения

    private static bool anyObjectRaised = false; // Статический флаг для отслеживания, поднят ли какой-либо объект

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Отключаем кинематику Rigidbody
        originalRotation = transform.rotation; // Сохраняем исходную ротацию объекта
        originalMoveSpeed = moveSpeed; // Сохраняем исходную скорость перемещения

        // Деактивируем объект, который должен активироваться при поднятии
        if (activatedObject != null)
        {
            activatedObject.SetActive(false);
        }
    }

    void Update()
    {
        if (IsPointerOverUI()) return; // Проверяем, находится ли курсор над UI-элементом

        if (!isRaised && !isRotating && !anyObjectRaised && Input.GetMouseButtonDown(0))
        {
            // Выпускаем луч из камеры через позицию курсора
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Проверяем, является ли объект, по которому был произведен клик, активным объектом
                if (hit.collider.gameObject == gameObject)
                {
                    StartCoroutine(RaiseObject());
                }
            }
        }
        else if (isRaised && !isRotating)
        {
            // Проверяем, удерживается ли клавиша Shift
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            // Если удерживается клавиша Shift, то выполняем поворот на 90 градусов
            if (isShiftPressed && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                Vector3 rotationAxis = Vector3.zero;

                // Определяем ось поворота в зависимости от нажатой клавиши
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rotationAxis = Vector3.left; // Поворот вокруг оси X
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    rotationAxis = Vector3.down; // Поворот вокруг оси Y против часовой стрелки
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    rotationAxis = Vector3.right; // Поворот вокруг оси X против часовой стрелки
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    rotationAxis = Vector3.up; // Поворот вокруг оси Y
                }

                StartCoroutine(RotateObject(rotationAxis * 90f, rotationDuration));
            }
            else
            {
                // Если клавиша Shift не удерживается, то перемещаем объект по осям X и Z
                if (!isShiftPressed)
                {
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");

                    // Проверяем, удерживается ли клавиша Ctrl
                    bool isCtrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

                    // Определяем текущую скорость перемещения в зависимости от удерживания Ctrl
                    float currentMoveSpeed = isCtrlPressed ? slowMoveSpeed : originalMoveSpeed;

                    Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
                    Vector3 moveAmount = moveDirection * currentMoveSpeed * Time.deltaTime;
                    transform.Translate(moveAmount);
                }
            }

            // Если нажата левая кнопка мыши, опускаем объект
            if (Input.GetMouseButtonDown(0))
            {
                rb.isKinematic = false; // Отключаем кинематику Rigidbody
                isRaised = false; // Сбрасываем флаг поднятия
                anyObjectRaised = false; // Сбрасываем глобальный флаг

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
        isRaised = true; // Устанавливаем флаг поднятия
        anyObjectRaised = true; // Устанавливаем глобальный флаг
        rb.isKinematic = true; // Включаем кинематику Rigidbody

        // Перемещаем объект вверх на заданную высоту
        Vector3 newPosition = transform.position + new Vector3(0, liftHeight, 0);
        transform.position = newPosition;

        // Сбрасываем ротацию объекта на исходную
        transform.rotation = originalRotation;

        // Активируем объект, если он назначен
        if (activatedObject != null)
        {
            activatedObject.SetActive(true);
        }

        // Ожидаем до следующего кадра
        yield return null;
    }

    IEnumerator RotateObject(Vector3 angles, float duration)
    {
        isRotating = true; // Устанавливаем флаг вращения

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

        isRotating = false; // Сбрасываем флаг вращения
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        return results.Count > 0;
    }
}