using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler {
    [SerializeField]
    private string cardName = "Card";
    [SerializeField]
    private string cardDescription = "Description";
    [SerializeField]
    private int cardCost = 0;
    [SerializeField]
    private int cardId = 0;

    private bool inPlay = false;
    public bool zoomedIn = false;
    private bool currentlyDragging = false;
    public bool realPlayerCanViewCard = false;
    private Vector2 originalPosition; //This is for dragging the card
    Vector2 origScale;
    Vector2 origPosition; //This is for returning the card after zooming


    public string getName() { return cardName; }
    public string getDescription() { return cardDescription; }
    public int getCost() { return cardCost; }
    public int getId() { return cardId; }
    public void DestroyCard() {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.SetAsLastSibling();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (realPlayerCanViewCard && eventData.button == PointerEventData.InputButton.Right) {
            ToggleZoom();
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (!inPlay && realPlayerCanViewCard && !zoomedIn && eventData.button != PointerEventData.InputButton.Right) {
            this.transform.position = eventData.position;
            currentlyDragging = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if(currentlyDragging == false)
            originalPosition = transform.position;
        currentlyDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.position = originalPosition;
        currentlyDragging = false;
    }

    public void ToggleZoom() {
        if (!currentlyDragging) {
            if (!zoomedIn) {
                origScale = transform.localScale;
                origPosition = transform.position;
                transform.localScale = new Vector3(1f, 1f, 1f);
                transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
                zoomedIn = true;
            }
            else {
                transform.localScale = origScale;
                transform.position = origPosition;
                zoomedIn = false;
            }
        }
    }
}
