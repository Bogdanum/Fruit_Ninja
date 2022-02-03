using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T prefab;
    [SerializeField, Range(1, 25)] private int initialQuantity;
    
    public static GenericObjectPool<T> Instance { get; private set; }
    private Queue<T> objectsQueue = new Queue<T>();

    private void Awake()
    {
        Instance = this;
        AddObjects(initialQuantity);
    }

    public T Get()
    {
        if (objectsQueue.Count == 0)
        {
            AddObjects(1);
        }
        return objectsQueue.Dequeue();
    }

    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = GameObject.Instantiate(prefab);
            newObject.gameObject.SetActive(false);
            objectsQueue.Enqueue(newObject);
        }
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objectsQueue.Enqueue(objectToReturn);
    }
}
