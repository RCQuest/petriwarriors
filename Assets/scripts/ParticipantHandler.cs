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
	public int faction;
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
			LockStepManager.Instance.AddAction(new SpawnUnit(spawnMode, 
			                                                 faction,
			                                                 0,
			                                                 start.x,
			                                                 start.y,
			                                                 splitRate,
			                                                 mutationRate,
			                                                 attackPotency,
			                                                 decayRate,
			                                                 direction.x,
			                                                 direction.y,
			                                                 speed,
			                                                 splitting
				));
		}
		else if(spawnMode==SpawnMode.DIRECTOR&&res.canSpawnWithCost(GameConfig.DIRECTORCOST))
		{
			LockStepManager.Instance.AddAction(new SpawnUnit(spawnMode, 
			                                                 faction,
			                                                 0,
			                                                 start.x,
			                                                 start.y,
			                                                 splitRate,
			                                                 mutationRate,
			                                                 attackPotency,
			                                                 decayRate,
			                                                 direction.x,
			                                                 direction.y,
			                                                 speed,
			                                                 splitting
			                                                 ));
		}
		else if(spawnMode==SpawnMode.SPECIAL&&res.canSpawnWithCost(GameConfig.SPECIALCOST)&&specialCharge>1.0f)
		{
			LockStepManager.Instance.AddAction(new SpawnUnit(spawnMode, 
			                                                 faction,
			                                                 0,
			                                                 start.x,
			                                                 start.y,
			                                                 splitRate,
			                                                 mutationRate,
			                                                 attackPotency,
			                                                 decayRate,
			                                                 direction.x,
			                                                 direction.y,
			                                                 speed,
			                                                 splitting
			                                                 ));
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