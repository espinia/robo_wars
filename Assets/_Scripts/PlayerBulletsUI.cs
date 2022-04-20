using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerBulletsUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

	public PlayerShoot targetShooting;

	private void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		//se debe usar poco porque reserva memoria para los strings
		//igual este si conviene con evento
		_text.text = "Bullets: " + targetShooting.bulletsAmount;
	}

}
