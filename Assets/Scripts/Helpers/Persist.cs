using UnityEngine;
using System.Collections;

public class Persist : MonoBehaviour {

	void Awake () {
		DontDestroyOnLoad(gameObject);//keep object from being destroyed for use in to perserve game state in subsequent levels
	}

}
