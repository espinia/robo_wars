using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        WaveManager.SharedInstance.onWaveChanged.AddListener(RefreshText);
    }

    // Update is called once per frame
    void RefreshText()
    {
        _text.text = "Wave: " + 
            (WaveManager.SharedInstance.TotalWaves-WaveManager.SharedInstance.WavesCount)
            +"/"+
            WaveManager.SharedInstance.TotalWaves;
    }
}
