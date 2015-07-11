using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum SpawnMode
{
	GRUNT,
	DIRECTOR,
	SPECIAL
}

public enum Perk
{
	SPEED,
	ATTACK,
	HEALTH
}

public class ParticipantHandler : MonoBehaviour 
{
	public float splitRate;
	public float attackPotency;
	public float decayRate;
	public string faction;
	public float mutationRate;
	public float speed;
	public Resources res;

	public Core core;
	public GameObject gruntPrefab;
	public GameObject pointerPrefab;
	public GameObject specialPrefab;

	//public Text typeText;
	private SpawnMode spawnMode;
	//public Text perkText;
	private ArrayList perks;
	//public Text splittingText;
	public bool splitting;
	//public Text specialText;
	private float specialCharge;
	
	public void setMode(SpawnMode mode)
	{
		spawnMode=mode;
		//typeText.text="Spawn Mode: "+mode;
	}

	public void addPerk(Perk perk)
	{
		perks.Add(perk);
		//perkText.text=perkText.text+", "+perk;
	}

	public void removePerk(Perk perk)
	{
		perks.Add(perk);
		string text = "";
		for(int i=0;i<perks.Count;i++)
		{
			text=text+perks[i];
		}
		//perkText.text=text;
	}

	void Start()
	{
		specialCharge=0.0f;
		setMode(SpawnMode.GRUNT);
		perks=new ArrayList();
		splitting=true;
		addPerk(Perk.SPEED);
		res.setRes (GameConfig.STARTINGRES);
		//Debug.Log (res.getRes());
	}

	void Update ()
	{
		specialUpdate();
	}

	public void spawnEntity(Vector3 direction, Vector3 start)
	{
		//Debug.Log(res.getRes());
		//if(res.canSpawnWithCost(GameConfig.GRUNTCOST)) Debug.Log ("spawned");
		if(spawnMode==SpawnMode.GRUNT&&res.canSpawnWithCost(GameConfig.GRUNTCOST)&&(core.transform.position-start).magnitude<GameConfig.SPAWNDISTANCE)
		{
			GameObject germ = (Network.Instantiate (gruntPrefab, 
			              new Vector2(start.x,start.y), 
			              Quaternion.identity,0)
			                   as GameObject);
			germ
				.GetComponent<Germ2> ()
					.create (Mathf.Abs (splitRate+((Random.value-0.5f)*mutationRate)),
					         Mathf.Abs (attackPotency+((Random.value-0.5f)*mutationRate)),
					         Mathf.Abs (decayRate+((Random.value-0.5f)*0.001f)),
					         faction,
					         mutationRate*0.9f,
					         new Vector2(direction.x,direction.y),
					         speed,
					         res,
					         splitting);
			
			res.decrementBy(GameConfig.GRUNTCOST);
			//germ.transform.parent = transform.parent;
			germ.GetComponent<Germ2> ().childPrefab=this.gruntPrefab;
		}
		else if(spawnMode==SpawnMode.DIRECTOR&&res.canSpawnWithCost(GameConfig.DIRECTORCOST))
		{
			GameObject germ = (Network.Instantiate(pointerPrefab,
			             new Vector2(start.x,start.y),
			             Quaternion.identity,0)
			                   as GameObject);
			germ.GetComponent<Pointer>()
					.create(new Vector2(direction.x,direction.y),
					        res);
			//Debug.Log ("POINTER");
			res.decrementBy(GameConfig.DIRECTORCOST);
			//germ.transform.parent = transform.parent;
		}
		else if(spawnMode==SpawnMode.SPECIAL&&res.canSpawnWithCost(GameConfig.SPECIALCOST)&&specialCharge>1.0f)
		{
			GameObject[] grunts = GameObject.FindGameObjectsWithTag ("Germ");
			foreach(GameObject grunt in grunts)
			{
				if(grunt.GetComponent<MAttributes>().faction.Equals (faction)) grunt.GetComponent<Germ2>().die();
			}
			GameObject[] pointers = GameObject.FindGameObjectsWithTag ("Pointer");
			foreach(GameObject pointer in pointers)
			{
				if(pointer.GetComponent<MAttributes>().faction.Equals (faction)) pointer.GetComponent<Pointer>().die();
			}
			specialCharge=0.0f;
			GameObject germ = (Network.Instantiate(specialPrefab,
			             new Vector2(start.x,start.y),
			             Quaternion.identity,0)
			                   as GameObject);
			germ.GetComponent<Special>()
					.create(new Vector2(direction.x,direction.y),
					        res);
			res.decrementBy(GameConfig.SPECIALCOST);
			//germ.transform.parent = transform.parent;
		}
		else
		{}
	}
	public void removeAllGrunts()
	{
		GameObject[] grunts = GameObject.FindGameObjectsWithTag ("Germ");
		foreach(GameObject grunt in grunts)
		{
			if(grunt.GetComponent<MAttributes>().faction.Equals (faction)) grunt.GetComponent<Germ2>().die();
		}
	}
	public void removeAllPointers()
	{
		GameObject[] grunts = GameObject.FindGameObjectsWithTag ("Pointer");
		foreach(GameObject grunt in grunts)
		{
			if(grunt.GetComponent<MAttributes>().faction.Equals (faction)) grunt.GetComponent<Pointer>().die();
		}
	}

	public void toggleSplitting()
	{
		splitting = !splitting;
		GameObject[] grunts = GameObject.FindGameObjectsWithTag ("Germ");
		foreach(GameObject grunt in grunts)
		{
			if(grunt.GetComponent<MAttributes>().faction.Equals (faction)) grunt.GetComponent<Germ2>().switchSplit(splitting);
		}
		//splittingText.text="Splitting: "+splitting;
	}

	public void toggleSplitting(bool toggle)
	{
		splitting = toggle;
		GameObject[] grunts = GameObject.FindGameObjectsWithTag ("Germ");
		foreach(GameObject grunt in grunts)
		{
			if(grunt.GetComponent<MAttributes>().faction.Equals (faction)) grunt.GetComponent<Germ2>().switchSplit(splitting);
		}
		//splittingText.text="Splitting: "+splitting;
	}

	public void specialUpdate()
	{
		specialCharge+=res.getRes()*(GameConfig.SPECIALRATE/GameConfig.STARTINGRES)*Time.deltaTime;
		//if(specialCharge>1.0f) specialText.text="Special: 100%";
		//else specialText.text="Special: "+(specialCharge*100.0f).ToString()+"%";
	}
}