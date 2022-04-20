using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        EnemyManager.SharedInstance.onEnemyChanged.AddListener(RefreshText);
        //para que no quede el default
        RefreshText();
    }

    // Update is called once per frame
    void RefreshText()
    {
        _text.text = "REMAINING ENEMIES: " + EnemyManager.SharedInstance.EnemyCount;
    }
}
