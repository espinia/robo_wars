using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
	[SerializeField]
	private float amount;

	public float maximunLife=100.0f;

	public UnityEvent onDeath;

	private void Awake()
	{
		amount = maximunLife;
	}

	public float Amount
	{
		get => amount;

		set
		{
			amount = value;
			if (amount <= 0)
			{
				onDeath.Invoke();				
			}
		}
	} 
	
}
