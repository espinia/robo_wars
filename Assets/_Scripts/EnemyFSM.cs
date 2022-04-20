using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Sight))]
public class EnemyFSM : MonoBehaviour
{
	// ir a la base, atacar base, perseguir player, atacar player
	//el atack podria ser solo uno ya que solo cambia el objeto a atacar
	public enum EnemyState { GoToBase, AttackBase, ChasePlayer, AttackPlayer }

	//estado actual
	public EnemyState currentState;

	//variable para script de visión
	private Sight _sight;

	//transform para posición de la base
	private Transform baseTransform;

	//distancia minima para atacar
	public float baseAttackDistance;

	//distancia minima para atacar
	public float playerAttackDistance;

	//Nav Mesh agent
	private NavMeshAgent agent;

	//para las animaciones
	private Animator animator;

	//para disparar
	public Weapon weapon;

	private void Awake()
	{
		_sight = GetComponent<Sight>();
		agent = GetComponentInParent<NavMeshAgent>();
		animator = GetComponentInParent<Animator>();
	}

	private void Start()
	{
		baseTransform = GameObject.FindWithTag("Base").transform;
	}

	private void Update()
	{
		switch (currentState)
		{
			case EnemyState.GoToBase:
				GoToBase();
				break;
			case EnemyState.AttackBase:
				AttackBase();
				break;
			case EnemyState.ChasePlayer:
				ChasePlayer();
				break;
			case EnemyState.AttackPlayer:
				AttackPlayer();
				break;
			default:
				break;
		}
	}

	void GoToBase()
	{
		//deja de disparar si irá a la base
		animator.SetBool("ShotBulletBool", false);
		//print("GoToBase");
		//se pone a andar el agente
		agent.isStopped = false;
		//marcamos destino
		agent.SetDestination(baseTransform.position);

		//tenemos un target?
		if(_sight.detectedTarget!=null)
		{
			//pasamos a perseguir
			currentState = EnemyState.ChasePlayer;
			return;
		}

		//calculando distancia a la base
		float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);

		if(distanceToBase<baseAttackDistance)
		{
			currentState = EnemyState.AttackBase;
		}

	}

	void AttackBase()
	{
		//print("AttackBase");

		//detenemos el movimiento
		agent.isStopped = true;

		//mirar a la base
		LookAt(baseTransform.position);

		//disparamos a objetivo		
		ShootTarget();
	}

	void ChasePlayer()
	{
		//deja de disparar si ya va a perseguir al jugador
		animator.SetBool("ShotBulletBool", false);

		//print("ChasePlayer");

		//tenemos un target en visión?
		if(_sight.detectedTarget==null)
		{
			//no, vamos a la base
			currentState = EnemyState.GoToBase;
			return;
		}

		//si nos detenemos 
		agent.isStopped = false;
		//seguimos al target
		agent.SetDestination(_sight.detectedTarget.transform.position);

		//calculando distancia al jugador
		//que se obtiene de la vision
		//aunque habia dicho que del collider no usemos transform
		//dice que es por los pivotes
		float distanceToPlayer = Vector3.Distance(transform.position, _sight.detectedTarget.transform.position);

		if (distanceToPlayer < playerAttackDistance)
		{
			currentState = EnemyState.AttackPlayer;
		}
	}

	void AttackPlayer()
	{
		//print("AttackPlayer");
		//detener
		agent.isStopped = true;

		//hay aún un enemigo en la visión
		if(_sight.detectedTarget==null)
		{
			//no, ir a la base
			currentState = EnemyState.GoToBase;
			return;
		}

		//mirar al player
		LookAt(_sight.detectedTarget.transform.position);

		//disparamos
		ShootTarget();

		//calculando distancia al jugador
		//que se obtiene de la vision		
		float distanceToPlayer = Vector3.Distance(transform.position, _sight.detectedTarget.transform.position);

		//se multiplica por una tolerancia para que no haya saltos y que no esté el jugador 
		//saltando dentro y fuera
		if (distanceToPlayer > playerAttackDistance * 1.1f)
		{
			currentState = EnemyState.ChasePlayer;
		}
	}
	
	void ShootTarget()
	{
		if (weapon.ShootBullet("Enemy Bullet",0))
		{
			animator.SetBool("ShotBulletBool", true);
		}
	}

	void LookAt(Vector3 targetPos)
	{
		//rotamos para que mire
		//calculamos el vector normalizado
		Vector3 directionToLook = Vector3.Normalize(targetPos - transform.position);

		//cuidado porque puede rotar incorrectamente
		//así que a la Y no le hacemos caso
		directionToLook.y = 0;

		//pero el que hay que rotar es el padre, porque esto está en la IA
		transform.parent.forward = directionToLook;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
	}
}
