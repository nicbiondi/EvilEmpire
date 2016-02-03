using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler{
	public Transform originalParent = null;
	CanvasGroup canvasGroup;
	bool canMove = true;

	public bool CanMove {
		get {
			return canMove;
		}
		set {
			canMove = value;
		}
	}

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		originalParent = transform.parent;
		transform.SetParent(transform.parent.parent);
		SetRaycastBlocking(false);
		EventManager.TriggerEvent("PickUpTile");
	}
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position;
		Debug.Log("eventData.position;" +eventData.position);
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent(originalParent);
		if(canMove)
			SetRaycastBlocking(true);
		EventManager.TriggerEvent("DropTile");
	}
	public void SetRaycastBlocking(bool isBlocking)
	{
		canvasGroup.blocksRaycasts =isBlocking;
	}
}
