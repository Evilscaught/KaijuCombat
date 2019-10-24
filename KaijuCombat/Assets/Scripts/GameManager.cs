using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject canvas;

    public Player player1, player2;

    public GameObject endTurnButton;

    public int turnNumber = 1;

    public int currentPlayersTurn = 0; //0 = Player 1 , 1 = Player 2

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
        for(int i = 0; i < player1.hand.Count; i++) {
            Instantiate(player1.hand[i], new Vector3(transform.position.x + i * 2*70+100, transform.position.y + 100, transform.position.z), Quaternion.identity).gameObject.transform.SetParent(canvas.transform);
        }

        for (int i = 0; i < player2.hand.Count; i++) {
            Instantiate(player2.hand[i], new Vector3(transform.position.x + i * 2*70+100, transform.position.y + 500, transform.position.z), Quaternion.identity).gameObject.transform.SetParent(canvas.transform);
        }
    }
}
