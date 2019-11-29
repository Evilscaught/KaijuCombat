/* Author(s):           Bradly Shoen (Code), Scott McKay (Documentation & Formatting)
 * Group:               The Omnipotent Creators
 * Date of Creation:    Thursday, October 24th, 2019 at 1:53 p.m. M.S.T. (Autumn Semester MMXIX)
 * Last Edit:           Thursday, November 28th, 2019
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Defines the card properties
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The MonsterCard is a card object with the following properties: card attack and card defense
/// </summary>
public class MonsterCard : Card, IPointerClickHandler
{
    [SerializeField]
    private int cardAttack = 0;
    [SerializeField]
    private int cardDefense = 0;

    /// <summary>
    /// This method will take the damage the card has recieved and reduce the cards defense
    /// </summary>
    /// <param name="damage">Amount of damage to inflict on the card</param>
    public void ApplyDamage(int damage)
    {
        cardDefense -= damage;
    }

    /// <summary>
    /// This method will allow the card to take and deal damage
    /// </summary>
    /// <param name="target">Target Card</param>
    public void AttackCard(MonsterCard target)
    {
        ApplyDamage(target.cardAttack);
        target.ApplyDamage(cardAttack);
        if (cardDefense <= 0)
        {
            DestroyCard();
        }
        if(target.cardDefense <= 0)
        {
            target.DestroyCard();
        }
    }

    /// <summary>
    /// This method will deal damage to the player, the damage dealt will be based on the cardAttack
    /// </summary>
    /// <param name="target"></param>
    public void AttackPlayer(Player target)
    {
        target.ApplyDamage(cardAttack);
    }

    public int getAttack() { return cardAttack; }
    public int getDefense() { return cardDefense; }

}
