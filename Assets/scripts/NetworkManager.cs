using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	private const string typeName = "GameName";
	private const string gameName = "RoomName";
	private HostData[] hostList;
	public GameObject playerPrefab;
	
	private void SpawnPlayer(Vector3 pos)
	{
		Network.Instantiate(playerPrefab, pos, Quaternion.identity, 0);
	}
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	private void StartServer()
	{
		Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);

	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	void OnServerInitialized()
	{
		SpawnPlayer(new Vector3(0f,7f,0f));
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer(new Vector3(0f,-7f,0f));
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}
}
