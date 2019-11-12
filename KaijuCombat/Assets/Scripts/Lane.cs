using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour{
    private Player player1, player2;
    public List<Card> player1Monsters, player2Monsters;

    public int MAXMONSTERS = 3;

    private void Start() {
        player1 = GameManager.instance.player1;
        player2 = GameManager.instance.player2;
    }

}
