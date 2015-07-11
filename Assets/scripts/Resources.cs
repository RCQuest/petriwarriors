using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resources : MonoBehaviour 
{

	private int germRes;
	//public Text resText;

	public void increment()
	{
		germRes++;
		updateRes();
	}

	public void incrementBy(int amount)
	{
		germRes+=amount;
		updateRes();
	}

	public void decrement()
	{
		germRes--;
		updateRes();
	}

	public void decrementBy(int amount)
	{
		germRes-=amount;
		updateRes();
	}

	public void setRes(int set)
	{
		germRes=set;
		Debug.Log (set);
		updateRes();
	}

	private void updateRes()
	{
		//resText.text="Resource Level: "+germRes;
	}

	public bool canSpawnWithCost(int cost)
	{
		return cost<=germRes;
	}

	public int getRes()
	{
		return germRes;
	}
}
