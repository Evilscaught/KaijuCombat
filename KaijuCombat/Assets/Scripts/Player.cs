using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentHealth = 20;
    public int currentMana = 0;
    public int currentMaxMana = 0;
    public List<Card> deck;
    public List<Card> graveyard;
    public List<Card> hand;
    public List<GameObject> instantiatedHandCards;
    public List<GameObject> instantiatedGraveyardCards;
    public List<GameObject> instantiatedLaneCards;

    public void EndTurn() {
        GameManager.instance.NextPlayersTurn();
    }

    public void StartTurn() {
        if(currentMaxMana < 10)
            currentMaxMana += 1;
        currentMana = currentMaxMana;
        DrawCard();
    }

    public void DrawCard() {
        if (deck.Count > 0 && hand.Count < 7) {
            int randomIndex = Random.Range(0, deck.Count);
            Card drawedCard = deck[randomIndex];
            deck.RemoveAt(randomIndex);
            hand.Add(drawedCard);

            //Instantiate the new card to the screen and add it to our instantiated list
            GameObject createdCard = Instantiate(drawedCard, transform.position, Quaternion.identity).gameObject;
            createdCard.transform.SetParent(GameManager.instance.canvas.transform);
            createdCard.name = createdCard.name.Replace("(Clone)", "");
            instantiatedHandCards.Add(createdCard);
        }
        else if(deck.Count > 0 && hand.Count >= 7) {
            int randomIndex = Random.Range(0, deck.Count);
            Card drawedCard = deck[randomIndex];
            deck.RemoveAt(randomIndex);
            graveyard.Add(drawedCard);

            //Instantiate the new card to the screen and add it to our instantiated list
            GameObject createdCard = Instantiate(drawedCard, transform.position, Quaternion.identity).gameObject;
            createdCard.transform.SetParent(GameManager.instance.canvas.transform);
            createdCard.name = createdCard.name.Replace("(Clone)", "");
            instantiatedGraveyardCards.Add(createdCard);
        }
        else {
            ApplyDamage(1);
        }
    }

    public void ApplyDamage(int damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            PlayerDeath();
        }
    }

    public void PlayerDeath() {
        Destroy(gameObject);
    }

    public void RemoveCardFromHand(Card card) {
        int result = -1;
        for (int i = 0; i < hand.Count; ++i) {
            if(card.name == hand[i].name) {
                result = i;
                break;
            }
        }
        if (result != -1) {
            hand.RemoveAt(result);
            instantiatedHandCards.Remove(card.gameObject);
        }
    }
}
