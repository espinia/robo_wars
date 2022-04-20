using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Cantidad de puntos que otorga el enemigo")]
    public int pointsAmount = 10;

	private void Awake()
	{
		var life = GetComponent<Life>();
		life.onDeath.AddListener(DestroyEnemy);
	}
	private void Start()
	{
		EnemyManager.SharedInstance.AddEnemy(this);
	}

	private void DestroyEnemy()
	{
		//activamos la animación
		Animator anim = GetComponent<Animator>();
		anim.SetTrigger("PlayDie");
		Invoke("PlayDestruction", 1f);

		//liberamos la referencia al Listener
		var life = GetComponent<Life>();
		life.onDeath.RemoveListener(DestroyEnemy);

		//le damos un delay para darle tiempo
		Destroy(gameObject, 2.0f);

		EnemyManager.SharedInstance.RemoveEnemy(this);
		ScoreManager.SharedInstance.Amount += pointsAmount;		
	}

	void PlayDestruction()
	{
		ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
		explosion.Play();
	}
}
