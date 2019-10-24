using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCard : Card
{
    [SerializeField]
    private int cardAttack = 0;
    [SerializeField]
    private int cardDefense = 0;

    public void ApplyDamage(int damage) {
        cardDefense -= damage;
        if(cardDefense <= 0) {
            DestroyCard();
        }
    }

    public void AttackCard(MonsterCard target) {
        target.ApplyDamage(cardAttack);
    }

    public void AttackPlayer(Player target) {
        target.ApplyDamage(cardAttack);
    }

    public int getAttack() { return cardAttack; }
    public int getDefense() { return cardDefense; }

}
