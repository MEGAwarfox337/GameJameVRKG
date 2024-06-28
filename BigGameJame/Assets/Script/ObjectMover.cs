using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMover : MonoBehaviour
{
    public float liftHeight = 2f; // ��������� �������� ������� �� ��� Y � ������� �����������
    public float moveSpeed = 5f; // �������� ����������� �� ���� X � Z
    public GameObject activatedObject; // GameObject ��� ��������� ��� �������� �������
    private Rigidbody rb;
    private bool isRaised = false; // ���� ��� ������������ �������� �������
    private Quaternion originalRotation; // �������� �������� �������

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // �������� � ������������� ������
        originalRotation = transform.rotation; // ��������� �������� �������� �������

        // ������������ ������, ������� ����� ����������� ��� ��������
        if (activatedObject != null)
        {
            activatedObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isRaised && Input.GetMouseButtonDown(0))
        {
            // ���������, ��� ����� ������ � ������� ��������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // ��������� ������ �� �������� �������� liftHeight �� ��� Y � ������� �����������
                    Vector3 newPosition = transform.position + new Vector3(0, liftHeight, 0);
                    transform.position = newPosition;

                    // ������������� �������� ������� � �������� ���������
                    transform.rotation = originalRotation;

                    isRaised = true; // ������������� ���� �������� �������
                    rb.isKinematic = true; // ��������� Rigidbody � �������������� �����

                    // ���������� ������, ������� ������ ���� ����������� ��� ��������
                    if (activatedObject != null)
                    {
                        activatedObject.SetActive(true);
                    }
                }
            }
        }
        else if (isRaised)
        {
            // ����������� ������� �� ���� X � Z ��� ������ ������ W, A, S, D
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
            Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
            transform.Translate(moveAmount);

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
}
