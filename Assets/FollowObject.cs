using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target; // Цель в 3D мире, над которой будет отображаться UI элемент
    public Vector3 offset; // Смещение для точной настройки позиции UI элемента
    public Camera mainCamera; // Камера, с которой будет рассчитываться позиция

    private RectTransform uiElement; // RectTransform 
    void Start()
    {
        uiElement = GetComponent<RectTransform>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.WorldToScreenPoint(target.position + offset);
        // if (target != null)
        // {
        //     // Преобразование позиции цели из мировых координат в экранные
        //     Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position + offset);
        //     
        //     // Преобразование экранной позиции в координаты Canvas
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(uiElement.parent as RectTransform, screenPosition, mainCamera, out Vector2 localPoint);
        //     
        //     // Установка позиции UI элемента
        //     uiElement.localPosition = localPoint;
        // }
    }
}
