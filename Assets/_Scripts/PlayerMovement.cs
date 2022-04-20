using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    //F = ma
    [Tooltip("Fuerza del personaje en N/s")]
    [Range(0, 1000)]
    public float speed;

    [Tooltip("Rotación de rotación del personaje en N/s")]
    [Range(0, 360)]
    public float rotationSpeed;

    private Rigidbody _rb;

    private Animator _animator;

    private void Start()
    {
        //invisible
        Cursor.visible = false;
        //bloquear el cursor enmedio de la pantalla
        //para no poder darle clic afuera
        Cursor.lockState = CursorLockMode.Locked;

        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical);
        //cinematicos
        //incr D = V* incr t
        float distancia = speed * Time.deltaTime;
        //transform.Translate(distancia * dir.normalized);
        _rb.AddRelativeForce(distancia * dir.normalized);

        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        //cinematicos
        float angle = rotationSpeed * Time.deltaTime;
        //transform.Rotate(0, angle * mouseX, 0);
        _rb.AddRelativeTorque(0, angle * mouseX, 0);

        _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        /*
        _animator.SetFloat("MovX", horizontal);
        _animator.SetFloat("MovY", vertical);

        //con control de teclas o botones
        if(Input.GetKey(KeyCode.LeftShift))
		{
            _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        }
        else
		{
            if(Mathf.Abs(horizontal)<0.01f && Mathf.Abs(vertical) < 0.01f)
			{
                //no se está moviendo 
                _animator.SetFloat("Velocity",0);
            }
			else
			{
                _animator.SetFloat("Velocity", 0.15f);
            }            
        }*/

        //directo
        //_animator.SetFloat("Velocity", _rb.velocity.magnitude);

        /*Con condiciones adaptadas
        if (_rb.velocity.magnitude > 5.0f)
        {
            _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        }
		else
		{
            _animator.SetFloat("Velocity", 0);
        }*/
    }
}
