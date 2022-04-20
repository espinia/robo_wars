using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    //distancia máxima de visión
    public float distance;

    //ángulo máximo de la visión
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
        //toma la posición del objeto IA y el radio de visión
        //y qué máscara tienen los objetos que quiero detectar
        //hay más versiones
        //devuelve un array de colliders de los objetos dentro de la esfera
        //los detectables tienen que tener collider
        //cuidado podría detectarse a sí mismo
        colliders = Physics.OverlapSphere(transform.position, distance, targetLayers);

        //se limpia la variable
        detectedTarget = null;

        //FILTRO DE DISTANCIA OK EN ESTOS OBJETOS
        foreach (Collider collider in colliders)
        {
            //calcular dirección al collider detectado
            //del collider se usa bounds por que solo es la envoltura y su centro
            //si se usa transform position vendrá el pivote, por eso es mejor el
            //Centro geométrico del collider detectado
            Vector3 directionToCollider = Vector3.Normalize(collider.bounds.center - transform.position);

            //función para obtener el ángulo entre vectores
            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);

            if (angleToCollider < angle)
			{
                //objeto dentro de ángulo de visión
                //recordar que el ángulo de visión es el doble del configurado, como está el código

                //Linecast traza una línea
                //desde posición inicial
                //a otra posición
                //y si pertenece a cierta layerMask

                //devuelve true si hay algo, y false si no hay nada
                //if(!Physics.Linecast(transform.position,
                //    collider.bounds.center,
                //    obstacleLayers))
                //esta versión puede tener parámetro de salida, el hit
                //puede venir un objeto en hit perteneciente a la capa obstacle layer
                if (!Physics.Linecast(transform.position,
                     collider.bounds.center,
                     out RaycastHit hit,
                     obstacleLayers))
                {
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    //referencia a último objetivo detectado
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
        //se rotará el vector forward usando quaterniones tanto como define el ángulo

        Gizmos.color = Color.magenta;
        //se obtiene el quaternion y se  multiplica por forward
        Vector3 rightDir=Quaternion.Euler(0,angle,0)*transform.forward;
        Vector3 leftDir =Quaternion.Euler(0, -angle, 0) * transform.forward;

        //dibujamos los rayos y le damos la dimensión adecuada
        Gizmos.DrawRay(transform.position, rightDir*distance);
        Gizmos.DrawRay(transform.position, leftDir * distance);
    }

}
