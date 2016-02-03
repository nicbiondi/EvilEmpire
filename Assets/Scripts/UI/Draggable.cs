using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler{
	public Transform originalParent = null;
	public Transform placeholderParent = null;
	CanvasGroup canvasGroup;
	bool canMove = true;
	GameObject placeholder = null;
	
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
	
		placeholder = new GameObject();//create dummy placeholder object to make placing more natural
		placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();

		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;
		
		placeholder.transform.SetSiblingIndex( this.transform.GetSiblingIndex() );
	
		originalParent = transform.parent;
		placeholderParent = originalParent;
		transform.SetParent(transform.parent.parent);
		SetRaycastBlocking(false);
		EventManager.TriggerEvent("PickUpTile");
	}
	public void OnDrag(PointerEventData eventData)
	{
		this.transform.position = eventData.position;
		
		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);
		
		int newSiblingIndex = placeholderParent.childCount;
		
		for(int i=0; i < placeholderParent.childCount; i++) {
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x) {
				
				newSiblingIndex = i;
				
				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;
				
				break;
			}
		}
		
		placeholder.transform.SetSiblingIndex(newSiblingIndex);
		
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent(originalParent);
		if(canMove)
			SetRaycastBlocking(true);
		transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );//move piece to correct place in layout
		Destroy(placeholder);//destry dummy spacing object
		EventManager.TriggerEvent("DropTile");
	}
	public void SetRaycastBlocking(bool isBlocking)
	{
		canvasGroup.blocksRaycasts =isBlocking;
	}
}
