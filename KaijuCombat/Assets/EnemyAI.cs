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
            //Confirmed it is the enemy's turn
            for (int i = 0; i < playerScript.hand.Count; ++i) {
                //For every card in the enemy's hand
                if (playerScript.currentMana- playerScript.hand[i].getCost() >= 0) {
                    //Check if the enemy can play this card

                    //Reset openLanes each playable card
                    List<int> openLanes = new List<int>();

                    for (int j = 0; j < gm.lanes.Count; ++j) {
                        //For each lane
                        if(gm.lanes[j].player2Monsters.Count < gm.lanes[j].MAXMONSTERS) {
                            //Check if the current lane is open for monsters to be played
                            openLanes.Add(j);
                        }
                    }

                    //Play card to a random open lane
                    playerScript.instantiatedHandCards[i].GetComponent<Card>().realPlayerCanViewCard = true;
                    playerScript.instantiatedHandCards[i].GetComponent<Card>().inPlay = true;
                    playerScript.currentMana -= playerScript.instantiatedHandCards[i].GetComponent<Card>().getCost();
                    gm.lanes[openLanes[Random.Range(0,openLanes.Count)]].AddCard(gm.player2, playerScript.instantiatedHandCards[i].GetComponent<Card>() as MonsterCard);
                    gm.DrawCardsOnScreen();
                }
            }
            playerScript.EndTurn();
        }
    }
}
