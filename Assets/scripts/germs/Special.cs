using UnityEngine;
using System.Collections;

public class Special : MonoBehaviour 
{

	public Vector2 move=new Vector2(0.0f,0.0f);
	public float speed;
	public Resources res;
	private MAttributes attributes;
	
	public void create(Vector2 move,Resources res)
	{
		this.move=move;
		this.res=res;
	}
	
	void Start()
	{
		attributes=gameObject.GetComponent<MAttributes>();
	}
	
	void Update()
	{
		transform.position=new Vector2(transform.position.x+move.x*speed*Time.deltaTime,
		                               transform.position.y+move.y*speed*Time.deltaTime);
		if(transform.position.magnitude>GameConfig.PETRIRADIUS
		   ||attributes.health<0.0f) 
			die();
	}
	
	public void die()
	{
		res.incrementBy(GameConfig.SPECIALCOST);
		Network.Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log (other.tag);
		if(other.CompareTag("Core")&&!other.gameObject.GetComponent<MAttributes>().faction.Equals(attributes.faction))
		{
			other.gameObject.GetComponent<MAttributes>().health-=(GameConfig.COREHEALTH+1.0f);
		}
	}

}
