using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject canvas;

    public Player player1, player2;

    public GameObject endTurnButton;

    public GameObject player1HealthText;
    public GameObject player1ManaText;
    public GameObject player1DeckImage;
    public GameObject player2HealthText;
    public GameObject player2ManaText;
    public GameObject player2DeckImage;
    public GameObject endGameText;

    public List<Lane> lanes;

    public int turnNumber = 1;

    public int currentPlayersTurn = 0; //0 = Player 1 , 1 = Player 2

    public Sprite cardBack;

    public bool endGame = false;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start() {
        for(int i = 0; i < 5; ++i) {
            player1.DrawCard();
            player2.DrawCard();
        }
        player1.StartTurn();
        DrawCardsOnScreen();
    }

    private void Update() {
        if (currentPlayersTurn == 0)
            endTurnButton.SetActive(true);
        else
            endTurnButton.SetActive(false);

        if (player1 == null) {
            if (!endGame) {
                endGameText.transform.SetParent(null);
                endGameText.transform.SetParent(canvas.transform);
            }
            endGameText.SetActive(true);
            endGameText.GetComponent<Text>().text = "Defeat!";
            endTurnButton.GetComponent<Button>().interactable = false;
            endGame = true;
        }

        if (player2 == null) {
            if (!endGame) {
                endGameText.transform.SetParent(null);
                endGameText.transform.SetParent(canvas.transform);
            }
            endGameText.SetActive(true);
            endGameText.GetComponent<Text>().text = "Victory!";
            endTurnButton.GetComponent<Button>().interactable = false;
            endGame = true;
        }

        if (player1) {
            if (player1 && player1HealthText.GetComponent<Text>().text != "Health: " + player1.GetComponent<Player>().currentHealth)
                player1HealthText.GetComponent<Text>().text = "Health: " + player1.GetComponent<Player>().currentHealth;

            if (player1ManaText.GetComponent<Text>().text != "Mana: " + player1.GetComponent<Player>().currentMana)
                player1ManaText.GetComponent<Text>().text = "Mana: " + player1.GetComponent<Player>().currentMana;

            if (player1.deck.Count <= 0)
                player1DeckImage.SetActive(false);
            else
                player1DeckImage.SetActive(true);
        }
        else {
            player1HealthText.GetComponent<Text>().text = "Health: 0";
        }

        if (player2) {
            if (player2HealthText.GetComponent<Text>().text != "Health: " + player2.GetComponent<Player>().currentHealth)
                player2HealthText.GetComponent<Text>().text = "Health: " + player2.GetComponent<Player>().currentHealth;

            if (player2ManaText.GetComponent<Text>().text != "Mana: " + player2.GetComponent<Player>().currentMana)
                player2ManaText.GetComponent<Text>().text = "Mana: " + player2.GetComponent<Player>().currentMana;

            if (player2.deck.Count <= 0)
                player2DeckImage.SetActive(false);
            else
                player2DeckImage.SetActive(true);
        }
        else {
            player2HealthText.GetComponent<Text>().text = "Health: 0";
        }

    }

    public void NextPlayersTurn() {
        if (currentPlayersTurn == 0) {
            currentPlayersTurn = 1;
            player2.StartTurn();
            DrawCardsOnScreen();
        }
        else {
            AllLanesAttack();
            currentPlayersTurn = 0;
            turnNumber += 1;
            player1.StartTurn();
            DrawCardsOnScreen();
        }
    }

    public void AllLanesAttack() {
        for(int i = 0; i < lanes.Count; ++i) {
            lanes[i].AttackPhase();
        }
        DrawCardsOnScreen();
    }

    public void DrawCardsOnScreen() {
        //Player 1's hand
        for(int i = 0; i < player1.instantiatedHandCards.Count; i++) {
            player1.instantiatedHandCards[i].GetComponent<RectTransform>().position = new Vector2(100*i+70, 100);
            player1.instantiatedHandCards[i].GetComponent<Card>().realPlayerCanViewCard = true;
            if(player1.instantiatedHandCards[i].GetComponent<Card>().zoomedIn)
                player1.instantiatedHandCards[i].GetComponent<Card>().ToggleZoom();
        }

        //Player 1's graveyard
        for (int i = 0; i < player1.instantiatedGraveyardCards.Count; i++) {
            player1.instantiatedGraveyardCards[i].GetComponent<RectTransform>().position = new Vector2(Screen.width-169, 285);
            player1.instantiatedGraveyardCards[i].GetComponent<Card>().realPlayerCanViewCard = true;
            player1.instantiatedGraveyardCards[i].GetComponent<Card>().currentlyDestroyed = true;
            if (player1.instantiatedGraveyardCards[i].GetComponent<Card>().zoomedIn)
                player1.instantiatedGraveyardCards[i].GetComponent<Card>().ToggleZoom();
        }

        //Player 2's hand
        for (int i = 0; i < player2.instantiatedHandCards.Count; i++) {
            player2.instantiatedHandCards[i].GetComponent<Image>().sprite = cardBack;
            player2.instantiatedHandCards[i].GetComponent<RectTransform>().position = new Vector2(100 * i + 70, Screen.height-100f);
        }

        //Player 2's graveyard
        for (int i = 0; i < player2.instantiatedGraveyardCards.Count; i++) {
            player2.instantiatedGraveyardCards[i].GetComponent<RectTransform>().position = new Vector2(Screen.width - 169, Screen.height-285);
            player2.instantiatedGraveyardCards[i].GetComponent<Card>().realPlayerCanViewCard = true;
            player2.instantiatedGraveyardCards[i].GetComponent<Card>().currentlyDestroyed = true;
            if (player2.instantiatedGraveyardCards[i].GetComponent<Card>().zoomedIn)
                player2.instantiatedGraveyardCards[i].GetComponent<Card>().ToggleZoom();
        }

        //Player 1's lane cards
        for (int i = 0; i < player1.instantiatedLaneCards.Count; i++) {
            player1.instantiatedLaneCards[i].GetComponent<RectTransform>().position = new Vector2(100 * i + 70, 285);
        }

        //Lanes
        for (int i = 0; i < lanes.Count; ++i) {
            for (int j = 0; j < lanes[i].player1Monsters.Count; j++) {
                lanes[i].player1Monsters[j].GetComponent<RectTransform>().position = new Vector2(lanes[i].transform.position.x, lanes[i].transform.position.y - 50 - 100 * j);
            }
        }
    }
}
