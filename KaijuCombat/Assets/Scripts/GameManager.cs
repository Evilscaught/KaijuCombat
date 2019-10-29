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

    public int turnNumber = 1;

    public int currentPlayersTurn = 0; //0 = Player 1 , 1 = Player 2

    public Sprite cardBack;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start() {
        player1.StartTurn();
        DrawCardsOnScreen();
    }

    private void Update() {
        if (currentPlayersTurn == 0)
            endTurnButton.SetActive(true);
        else
            endTurnButton.SetActive(false);
    }

    public void NextPlayersTurn() {
        if (currentPlayersTurn == 0) {
            currentPlayersTurn = 1;
            player2.StartTurn();
            DrawCardsOnScreen();
        }
        else {
            currentPlayersTurn = 0;
            turnNumber += 1;
            player1.StartTurn();
            DrawCardsOnScreen();
        }
    }

    public void DrawCardsOnScreen() {
        for(int i = 0; i < player1.instantiatedCards.Count; i++) {
            player1.instantiatedCards[i].transform.position = new Vector2(100*i+70, 100);
        }
        for (int i = 0; i < player2.instantiatedCards.Count; i++) {
            player2.instantiatedCards[i].GetComponent<Image>().sprite = cardBack;
            player2.instantiatedCards[i].transform.position = new Vector2(100 * i + 70, 300);
            print(player2.instantiatedCards[i].transform.position);
        }
    }
}
