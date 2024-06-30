using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    void Update()
    {
        if (IsPointerOverUI())
        {
            Debug.Log("Pointer is over UI");
            return;
        }

        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            // Ваш код для взаимодействия с объектами сцены
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Обработка нажатия на объект
                Debug.Log("Clicked on object: " + hit.collider.name);
            }
        }
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit UI Element: " + result.gameObject.name);
            }
            return true;
        }

        return false;
    }
}