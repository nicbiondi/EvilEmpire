using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	private static Game instance = null;
	List<DropZone> SolutionList = new List<DropZone>();
	//List<DropZone> BottomSolutionList = new List<DropZone>();
	public static Game Instance { get { return instance; } }
	
	// Use this for initialization
	void Awake () {
		instance = this;

	}
	
	void OnEnable()
	{
		EventManager.StartListening("CheckGameState", CheckGameState);
	}
	
	void OnDisable()
	{
		EventManager.StopListening("CheckGameState", CheckGameState);
	}
	
	void CheckGameState()
	{
		Debug.Log("check game state");
		if(isSolutionComplete())
		{
			EventManager.TriggerEvent("Celebrate");
			Invoke("RestartLevel",4f);
			Debug.Log("we won!");
		}
	}
	
	bool isSolutionComplete()
	{
		//if there are no slots left, then we won!
		foreach(DropZone dropZone in SolutionList)
	 		if(dropZone.ValidDropArea)
	 			return false;
	
		return true;
	}
	public void AddSlot(DropZone dropzone)
	{
		SolutionList.Add (dropzone);
	}
	void RestartLevel()
	{
		Application.LoadLevel(0);
	}
	
}
