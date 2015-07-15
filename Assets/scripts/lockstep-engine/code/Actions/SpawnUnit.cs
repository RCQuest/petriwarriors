using System;

[Serializable]
public class SpawnUnit : IAction
{
	int owningPlayer;
	int spawnID;
	SpawnMode mode;

	
	public SpawnUnit (int owningPlayer, int spawnID, SpawnMode mode) {
		this.owningPlayer = owningPlayer;
		this.spawnID = spawnID;
		this.mode = mode;
	}
	
	public void ProcessAction() {
		Spawner unit = SceneManager.Manager.CreateUnit(owningPlayer, spawnID);
		switch(mode)
		{
		case SpawnMode.DIRECTOR:
			unit.SpawnDirector();
			break;
		case SpawnMode.GRUNT:
			unit.SpawnGrunt();
			break;
		case SpawnMode.SPECIAL:
			unit.SpawnSpecial();
			break;
		default:
			unit.SpawnGrunt();
			break;
		}

	}

}
