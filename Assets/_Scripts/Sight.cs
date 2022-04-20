using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    //distancia m�xima de visi�n
    public float distance;

    //�ngulo m�ximo de la visi�n
    public float angle;

    //para detectar player y base, son los objetivos
    public LayerMask targetLayers;

    //objetos a evitar
    public LayerMask obstacleLayers;

    //target detectado para atacar
    public Collider detectedTarget;

    private Collider[] colliders;

    private void Update()
	{
        //toma la posici�n del objeto IA y el radio de visi�n
        //y qu� m�scara tienen los objetos que quiero detectar
        //hay m�s versiones
        //devuelve un array de colliders de los objetos dentro de la esfera
        //los detectables tienen que tener collider
        //cuidado podr�a detectarse a s� mismo
        colliders = Physics.OverlapSphere(transform.position, distance, targetLayers);

        //se limpia la variable
        detectedTarget = null;

        //FILTRO DE DISTANCIA OK EN ESTOS OBJETOS
        foreach (Collider collider in colliders)
        {
            //calcular direcci�n al collider detectado
            //del collider se usa bounds por que solo es la envoltura y su centro
            //si se usa transform position vendr� el pivote, por eso es mejor el
            //Centro geom�trico del collider detectado
            Vector3 directionToCollider = Vector3.Normalize(collider.bounds.center - transform.position);

            //funci�n para obtener el �ngulo entre vectores
            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);

            if (angleToCollider < angle)
			{
                //objeto dentro de �ngulo de visi�n
                //recordar que el �ngulo de visi�n es el doble del configurado, como est� el c�digo

                //Linecast traza una l�nea
                //desde posici�n inicial
                //a otra posici�n
                //y si pertenece a cierta layerMask

                //devuelve true si hay algo, y false si no hay nada
                //if(!Physics.Linecast(transform.position,
                //    collider.bounds.center,
                //    obstacleLayers))
                //esta versi�n puede tener par�metro de salida, el hit
                //puede venir un objeto en hit perteneciente a la capa obstacle layer
                if (!Physics.Linecast(transform.position,
                     collider.bounds.center,
                     out RaycastHit hit,
                     obstacleLayers))
                {
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    //referencia a �ltimo objetivo detectado
                    detectedTarget = collider;
                    break;
                }
                else
                {
                    //hit trae algo
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }

			}
		}
        /*
        for (int i=0;i<colliders.Length;i++)
		{
            Collider collider = colliders[i];
            //procesar
		}*/
	}

	private void OnDrawGizmos()
	{
        //cambiamos el color
        //se puede con new Color definir uno
        Gizmos.color = Color.red;
        //dibuja la esfera
        Gizmos.DrawWireSphere(transform.position, distance);

        //calculamos los vectores para dibujar un rayo
        //se rotar� el vector forward usando quaterniones tanto como define el �ngulo

        Gizmos.color = Color.magenta;
        //se obtiene el quaternion y se  multiplica por forward
        Vector3 rightDir=Quaternion.Euler(0,angle,0)*transform.forward;
        Vector3 leftDir =Quaternion.Euler(0, -angle, 0) * transform.forward;

        //dibujamos los rayos y le damos la dimensi�n adecuada
        Gizmos.DrawRay(transform.position, rightDir*distance);
        Gizmos.DrawRay(transform.position, leftDir * distance);
    }

}
