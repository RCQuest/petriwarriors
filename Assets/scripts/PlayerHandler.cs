using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHandler : MonoBehaviour 
{
	public ParticipantHandler spawner;

	private bool dragging;
	private float distance=0.0f;
	
	private Vector3 start;
	private Vector3 direction;
	
	// Update is called once per frame
	void Update ()
	{
		if (this.GetComponentInParent<NetworkView>().isMine)
		{
			inputMovement();
		}

	}
	private void inputMovement()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 rayPoint = ray.GetPoint(distance);
		if(Input.GetMouseButtonDown(0)&&!dragging)
		{
			distance = Vector3.Distance(transform.position, Camera.main.transform.position);
			start=rayPoint;
			dragging = true;
		}
		if(dragging&&Input.GetMouseButtonUp(0))
		{
			if(!Input.GetKey(KeyCode.LeftShift)) direction=(rayPoint-start).normalized;
			else direction=Vector3.zero;
			
			spawner.spawnEntity(direction,start);
			
			dragging=false;
		}
		if (Input.GetKeyDown(KeyCode.E)&&!dragging)
		{
			spawner.setMode(SpawnMode.SPECIAL);
		}
		if (Input.GetKeyDown(KeyCode.W)&&!dragging)
		{
			spawner.setMode(SpawnMode.DIRECTOR);
		}
		if (Input.GetKeyDown(KeyCode.Q)&&!dragging)
		{
			spawner.setMode(SpawnMode.GRUNT);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			spawner.removeAllGrunts();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			spawner.removeAllPointers();
		}
		if(Input.GetMouseButtonDown(1))
		{
			spawner.toggleSplitting();
		}
	}
}