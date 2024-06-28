using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMover : MonoBehaviour
{
    public float liftHeight = 2f; // ��������� �������� ������� �� ��� Y � ������� �����������
    public float moveSpeed = 5f; // �������� ����������� �� ���� X � Z
    public float rotationDuration = 1f; // ������������ �������� � ��������
    public GameObject activatedObject; // GameObject ��� ��������� ��� �������� �������
    public GameObject rotatableObject; // GameObject, ������� ����� �������
    public float slowMoveSpeed = 2.5f; // �������� ����������� ��� ������� ������� Ctrl
    private Rigidbody rb;
    private bool isRaised = false; // ���� ��� ������������ �������� �������
    private bool isRotating = false; // ���� ��� ������������ �������� �������
    private Quaternion originalRotation; // �������� �������� �������
    private float originalMoveSpeed; // �������� �������� �����������

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // �������� � ������������� ������
        originalRotation = transform.rotation; // ��������� �������� �������� �������
        originalMoveSpeed = moveSpeed; // ��������� �������� �������� �����������

        // ������������ ������, ������� ����� ����������� ��� ��������
        if (activatedObject != null)
        {
            activatedObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isRaised && !isRotating && Input.GetMouseButtonDown(0))
        {
            // ���������, ��� ����� ������ � ������� ��������
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
            // ���������, ������ �� ������� Shift
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            // ���� ������ ������� Shift, �� ���������� ������� �������� �� 90 ��������
            if (isShiftPressed && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                Vector3 rotationAxis = Vector3.zero;

                // ���������� ��� �������� � ����������� �� ������� �������
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rotationAxis = Vector3.left; // ������� ������ ��� X
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    rotationAxis = Vector3.down; // ������� ������ ��� Y � ������������� �����������
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    rotationAxis = Vector3.right; // ������� ������ ��� X � ������������� �����������
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    rotationAxis = Vector3.up; // ������� ������ ��� Y
                }

                StartCoroutine(RotateObject(rotationAxis * 90f, rotationDuration));
            }
            else
            {
                // ���� ������� Shift �� ������, �� ��������� ����������� ������� �� ���� X � Z
                if (!isShiftPressed)
                {
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");

                    // ���������, ������ �� ������� Ctrl
                    bool isCtrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

                    // ������������� �������� ����������� � ����������� �� ��������� ������� Ctrl
                    float currentMoveSpeed = isCtrlPressed ? slowMoveSpeed : originalMoveSpeed;

                    Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
                    Vector3 moveAmount = moveDirection * currentMoveSpeed * Time.deltaTime;
                    transform.Translate(moveAmount);
                }
            }

            // ���� ������ ������ ���, �������� ������
            if (Input.GetMouseButtonDown(0))
            {
                rb.isKinematic = false; // ���������� Rigidbody � ������������ �����
                isRaised = false; // ���������� ���� �������� �������

                // ������������ ������, ������� ��� ����������� ��� ��������
                if (activatedObject != null)
                {
                    activatedObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator RaiseObject()
    {
        isRaised = true; // ������������� ���� �������� �������
        rb.isKinematic = true; // ��������� Rigidbody � �������������� �����

        // ��������� ������ �� �������� �������� liftHeight �� ��� Y � ������� �����������
        Vector3 newPosition = transform.position + new Vector3(0, liftHeight, 0);
        transform.position = newPosition;

        // ������������� �������� ������� � �������� ���������
        transform.rotation = originalRotation;

        // ���������� ������, ������� ������ ���� ����������� ��� ��������
        if (activatedObject != null)
        {
            activatedObject.SetActive(true);
        }

        // ���� ���������� �������� ����� ����������� ����� ��������
        yield return null;
    }

    IEnumerator RotateObject(Vector3 angles, float duration)
    {
        isRotating = true; // ������������� ���� �������� �������

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

        isRotating = false; // ���������� ���� �������� ����� ����������
    }
}
