using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler{
	private bool validDropArea = true;
	public string answer = "e";
	Game game;
	
	void Start()
	{
		game = Game.Instance;
		//drop zone registers itself with Game
		game.AddSlot(this);
	}

	public bool ValidDropArea {
		get {
			return validDropArea;
		}
		set {
			validDropArea = value;
		}
	}

	public void OnPointerEnter(PointerEventData eventdata)
	{
	}
	public void OnPointerExit(PointerEventData eventdata)
	{
	}
	public void OnDrop(PointerEventData eventdata)
	{
		Draggable draggable = eventdata.pointerDrag.GetComponent<Draggable>();
		if(draggable != null && validDropArea)
		{
			string tileText = eventdata.pointerDrag.GetComponentInChildren<Text>().text;
			if(tileText.ToLower() == answer)//if correct letter was placed
			{
				validDropArea=false;//only allow one tile to be dropped
	 			draggable.originalParent = transform;//set tile onto this dropzone
				draggable.SetRaycastBlocking(false);
				EventManager.TriggerEvent("CheckGameState");
	 		}
	 	}
	}
}
