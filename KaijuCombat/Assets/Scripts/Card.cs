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

    public bool realPlayerCanViewCard = false;
    public bool zoomedIn = false;
    public bool currentlyDestroyed = false;

    private bool inPlay = false;
    private bool currentlyDragging = false;
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
        if (!inPlay && realPlayerCanViewCard && !zoomedIn && eventData.button != PointerEventData.InputButton.Right && !currentlyDestroyed) {
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
        RectTransform lane1 = GameManager.instance.lanes[0].transform as RectTransform;
        RectTransform lane2 = GameManager.instance.lanes[1].transform as RectTransform;
        RectTransform lane3 = GameManager.instance.lanes[2].transform as RectTransform;
        RectTransform lane4 = GameManager.instance.lanes[3].transform as RectTransform;
        RectTransform lane5 = GameManager.instance.lanes[4].transform as RectTransform;
        if (GameManager.instance.currentPlayersTurn == 0 && realPlayerCanViewCard && GameManager.instance.player1.currentMana >= cardCost && !inPlay) {
            if (RectTransformUtility.RectangleContainsScreenPoint(lane1, Input.mousePosition) && lane1.GetComponent<Lane>().player1Monsters.Count + 1 <= lane1.GetComponent<Lane>().MAXMONSTERS) {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane1.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(lane2, Input.mousePosition) && lane2.GetComponent<Lane>().player1Monsters.Count + 1 <= lane2.GetComponent<Lane>().MAXMONSTERS) {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane2.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(lane3, Input.mousePosition) && lane3.GetComponent<Lane>().player1Monsters.Count + 1 <= lane3.GetComponent<Lane>().MAXMONSTERS) {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane3.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(lane4, Input.mousePosition) && lane4.GetComponent<Lane>().player1Monsters.Count + 1 <= lane4.GetComponent<Lane>().MAXMONSTERS) {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane4.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(lane5, Input.mousePosition) && lane5.GetComponent<Lane>().player1Monsters.Count + 1 <= lane5.GetComponent<Lane>().MAXMONSTERS) {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane5.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else {
                transform.position = originalPosition;
            }
        }
        else {
            transform.position = originalPosition;
        }
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
