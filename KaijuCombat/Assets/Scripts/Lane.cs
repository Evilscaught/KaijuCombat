using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour{
    private Player player1, player2;
    public List<MonsterCard> player1Monsters, player2Monsters;

    public int MAXMONSTERS = 3;

    private void Start() {
        player1 = GameManager.instance.player1;
        player2 = GameManager.instance.player2;
    }

    private void Update() {
        if (player1Monsters.Count > 0 && player1Monsters[0] == null) {
            player1Monsters.RemoveAt(0);
        }
        if (player2Monsters.Count > 0 && player2Monsters[0] == null) {
            player2Monsters.RemoveAt(0);
        }
    }

    public void AddCard(Player player, MonsterCard card) {
        if(player == player1) {
            player1Monsters.Add(card);
            player1.RemoveCardFromHand(card);
        }
        else if (player == player2) {
            player2Monsters.Add(card);
            player2.RemoveCardFromHand(card);
        }
    }

    public void AttackPhase() {
        if (player1Monsters.Count > 0) {
            if (player2Monsters.Count == 0) {
                player1Monsters[0].AttackPlayer(player2);
            }
            else {
                player1Monsters[0].ApplyDamage(player2Monsters[0].getAttack());
                player2Monsters[0].ApplyDamage(player1Monsters[0].getAttack());
                if (player1Monsters[0].getDefense() <= 0) {
                    GameManager.instance.player1.graveyard.Add(player1Monsters[0]);
                    GameManager.instance.player1.instantiatedGraveyardCards.Add(player1Monsters[0].gameObject);
                    player1Monsters.RemoveAt(0);
                }
                if (player2Monsters[0].getDefense() <= 0) {
                    GameManager.instance.player2.graveyard.Add(player2Monsters[0]);
                    GameManager.instance.player2.instantiatedGraveyardCards.Add(player2Monsters[0].gameObject);
                    player2Monsters.RemoveAt(0);
                }
            }
        }else if(player2Monsters.Count > 0) {
            player2Monsters[0].AttackPlayer(player1);
        }
    }

}
