using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager SharedInstance;
    private List<WaveSpawner> waves;   
    
    public int WavesCount
	{
        get => waves.Count;
	}

    private int maxWaves;
    public int TotalWaves
    {
        get => maxWaves;
    }

    public UnityEvent onWaveChanged;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            waves = new List<WaveSpawner>();
        }
        else
        {
            Debug.LogWarning("WaveManager duplicado, debe destruirse");
            Destroy(gameObject);
        }
    }

    public void AddWave(WaveSpawner wave)
	{
        maxWaves++;
        waves.Add(wave);
        onWaveChanged.Invoke();

    }

    public void RemoveWave(WaveSpawner wave)
    {
        waves.Remove(wave);
        onWaveChanged.Invoke();
    }
}
