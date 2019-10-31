using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {
    [SerializeField]
    private string cardName = "Card";
    [SerializeField]
    private string cardDescription = "Description";
    [SerializeField]
    private int cardCost = 0;
    [SerializeField]
    private int cardId = 0;

    private bool inPlay = false;
    public bool realPlayerOwnsCard = false;
    private Vector2 originalPosition;


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

    public void OnDrag(PointerEventData eventData) {
        if(!inPlay && realPlayerOwnsCard)
            this.transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        originalPosition = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.position = originalPosition;
    }
}
