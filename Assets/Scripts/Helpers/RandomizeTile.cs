using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//Class for randomizing tiles
public class RandomizeTile : MonoBehaviour {
	Text text;
	// Use this for initialization
	void Start () {
		RandomizeText();
	}
	
	void RandomizeText()
	{
		string st = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		char c = st[Random.Range(0,st.Length)];
		text = GetComponentInChildren<Text>();
		
		text.text = c.ToString().ToUpper();
	}
	
}
