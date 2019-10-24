using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 20;
    public int mana = 0;
    public List<Card> deck;
    public List<Card> graveyard;
    public List<Card> hand;

    public void EndTurn() {
        GameManager.instance.NextPlayersTurn();
    }

    public void StartTurn() {
        mana += 1;
        DrawCard();
    }

    public void DrawCard() {
        if (deck.Count > 0) {
            int randomIndex = Random.Range(0, deck.Count);
            Card drawedCard = deck[randomIndex];
            deck.RemoveAt(randomIndex);
            hand.Add(drawedCard);
        }
        else {
            health -= 1;
        }
    }

    public void ApplyDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            PlayerDeath();
        }
    }

    public void PlayerDeath() {
        Destroy(gameObject);
    }
}
