using UnityEngine;
using System.Collections;

public class PlayerNetworking : MonoBehaviour 
{
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{

		}
		else
		{

		}
	}
}
