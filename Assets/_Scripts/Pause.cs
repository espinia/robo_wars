using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
	public Button exitButton;

	public AudioMixerSnapshot pauseSNP;
	public AudioMixerSnapshot gameSNP;

	private void Start()
	{
		pauseMenu.SetActive(false);
		exitButton.onClick.AddListener(ExitGame);
	}

	// Update is called once per frame
	void Update()
    {
        //if (Input.GetAxis("Cancel")!=0)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			//aparecer cursor
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			pauseSNP.TransitionTo(0.5f);

			pauseMenu.SetActive(true);
			Time.timeScale = 0;
		}
    }

	public void ResumeGame()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;

		gameSNP.TransitionTo(0.5f);

		//desaparecer cursor
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void ExitGame()
	{
		print("Ejecución finalizada");
		Application.Quit();
	}

}
