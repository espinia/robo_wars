using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
	public float damage;

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.name);

		//destruye el objeto, no es necesario en object pooling
		//Destroy(gameObject);

		//regresa al pool el objeto
		gameObject.SetActive(false);

		/*
		if (other.CompareTag("Enemy") || other.CompareTag("Player"))
		{
			//solo player y enemigos son destruidos
			Destroy(other.gameObject);
		}*/

		Life life = other.GetComponent<Life>();
		if(life!=null)
		{
			life.Amount -= damage;
		}
	}
}
