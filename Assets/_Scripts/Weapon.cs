using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public ParticleSystem fireEffect;
	public AudioSource shootSound;

	public Transform shootingPoint;
	public float shootRate;

	float lastShootTime;

	string layerName;

	public bool ShootBullet(string layerName, float delay)
	{
		bool ret = false;
		///no estamos en pausa
		if (Time.timeScale > 0)
		{
			//cuanto ha pasado
			float timeSinceLastShoot = Time.time - lastShootTime;

			//es menor que el rate
			if (timeSinceLastShoot >= shootRate)
			{
				//voy a disparar tomo el tiempo del disparo
				lastShootTime = Time.time;

				this.layerName = layerName;
				Invoke("FireBullet", delay);

				ret = true;
			}
			else
			{
				//si, no disparo
			}
		}
		return ret;
	}

	void FireBullet()
	{
		//sacamos la bala del pool
		GameObject bullet = ObjectPool.SharedInstance.GetFirstPooledObject();

		//ponemos la capa del bullet para que no colisione con nosotros mismos
		bullet.layer = LayerMask.NameToLayer(this.layerName);

		//posicionamos
		bullet.transform.position = shootingPoint.transform.position;
		bullet.transform.rotation = shootingPoint.transform.rotation;

		//activamos la bala
		bullet.SetActive(true);

		if (fireEffect != null)
			fireEffect.Play();

		if (shootSound != null)
			shootSound.Play();
	}
}
