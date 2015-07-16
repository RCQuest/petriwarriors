using System;

[Serializable]
public class SpawnUnit : IAction
{
	private SpawnMode mode;
	private int owningPlayer;
	private int spawnID;
	private float startX;
	private float startY;
	private float splitRate;
	private float mutationRate;
	private float attackPotency;
	private float decayRate;
	private int faction;
	private float directionX;
	private float directionY;
	private float speed;
	private bool splitting;


	
	public SpawnUnit (SpawnMode mode, 
	                  int   owningPlayer,
	                  int   spawnID,
	                  float startX,
	                  float startY,
	                  float splitRate,
	                  float mutationRate,
	                  float attackPotency,
	                  float decayRate,
	                  float directionX,
	                  float directionY,
	                  float speed,
	                  bool splitting) {
		this.mode=mode; 
		this.owningPlayer=owningPlayer;
		this.spawnID=spawnID;
		this.startX=startX;
		this.startY=startY;
		this.splitRate=splitRate;
		this.mutationRate=mutationRate;
		this.attackPotency=attackPotency;
		this.decayRate=decayRate;
		this.directionX=directionX;
		this.directionY=directionY;
		this.speed=speed;
		this.splitting=splitting;
	}
	
	public void ProcessAction() {
		Spawner unit = SceneManager.Manager.CreateUnit(owningPlayer,
		                                               spawnID,
		                                               startX,
		                                               startY,
		                                               splitRate,
		                                               mutationRate,
		                                               attackPotency,
		                                               decayRate,
		                                               directionX,
		                                               directionY,
		                                               speed,
		                                               splitting);
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
