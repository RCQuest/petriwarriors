//The MIT License (MIT)

//Copyright (c) 2013 Clinton Brennan

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.

/// <summary>
/// Scene manager. This is intended to be a singleton class.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SceneManager : MonoBehaviour 
{
	
	public static SceneManager Manager;
	public GameObject playerPrefab;
	
	public List<IHasGameFrame> GameFrameObjects;
	public GameObject gruntPrefab;
	public GameObject specialPrefab;
	public GameObject directorPrefab;

    public Resources tempResources;
	
	void Awake() {
		SetupLog ();
		Manager = this;
		GameFrameObjects = new List<IHasGameFrame>();
	}
	
	public Spawner CreateUnit(int owningPlayer,
	                          int spawnID,
	                          float startX,
	                          float startY,
	                          float splitRate,
	                          float mutationRate,
	                          float attackPotency,
	                          float decayRate,
	                          float directionX,
	                          float directionY,
	                          float speed,
	                          bool splitting)
	{

		return new Spawner(startX,
		                   startY,
		                   splitRate,
		                   mutationRate,
		                   attackPotency,
		                   decayRate,
		                   owningPlayer,
		                   directionX,
		                   directionY,
		                   speed,
		                   splitting);
	}

	public Resources GetRes(int playerID)
	{
		return tempResources;
	}

	public GameObject GetGruntPrefab()
	{
		return gruntPrefab;
	}

	public GameObject GetDirectorPrefab()
	{
		return directorPrefab;
	}

	public GameObject GetSpecialPrefab()
	{
		return specialPrefab;
	}

	public void CreatePlayer()
	{

	}

	public void ResetSpecialCharge(int playerID)
	{

	}

	/// <summary>
	/// Reads in the log configuration for log4net.
	/// </summary>
	void SetupLog() {
		
		///object obj = Resources.Load ("logConfig");
		///if(obj != null) {
		///	TextAsset configText = obj as TextAsset;
		///	if(configText != null) {
		///		MemoryStream memStream = new MemoryStream(configText.bytes);
		///		XmlConfigurator.Configure(memStream);	
		///	}
		///}
	}
}
