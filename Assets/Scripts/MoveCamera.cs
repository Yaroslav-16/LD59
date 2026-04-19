using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [Header("Настройки")]
    public float dragSpeed = 5f;              // скорость перетаскивания (высокое значение = быстро)
    public float edgeScrollSpeed = 10f;       // скорость скролла краями
    public bool edgeScrolling = true;

    [Header("Плавность (опционально)")]
    public bool useSmoothing = false;         // выключи плавность для мгновенного отклика
    public float smoothTime = 0.03f;           // если включена, то маленькое значение

    [Header("Границы")]
    public bool useBounds = true;
    public float minX = -20f;
    public float maxX = 20f;
    public float minY = -15f;
    public float maxY = 15f;

    private Camera mainCamera;
    private Vector3 dragStartPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private bool isDragging = true;

    void Start()
    {
        mainCamera = Camera.main;
        targetPosition = transform.position;
    }

    void Update()
    {
        // ПКМ нажата - запоминаем точку старта
        if (Input.GetMouseButtonDown(1))
        {
            dragStartPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        // ПКМ зажата - двигаем камеру МГНОВЕННО
        if (Input.GetMouseButton(1) && isDragging)
        {
            Vector3 currentMouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragStartPosition - currentMouse;

            // Прямое перемещение с множителем скорости
            targetPosition = transform.position + difference * dragSpeed;
            dragStartPosition = currentMouse;
        }

        // Отпустили ПКМ
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // Авто-скролл по краям
        if (edgeScrolling && !isDragging)
        {
            Vector3 move = Vector3.zero;
            Vector3 mousePos = Input.mousePosition;

            if (mousePos.x <= 20) move.x = -edgeScrollSpeed * Time.deltaTime;
            else if (mousePos.x >= Screen.width - 20) move.x = edgeScrollSpeed * Time.deltaTime;

            if (mousePos.y <= 20) move.y = -edgeScrollSpeed * Time.deltaTime;
            else if (mousePos.y >= Screen.height - 20) move.y = edgeScrollSpeed * Time.deltaTime;

            targetPosition += move;
        }

        // Применяем перемещение (мгновенно или плавно)
        if (useSmoothing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            transform.position = targetPosition;  // МГНОВЕННОЕ перемещение
        }

        // Ограничения
        if (useBounds)
        {
            Vector3 clamped = transform.position;
            clamped.x = Mathf.Clamp(clamped.x, minX, maxX);
            clamped.y = Mathf.Clamp(clamped.y, minY, maxY);
            transform.position = clamped;
            targetPosition = clamped;
        }
    }
}