using UnityEngine;
using System.Collections;

public class ScrollQuad : MonoBehaviour {

	public float speed = 100f;
	public float mod = 2;
	public enum ScrollDirection{horrizontal,vertical};
	public ScrollDirection scrollDirection = ScrollDirection.horrizontal;
	public int sortingOrder = 0;
	private Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.sortingOrder = sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time *speed;
		if(scrollDirection == ScrollDirection.horrizontal)
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2((Time.time *speed)%mod, 0f);
		else
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0f, (Time.time *speed)%mod);
	}
}

