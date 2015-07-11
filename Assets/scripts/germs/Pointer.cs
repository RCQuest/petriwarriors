using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour 
{

	public Vector2 pointDirection=new Vector2(0.0f,0.0f);
	public Resources res;
	private MAttributes attributes;
	private bool dead;

	public void create(Vector2 pointDirection,Resources res)
	{
		this.pointDirection=pointDirection;
		this.res=res;
	}

	void Start()
	{
		dead=false;
		attributes=gameObject.GetComponent<MAttributes>();
	}

	void Update()
	{
		if((transform.position.magnitude>GameConfig.PETRIRADIUS
		   //||Input.GetKeyDown(KeyCode.S)
		   ||attributes.health<0.0f)
		   &&!dead) 
			die();
	}

	public void die()
	{
		dead=true;

		res.setRes(res.getRes()+GameConfig.DIRECTORCOST);
		Network.Destroy(this.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Germ")&&other.gameObject.GetComponent<MAttributes>().faction.Equals(attributes.faction))
		{
			other.gameObject.GetComponent<Germ2>().move=pointDirection.normalized;
		}
	}
}
