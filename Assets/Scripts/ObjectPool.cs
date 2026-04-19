using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [Header("Настройки пула")]
    public GameObject objectPrefab;     // что будем пулить
    public int poolSize = 20;           // размер пула

    private Queue<GameObject> pool = new Queue<GameObject>();


    private void Awake()
    {
        G.objectPool = this;
    }

    void Start()
    {
        // Создаём пул объектов
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);        // сразу выключаем
            pool.Enqueue(obj);           // кладём в очередь
        }
    }

    // Взять объект из пула
    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            // Если пул пуст — создаём новый (опционально)
            Debug.LogWarning("Пул пуст! Создаём новый объект");
            GameObject newObj = Instantiate(objectPrefab, position, rotation);
            return newObj;
        }

        GameObject obj = pool.Dequeue();  // достаём из очереди
        obj.SetActive(true);              // включаем
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    // Вернуть объект в пул
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);             // выключаем
        pool.Enqueue(obj);                // возвращаем в очередь
    }
}