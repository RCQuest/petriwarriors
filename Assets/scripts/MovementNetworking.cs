using UnityEngine;
using System.Collections;

public class MovementNetworking : MonoBehaviour 
{
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition;
	private Vector3 syncEndPosition;

	void Update()
	{
		if (!this.GetComponent<NetworkView>().isMine)
		{
			SyncedMovement();
		}
	}
	
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		this.GetComponent<Rigidbody>().position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = this.GetComponent<Rigidbody>().position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = this.GetComponent<Rigidbody>().position;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = this.GetComponent<Rigidbody>().position;
		}
	}
	
}
