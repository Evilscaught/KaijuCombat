/* Author(s):           Bradly Shoen (Code), Scott McKay (Documentation & Formatting)
 * Group:               The Omnipotent Creators
 * Date of Creation:    Thursday, October 24th, 2019 at 1:53 p.m. M.S.T. (Autumn Semester MMXIX)
 * Last Edit:           Thursday, November 28th, 2019
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Defines the player properties
 */

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

    /// <summary>
    /// Initiates the next turn
    /// </summary>
    public void EndTurn()
    {
        GameManager.instance.NextPlayersTurn();
    }

    /// <summary>
    /// Starts the current turn
    /// - Increases the maximum amount of mana by 1 (if less than 10)
    /// - Calls DrawCard() method
    /// </summary>
    public void StartTurn()
    {
        if(currentMaxMana < 10)
            currentMaxMana += 1;
        currentMana = currentMaxMana;
        DrawCard();
    }

    /// <summary>
    /// Draws a card from the cards available to the game and adds it to the players hand
    /// </summary>
    public void DrawCard()
    {
        if (deck.Count > 0 && hand.Count < 7)
        {
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
        else if(deck.Count > 0 && hand.Count >= 7)
        {
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
        else
        {
            ApplyDamage(1);
        }
    }

    /// <summary>
    /// Deals damage to the player by subtracting damage from their health.  Player dies if health reaches 0.
    /// </summary>
    /// <param name="damage">Damage</param>
    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    /// <summary>
    /// Remove the player object from the game if the player dies
    /// </summary>
    public void PlayerDeath()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Removes card from the players hand
    /// </summary>
    /// <param name="card">Card to be removed</param>
    public void RemoveCardFromHand(Card card)
    {
        int result = -1;
        for (int i = 0; i < hand.Count; ++i)
        {
            if(card.name == hand[i].name)
            {
                result = i;
                break;
            }
        }
        if (result != -1)
        {
            hand.RemoveAt(result);
            instantiatedHandCards.Remove(card.gameObject);
        }
    }
}
