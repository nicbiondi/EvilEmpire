using UnityEngine;
using System.Collections;

//this class is used for loading up initilizer when starting game from level scenes.  This is just a nice-to-have that makes developing levels a bit quicker if added to level scenes
public class LoadHelper : MonoBehaviour {
	
	Game game;
	void Awake () {
	
		//look for Gameobject, if you don't find it then the game was loaded up out of order.. so load up the initializer
		game = Game.Instance;	
		if(!game)
			Application.LoadLevel("Initializer");
	}
	
}
