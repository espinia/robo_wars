using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	public Text actualScore, actualTime;
	public Text bestScore, bestTime;

	public bool playerHasWon;

	private void Start()
	{
		//activamos el cursor
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		if (playerHasWon)
		{
			int score = PlayerPrefs.GetInt("Last Score", 0);
			float time = PlayerPrefs.GetFloat("Last Time", 0.0f);
			//actualScore.text = "Score:" + PlayerPrefs.GetInt("Last Score", 0);
			actualScore.text =string.Format("Score:{0}", score);
			actualScore.text = string.Format("Time:{0}", time);

			bestScore.text = "Best:" + PlayerPrefs.GetInt("High Score", 0);
			bestTime.text = "Best:" + PlayerPrefs.GetFloat("Low Time", 0.0f);

			//otra forma
			print($"La partida ha terminado con {score} puntos en {time} segundos");

			string msg = $"Esto es un mensaje de {score} puntos";
			print(msg);

			//internamente gestiona mejor la memoria
			StringBuilder builder = new StringBuilder();

			builder.Append("Score: ");
			builder.Append(score);
			
			print(builder.ToString());

			//lo podemos limpiar para crear otra cadena
			builder.Clear();

		}
	}

	public void ReloadLevel()
	{
		SceneManager.LoadScene("Level1");
	}
}
