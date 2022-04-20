using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeWaves : MonoBehaviour
{
	[SerializeField]
    private Life playerLife;

    [SerializeField]
    private Life baseLife;

    private void Start()
	{
        playerLife.onDeath.AddListener(CheckLoseCondition);
        baseLife.onDeath.AddListener(CheckLoseCondition);

        EnemyManager.SharedInstance.onEnemyChanged.AddListener(CheckWinCondition);
        WaveManager.SharedInstance.onWaveChanged.AddListener(CheckWinCondition);
    }

    void CheckLoseCondition()
	{
        //RegisterScore();
        SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
    }

    void CheckWinCondition()
	{
        if (EnemyManager.SharedInstance.EnemyCount <= 0
            &&
            WaveManager.SharedInstance.WavesCount <= 0)
        {
            RegisterScore();
            RegisterTime();
            SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
        }
    }

    void RegisterScore()
	{
        int actualScore = ScoreManager.SharedInstance.Amount;

        PlayerPrefs.SetInt("Last Score", actualScore);

        int highScore = PlayerPrefs.GetInt("High Score", 0);

        if(actualScore > highScore)
        {
            PlayerPrefs.SetInt("High Score", actualScore);
        }
	}

    void RegisterTime()
    {
        float actualTime = Time.time;

        PlayerPrefs.SetFloat("Last Time", actualTime);

        float lowTime = PlayerPrefs.GetFloat("Low Time", 9999999.9f);

        if (actualTime < lowTime)
        {
            PlayerPrefs.SetFloat("Low Time", actualTime);
        }
    }
}
