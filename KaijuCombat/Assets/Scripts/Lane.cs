/* Author(s):           Bradly Shoen (Code), Scott McKay (Documentation & Formatting)
 * Group:               The Omnipotent Creators
 * Date of Creation:    Tuesday, November 12th, 2019 at 12:28 p.m. M.S.T. (Autumn Semester MMXIX)
 * Last Edit:           Thursday, November 28th, 2019
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Defines the properties of the lanes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the properties of the lanes
/// </summary>
public class Lane : MonoBehaviour
{
    private Player player1, player2;
    public List<MonsterCard> player1Monsters, player2Monsters;

    public int MAXMONSTERS = 3;

    private void Start()
    {
        player1 = GameManager.instance.player1;
        player2 = GameManager.instance.player2;
    }

    /// <summary>
    /// If a monster card has been destroyed, remove it from the playerMonsters list 
    /// </summary>
    private void Update()
    {
        if (player1Monsters.Count > 0 && player1Monsters[0] == null)
        {
            player1Monsters.RemoveAt(0);
        }
        if (player2Monsters.Count > 0 && player2Monsters[0] == null)
        {
            player2Monsters.RemoveAt(0);
        }
    }

    /// <summary>
    /// This method will add a card to the player:
    /// - Removes the card from the players hand
    /// - Adds the card to the players card collection
    /// </summary>
    /// <param name="player">The current player object</param>
    /// <param name="card">The current card object</param>
    public void AddCard(Player player, MonsterCard card)
    {
        if(player == player1)
        {
            player1Monsters.Add(card);
            player1.RemoveCardFromHand(card);
        }
        else if (player == player2)
        {
            player2Monsters.Add(card);
            player2.RemoveCardFromHand(card);
        }
    }

    /// <summary>
    /// This method specifies the rules of the attack during the attack phase
    /// </summary>
    public IEnumerator LaneAttackPhase()
    {
        //Debug.Log("Lane attack phase");
        if (player1Monsters.Count > 0 )
        {
            // If player 2 doesn't have any cards deployed, deal damage to player 2
            if (player2Monsters.Count == 0)
            {
                if (player1Monsters[0].getAttack() > 0)
                {
                    Debug.Log("Inside player #2 attack phase");
                    yield return StartCoroutine(GameManager.instance.spinCardAnimation(player1Monsters[0], 0.8f));
                    player1Monsters[0].AttackPlayer(player2);
                    yield return StartCoroutine(GameManager.instance.playerHurtAnimation(1, 0.7f));
                    Debug.Log("Done player #2 attack phase");
                }
            }
            else
            {
                if (player2Monsters[0].getAttack() > 0)
                {
                    yield return StartCoroutine(GameManager.instance.pullBackAttackCardAnimation(player2Monsters[0], player1Monsters[0], 1.3f, 20f, 1));
                    player1Monsters[0].ApplyDamage(player2Monsters[0].getAttack());
                    yield return StartCoroutine(GameManager.instance.shakeCardAnimation(player1Monsters[0], 0.7f));
                    //yield return StartCoroutine(GameManager.instance.flashCardAnimation(player1Monsters[0], 1, 0.1f));
                }
                if (player1Monsters[0].getAttack() > 0)
                {
                    yield return StartCoroutine(GameManager.instance.pullBackAttackCardAnimation(player1Monsters[0], player2Monsters[0], 1.3f, 20f, 0));
                    player2Monsters[0].ApplyDamage(player1Monsters[0].getAttack());
                    yield return StartCoroutine(GameManager.instance.shakeCardAnimation(player2Monsters[0], 0.7f));
                    //yield return StartCoroutine(GameManager.instance.flashCardAnimation(player2Monsters[0], 1, 0.1f));
                }

                if (player1Monsters[0].getDefense() <= 0)
                {
                    GameManager.instance.player1.graveyard.Add(player1Monsters[0]);
                    GameManager.instance.player1.instantiatedGraveyardCards.Add(player1Monsters[0].gameObject);
                    player1Monsters.RemoveAt(0);
                }
                if (player2Monsters[0].getDefense() <= 0)
                {
                    GameManager.instance.player2.graveyard.Add(player2Monsters[0]);
                    GameManager.instance.player2.instantiatedGraveyardCards.Add(player2Monsters[0].gameObject);
                    player2Monsters.RemoveAt(0);
                }

            }
        }
        // Deal damage to player 1 if player 2 has monster cards
        else if (player2Monsters.Count > 0 && player2Monsters[0].getAttack() > 0)
        {
            yield return StartCoroutine(GameManager.instance.spinCardAnimation(player2Monsters[0], 0.8f));
            player2Monsters[0].AttackPlayer(player1);
            yield return StartCoroutine(GameManager.instance.playerHurtAnimation(0, 0.7f));
        }

        Debug.Log("Bottom of lane attack phase");
        yield return null;
    }
/*
    public IEnumerator CardAttacks()
    {
        yield return StartCoroutine(this.manaTextGlowFadeIn(glowTime));
        yield return StartCoroutine(this.manaTextGlowFadeOut(glowTime));
    }

    private IEnumerator manaTextGlowFadeIn(float glowTime)
    {

    }
    */


}
