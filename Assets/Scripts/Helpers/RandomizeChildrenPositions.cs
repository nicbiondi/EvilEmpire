using UnityEngine;
using System.Collections;

public class RandomizeChildrenPositions : MonoBehaviour {
	public GameObject[] randomizeElements;
	public GameObject ParentGo;
	private int passes = 2;
	
	void Start()
	{
		RandomizeChildrenOrder();
	}
	void RandomizeChildrenOrder()
	{
		
		int childCount = randomizeElements.Length;
		for (int i = 0; i < childCount * passes; i++) {
			Transform child = ParentGo.transform.GetChild(Random.Range(0,childCount));
			if(child.gameObject.activeSelf)
			{
				child.SetParent(null);
				child.transform.SetParent(ParentGo.transform);
			}
		}
	}
}
