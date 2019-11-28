using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Player playerScript;
    GameManager gm;

    private void Start() {
        playerScript = GetComponent<Player>();
        gm = GameManager.instance;
    }

    private void Update() {
        if(gm.currentPlayersTurn == 1) {
            for(int i = 0; i < playerScript.hand.Count; ++i) {
                if(playerScript.currentMana- playerScript.hand[i].getCost() >= 0) {
                    for(int j = 0; j < gm.lanes.Count; ++j) {
                        if(gm.lanes[j].player2Monsters.Count < gm.lanes[j].MAXMONSTERS) {
                            playerScript.instantiatedHandCards[i].GetComponent<Card>().realPlayerCanViewCard = true;
                            playerScript.instantiatedHandCards[i].GetComponent<Card>().inPlay = true;
                            playerScript.currentMana -= playerScript.instantiatedHandCards[i].GetComponent<Card>().getCost();
                            gm.lanes[j].AddCard(gm.player2, playerScript.instantiatedHandCards[i].GetComponent<Card>() as MonsterCard);
                            gm.DrawCardsOnScreen();
                            break;
                        }
                    }
                }
            }
            playerScript.EndTurn();
        }
    }
}
