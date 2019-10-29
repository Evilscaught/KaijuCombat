﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler {
    [SerializeField]
    private string cardName = "Card";
    [SerializeField]
    private string cardDescription = "Description";
    [SerializeField]
    private int cardCost = 0;
    [SerializeField]
    private int cardId = 0;

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

}
