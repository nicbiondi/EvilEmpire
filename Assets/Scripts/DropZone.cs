using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler{

	public void OnPointerEnter(PointerEventData eventdata)
	{
	}
	public void OnPointerExit(PointerEventData eventdata)
	{
	}
	public void OnDrop(PointerEventData eventdata)
	{
	 Draggable drag = eventdata.pointerDrag.GetComponent<Draggable>();
	 if(drag != null)
	 {
	 	drag.originalParent = transform;
	 }
	}
}
