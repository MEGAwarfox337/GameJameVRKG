using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 1f; // Скорость перемещения камеры
    public float minX = -10f; // Минимальное значение X
    public float maxX = 10f; // Максимальное значение X

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            float newPosX = transform.position.x + scroll * moveSpeed;
            newPosX = Mathf.Clamp(newPosX, minX, maxX);

            transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, transform.position.y, transform.position.z - 1), new Vector3(minX, transform.position.y, transform.position.z + 1));
        Gizmos.DrawLine(new Vector3(maxX, transform.position.y, transform.position.z - 1), new Vector3(maxX, transform.position.y, transform.position.z + 1));
    }
}
