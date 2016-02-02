﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	private static Game instance = null;
	private int currentLevel = 1;
	List<DropZone> SolutionList = new List<DropZone>();
	//List<DropZone> BottomSolutionList = new List<DropZone>();
	public static Game Instance { get { return instance; } }
	public GameState gameState = GameState.PLAYING;
	
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);//keep object from being destroyed for use in to perserve game state in subsequent levels
		instance = this;

	}
	void OnLevelWasLoaded(int levelNum)
	{
		currentLevel=levelNum;
		Debug.Log("current level is: " + currentLevel);
		EventManager.TriggerEvent("NewLevel");
	}
	void Start()
	{
		Application.LoadLevel("Level1");//boot to first level after initializing
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
			if(gameState == GameState.PLAYING)
			{
				Invoke("Win",4f);
			}
			else if(gameState == GameState.RESTARTING)
			{
				Invoke("RestartGame",4f);
			}
			else if(gameState==GameState.QUITTING)
			{
				Invoke("QuitGame",4f);
			}
			 	
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
	public void RestartGame()
	{
		currentLevel=1;
		gameState= GameState.PLAYING;
		Application.LoadLevel(currentLevel);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
	void Win()
	{
		
		EventManager.TriggerEvent("LevelEnded");
		Application.LoadLevel(currentLevel+1);
	}
	
}
public enum GameState {RESTARTING,PLAYING,QUITTING};
