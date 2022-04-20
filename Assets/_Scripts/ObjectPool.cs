using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    public GameObject prefab;

    //lista de objetos del pool
    public List<GameObject> pooledObjects;

    //numero de objetos colocar en el pool
    public int amountToPool;

	private void Awake()
	{
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
        else
        {
            Debug.LogError("YA HAY OTRO POOL EN PANTALLA");
            Destroy(gameObject);
        }
	}

	private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            //insstanciar
            tmp = Instantiate(prefab);
            //desactivar
            tmp.SetActive(false);
            //agregar
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetFirstPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            //no esta activo en la jerarquia de la escena
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
