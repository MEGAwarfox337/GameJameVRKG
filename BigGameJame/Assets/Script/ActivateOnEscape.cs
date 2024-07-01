using UnityEngine;

public class ActivateOnEscape : MonoBehaviour
{
    public GameObject objectToActivate; // ������, ������� ����� ������������

    void Update()
    {
        // ���������, ������ �� ������� Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���������, ������ �� ������ ��� ���������
            if (objectToActivate != null)
            {
                // ���������� ������
                objectToActivate.SetActive(true);
            }
        }
    }
}
