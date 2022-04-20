using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //prefab del enemigo a spawnear
    [Tooltip("Prefab del enemigo a instanciar")]
    public GameObject prefab;

    [Tooltip("Tiempo en el que se inicia y finaliza la oleada")]
    public float startTime, endTime;

    [Tooltip("Tiempo entre generación de enemigos")]
    public float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        WaveManager.SharedInstance.AddWave(this);
        InvokeRepeating("SpawnEnemy", startTime, spawnRate);
        //Invoke("CancelInvoke", endTime);
        Invoke("EndWave", endTime);
    }

    void SpawnEnemy()
	{
        // a partir de x,  y y z genera el cuaternión
        // en esta función los parámtros son radianes
        /*
        Quaternion q = Quaternion.Euler(0, 
            transform.rotation.eulerAngles.y+Random.Range(-45.0f,45.0f),0);
        Instantiate(prefab, transform.position,q);*/
        Instantiate(prefab, transform.position, transform.rotation);
    }

    void EndWave()
	{
        CancelInvoke();
        WaveManager.SharedInstance.RemoveWave(this);
    }
}
