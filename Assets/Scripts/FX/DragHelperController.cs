using UnityEngine;
using System.Collections;

public class DragHelperController : MonoBehaviour {

	public Transform startspot;
	public Transform endspot;
	public float seconds = 0.5f;
	public float initialDelay = 1f;
	public float subsequentDelay = 0.6f;
	
	private bool shouldAnimate = true;
	
	void Start () {
		Invoke("StartMoving", initialDelay);
		
	}
	void OnEnable()
	{
		EventManager.StartListening("PickUpTile",StopAnimaton);//start listening
	}

	void OnDisable()
	{
		EventManager.StopListening("PickUpTile", StopAnimaton);//stop listening
	}
	void StopAnimaton()
	{
		gameObject.SetActive(false);
		shouldAnimate=false;
		
	}

	void StartMoving()
	{
		transform.position = startspot.position;
		StartCoroutine(SmoothMove(startspot.position, endspot.position, seconds));
	}
	private IEnumerator SmoothMove (Vector3 startpos, Vector3 endpos, float seconds) {
		float t = 0.0f;
		while (t <= 1.0) {
			t += Time.deltaTime/seconds;
			transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, t));
			if(!shouldAnimate)
				return false;
			yield return null;
		}
		Invoke("StartMoving",subsequentDelay);
	}
}
