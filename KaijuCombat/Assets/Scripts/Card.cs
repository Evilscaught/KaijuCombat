/* Author(s):           Bradly Shoen (Code), Scott McKay (Documentation & Formatting)
 * Group:               The Omnipotent Creators
 * Date of Creation:    Thursday, October 24th, 2019 at 1:53 p.m. M.S.T. (Autumn Semester MMXIX)
 * Last Edit:           Thursday, November 28th, 2019
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Defines the card properties and the interaction the card has with the game
 */

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

    public bool inPlay = false;
    private bool currentlyDragging = false;
    private Vector2 originalPosition; //This is for dragging the card
    Vector2 origScale;
    Vector2 origPosition; //This is for returning the card after zooming
    private Sprite originalSprite;


    public string getName() { return cardName; }
    public string getDescription() { return cardDescription; }
    public int getCost() { return cardCost; }
    public int getId() { return cardId; }

    /// <summary>
    /// Destroy the card
    /// </summary>
    public void DestroyCard()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// This method will call the ToggleZoom() method (which expands the card) provided that the following conditions have been met:
    /// - The player is permitted to interact with the card
    /// - The player has right mouse clicked.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (realPlayerCanViewCard && eventData.button == PointerEventData.InputButton.Right)
        {
            ToggleZoom();
        }
    }

    /// <summary>
    /// This method initiates card dragging provided that the following conditions have been met:
    /// - The card hasn't been already deployed to the game board
    /// - The player is permitted to interact with the card
    /// - The card is not enlarged
    /// - The right mouse button is not being clicked on
    /// - The card hasn't been destroyed
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (!inPlay && realPlayerCanViewCard && !zoomedIn && eventData.button != PointerEventData.InputButton.Right && !currentlyDestroyed)
        {
            this.transform.position = eventData.position;
            currentlyDragging = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentlyDragging == false)
        {
            originalPosition = transform.position;
        }
        currentlyDragging = true;
    }

    /// <summary>
    /// This method allows the player to drag cards into the lanes provided that the following conditions have been met:
    /// - It it currently the players turn
    /// - The player is permitted to interact with the card
    /// - The player has enough mana to deploy the card
    /// - The amount of cards in the lane hasn't exceeded the maximum amount of cards the lane can have
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform lane1 = GameManager.instance.lanes[0].transform as RectTransform;
        RectTransform lane2 = GameManager.instance.lanes[1].transform as RectTransform;
        RectTransform lane3 = GameManager.instance.lanes[2].transform as RectTransform;
        RectTransform lane4 = GameManager.instance.lanes[3].transform as RectTransform;
        RectTransform lane5 = GameManager.instance.lanes[4].transform as RectTransform;
        if (GameManager.instance.currentPlayersTurn == 0 && realPlayerCanViewCard && GameManager.instance.player1.currentMana >= cardCost && !inPlay)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(lane1, Input.mousePosition) && lane1.GetComponent<Lane>().player1Monsters.Count + 1 <= lane1.GetComponent<Lane>().MAXMONSTERS)
            {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane1.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(lane2, Input.mousePosition) && lane2.GetComponent<Lane>().player1Monsters.Count + 1 <= lane2.GetComponent<Lane>().MAXMONSTERS)
            {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane2.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(lane3, Input.mousePosition) && lane3.GetComponent<Lane>().player1Monsters.Count + 1 <= lane3.GetComponent<Lane>().MAXMONSTERS)
            {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane3.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(lane4, Input.mousePosition) && lane4.GetComponent<Lane>().player1Monsters.Count + 1 <= lane4.GetComponent<Lane>().MAXMONSTERS)
            {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane4.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(lane5, Input.mousePosition) && lane5.GetComponent<Lane>().player1Monsters.Count + 1 <= lane5.GetComponent<Lane>().MAXMONSTERS)
            {
                GameManager.instance.player1.currentMana -= cardCost;
                inPlay = true;
                lane5.GetComponent<Lane>().AddCard(GameManager.instance.player1, this as MonsterCard);
                GameManager.instance.DrawCardsOnScreen();
            }
            else
            {
                transform.position = originalPosition;
            }
        }
        else
        {
            transform.position = originalPosition;
        }
        currentlyDragging = false;
    }

    /// <summary>
    /// This method will enlarge the card to the center of the board.
    /// If the card has already been enlarged it will return to the hand. 
    /// </summary>
    public void ToggleZoom()
    {
        if (!currentlyDragging)
        {
            if (!zoomedIn)
            {
                origScale = transform.localScale;
                origPosition = transform.position;
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
                zoomedIn = true;
            }
            else
            {
                transform.localScale = origScale;
                transform.position = origPosition;
                zoomedIn = false;
            }
        }
    }

    /// <summary>
    /// Renders the card sprites onto the card objects in the game
    /// </summary>
    void Awake()
    {
        originalSprite = GetComponent<Image>().sprite;
    }

    /// <summary>
    /// The update method will check if the player is allowed to see the contents of a card
    /// State 1, yes: the sprite will update to display the contents of the card
    /// State 2, no:  the sprite will update to display the back of the card
    /// </summary>
    void Update()
    {
        if (realPlayerCanViewCard == false)
        {
            GetComponent<Image>().sprite = GameManager.instance.cardBack;
        }
        else
        {
            GetComponent<Image>().sprite = originalSprite;
        }
    }
}
