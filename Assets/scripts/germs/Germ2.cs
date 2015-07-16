using UnityEngine;
using System.Collections;

public class Germ2 : MonoBehaviour 
{

	public float splitRate;
	public float splitTime;
	public float attackPotency;
	public float decayRate;
	//public float health;
	//public string faction;
	public MAttributes attributes;
	public GameObject childPrefab;
	public bool started;
	public float mutationRate;
	public Vector2 move;
	public float speed;
	public Resources res;

	public bool splitting;
	
	public void create(float splitRate, 
	                   float attackPotency, 
	                   float decayRate, 
	                   int faction,
	                   float mutationRate,
	                   //Transform parentGroup,
	                   Vector2 move,
	                   float speed,
	                   Resources res,
	                   bool splitting)
	{
		//this.transform.parent=parentGroup;
		this.res=res;
		this.speed=speed;
		this.move=move;
		this.splitRate = splitRate;
		this.attackPotency = attackPotency;
		this.decayRate = decayRate;
		this.mutationRate=mutationRate;
		splitTime = 1;
		started = true;
		attributes=gameObject.GetComponent<MAttributes>();
		attributes.faction = faction;
		this.splitting=splitting;
	}

	void Start()
	{
		attributes=gameObject.GetComponent<MAttributes>();
	}
	
	void Update () 
	{
		Vector2 newpos=move;//Random.insideUnitCircle+move;
		transform.position=new Vector2(transform.position.x+newpos.x*speed*Time.deltaTime,
		                               transform.position.y+newpos.y*speed*Time.deltaTime);
		if (started) 
		{
			splitTime -= splitRate*Time.deltaTime;
			attributes.health -= decayRate*Time.deltaTime;
			if (attributes.health <= 0.0f)
				die();
			else if (splitTime <= 0.0f && res.canSpawnWithCost(1) && splitting)
				split ();
		}
		if(transform.position.magnitude>GameConfig.PETRIRADIUS)//||Input.GetKeyDown(KeyCode.A))
		{
			die();
		}
	}

	public void switchSplit(bool ss)
	{
		splitting=ss;
	}

	public void die()
	{
		res.increment();
		Network.Destroy (this.gameObject);
	}
	
	private void split()
	{
		Vector2 childMove = new Vector2(move.x*Mathf.Cos (GameConfig.SPREADTHETA)-move.y*Mathf.Sin (GameConfig.SPREADTHETA),
		                                move.x*Mathf.Sin (GameConfig.SPREADTHETA)+move.y*Mathf.Cos (GameConfig.SPREADTHETA));
		if(move.magnitude>0.1f) move =  new Vector2(move.x*Mathf.Cos (-GameConfig.SPREADTHETA)-move.y*Mathf.Sin (-GameConfig.SPREADTHETA),
		                    						move.x*Mathf.Sin (-GameConfig.SPREADTHETA)+move.y*Mathf.Cos (-GameConfig.SPREADTHETA)).normalized;
		//Debug.Log ("SPREADANGLE: "+childMove+" "+move);
		if(childMove.magnitude<0.1f) childMove = childMove + Random.insideUnitCircle;
		LockStepManager.Instance.AddAction(new SpawnUnit(SpawnMode.GRUNT, 
		                                                 attributes.faction,
		                                                 0,
		                                                 transform.position.x,
		                                                 transform.position.y,
		                                                 splitRate,
		                                                 mutationRate,
		                                                 attackPotency,
		                                                 decayRate,
		                                                 childMove.x,
		                                                 childMove.y,
		                                                 speed,
		                                                 splitting
		                                                 ));
		splitTime=1;
		res.decrement();
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("COLLIDED!");

		if(other.CompareTag("Germ")&&!other.gameObject.GetComponent<MAttributes>().faction.Equals(attributes.faction))
		{
			//Debug.Log("GERMCOLLIDE:"+ other);
			Germ2 otherGerm = other.gameObject.GetComponent<Germ2>();
			//Debug.Log("DIFF!");
			float magA = otherGerm.move.magnitude;
			float magB = move.magnitude;
			if(magA<magB)
			{
				move.x=(otherGerm.move.x*(1.0f/(magB))+move.x)*0.5f;
				move.y=(otherGerm.move.y*(1.0f/(magB))+move.y)*0.5f;
			}
			else
			{
				move.x=(otherGerm.move.x+move.x*(1.0f/(magA)))*0.5f;
				move.y=(otherGerm.move.y+move.y*(1.0f/(magA)))*0.5f;
			}
		}
	}

	void OnTriggerStay(Collider other)
	{

		if((other.CompareTag("Germ")||other.CompareTag("Pointer")||other.CompareTag("Core")||other.CompareTag("Special"))
		   &&!other.gameObject.GetComponent<MAttributes>().faction.Equals(attributes.faction))
		{
			other.gameObject.GetComponent<MAttributes>().health-=attackPotency;
		}
	}
}
