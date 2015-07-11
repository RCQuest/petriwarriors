using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AITrait
{
	SHIELD
}

public class AIModule : MonoBehaviour 
{
	public List<AITrait> traits;
	public ParticipantHandler spawner;
	public Core core;
	public float speed;

	public bool paused;

	void Start()
	{
		paused=false;
	}

	void Update()
	{
		if(spawner.res.getRes ()>=30&&traits.Contains(AITrait.SHIELD)&&!paused)
		{
			StartCoroutine(spawnShield(30));
		}
		if(core==null)
		{
			paused=true;
		}
	}

	IEnumerator spawnShield(int desnity)
	{
		paused=true;
		spawner.setMode(SpawnMode.GRUNT);
		spawner.toggleSplitting(false);
		for(int i = 0; i<=30; i++)
		{
			spawner.spawnEntity(new Vector3(0.0f,0.0f,0.0f),
			                    new Vector3(core.transform.position.x+(2*Mathf.Cos (2*Mathf.PI*(i/30.0f))),
			            core.transform.position.y+(2*Mathf.Sin (2*Mathf.PI*(i/30.0f))),
			            0.0f)
			                    );
			yield return new WaitForSeconds(speed/10.0f);
			
		}
		paused=false;
	}
}
