using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterCard : Card, IPointerClickHandler {
    [SerializeField]
    private int cardAttack = 0;
    [SerializeField]
    private int cardDefense = 0;

    public void ApplyDamage(int damage) {
        cardDefense -= damage;
    }

    public void AttackCard(MonsterCard target) {
        ApplyDamage(target.cardAttack);
        target.ApplyDamage(cardAttack);
        if (cardDefense <= 0) {
            DestroyCard();
        }
        if(target.cardDefense <= 0) {
            target.DestroyCard();
        }
    }

    public void AttackPlayer(Player target) {
        target.ApplyDamage(cardAttack);
    }

    public int getAttack() { return cardAttack; }
    public int getDefense() { return cardDefense; }

}
