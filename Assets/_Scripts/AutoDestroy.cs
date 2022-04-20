using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Tooltip("Tiempo después del que se destruye el objeto")]
    public float delay;
    
    void OnEnable()
    {
        //Destroy(gameObject,delay);
        //para balas regresan al pool
        Invoke("HideObject", delay);
    }

    void HideObject()
	{
        gameObject.SetActive(false);
	}
}
