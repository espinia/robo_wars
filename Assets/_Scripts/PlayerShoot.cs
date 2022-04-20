using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{    
    //con _ porque es una componente propia
    Animator _animator;

    public int bulletsAmount;

    public Weapon weapon;

	private void Awake()
	{
        //si es mi propia componente es posible
        // si es de otro objeto no por el orden de ejecuci�n
        _animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        //bot�n del mouse izquierdo
        // manteniendo pulsado se queda disparando
        //Input.GetKey(KeyCode.Mouse0)
        //bala por bala
        //Input.GetKeyDown(KeyCode.Mouse0)
        if (Input.GetKey(KeyCode.Mouse0) && Time.timeScale > 0)
        {
            _animator.SetBool("ShotBulletBool", true);
            if (bulletsAmount > 0 && weapon.ShootBullet("Player Bullet",0.25f))
            {
                bulletsAmount--;
                //correcci�n para que no queden negativos
                if (bulletsAmount < 0)
                {
                    bulletsAmount = 0;
                }

            }
            /*ANTES REFACTORIZAR
                //versi�n con trigger
                //en editor poner la condici�n de entradaa a la animaci�n en triger
                //la de salida no lleva nada
                //_animator.SetTrigger("ShotBullet");
                _animator.SetBool("ShotBulletBool", true);

                if (bulletsAmount > 0)
                {
                    Invoke("FireBullet", 0.2f);
                }
                else
                {
                    //TODO: podemos reproducir un sonido de arma vac�a
                }*/
        }
        else
        {
            _animator.SetBool("ShotBulletBool", false);
        }
    }

    /*ANTES REFACTORIZAR
    void FireBullet()
	{
        //mejor se saca del pool            
        GameObject bullet = ObjectPool.SharedInstance.GetFirstPooledObject();

        //colocamos el layer
        bullet.layer = LayerMask.NameToLayer("Player Bullet");

        //se ajusta la posicion y rotaci�n, da versatilidad
        bullet.transform.position = shootingPoint.transform.position;
        bullet.transform.rotation = shootingPoint.transform.rotation;

        //se activa 
        bullet.SetActive(true);

        fireEffect.Play();
        shootSound.Play();
        //si lo pongo como parte del player funciona mejor sin instanciar
        //Instantiate(shootSound, transform.position, transform.rotation).Play();

        bulletsAmount--;
        //correcci�n para que no queden negativos
        if (bulletsAmount < 0)
        {
            bulletsAmount = 0;
        }
    }*/
}
