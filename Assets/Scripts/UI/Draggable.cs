using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler{
	public Transform originalParent = null;
	public void OnBeginDrag(PointerEventData eventData)
	{
		originalParent = transform.parent;
		transform.SetParent(transform.parent.parent);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		EventManager.TriggerEvent("PickUpTile");
	}
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position;
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent(originalParent);
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		EventManager.TriggerEvent("DropTile");
	}
}
