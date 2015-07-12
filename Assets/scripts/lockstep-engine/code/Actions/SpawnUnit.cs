using System;

[Serializable]
public class SpawnUnit : IAction
{
	int owningPlayer;
	int buildingID;
	
	public SpawnUnit (int owningPlayer, int buildingID) {
		this.owningPlayer = owningPlayer;
		this.buildingID = buildingID;
	}
	
	public void ProcessAction() {
		//<<>> b = SceneManager.Manager.<<>>(owningPlayer, buildingID);
		//b.SpawnUnit();
	}

}
