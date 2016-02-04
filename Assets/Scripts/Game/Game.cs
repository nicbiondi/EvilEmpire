using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	private static Game instance = null;
	public int currentLevel = 1;
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
		
		Debug.Log("current level is: levelNumber: " + currentLevel + " levelName:" + Application.loadedLevelName);
		EventManager.TriggerEvent("NewLevel");
	}
	void Start()
	{
		Application.LoadLevel("Tutorial");//boot to first level after initializing
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
		currentLevel=2;
		gameState= GameState.PLAYING;
		Application.LoadLevel("Level1");
	}
	public void QuitGame()
	{
		Application.Quit();
	}
	void Win()
	{
		currentLevel++;
		EventManager.TriggerEvent("LevelEnded");
		
		if(currentLevel>=6)//if you have completed the last level, load the win screen
			Application.LoadLevel("Win");
		else
			Application.LoadLevel(1);
	}
	
}
public enum GameState {RESTARTING,PLAYING,QUITTING};
