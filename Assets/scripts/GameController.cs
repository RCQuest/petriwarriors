using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	void Start()
	{
		Application.runInBackground = true;
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
		{ 
			//Application.LoadLevel(0);
		}
	}

}
