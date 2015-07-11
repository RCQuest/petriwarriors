using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Core : MonoBehaviour 
{

	private MAttributes attributes;

	public Text healthText;

	void Update()
	{
		if(attributes.health<0.0f) die();
	}
	
	public void die()
	{
		Network.Destroy(this.gameObject);
	}

	void Start()
	{
		attributes=gameObject.GetComponent<MAttributes>();
	}

	void OnTriggerStay(Collider other)
	{
		if((other.CompareTag("Germ")||other.CompareTag("Pointer")||other.CompareTag("Core"))
		   &&!other.gameObject.GetComponent<MAttributes>().faction.Equals(attributes.faction))
		{
			other.gameObject.GetComponent<MAttributes>().health-=1.0f;
			healthText.text="Core Health: "+attributes.health*(100/GameConfig.COREHEALTH)+"%";
		}

	}
}
