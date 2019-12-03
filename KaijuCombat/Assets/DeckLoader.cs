using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    Player playerScript;
    List<Card> loadedDeck;


    private void Start() {
        playerScript = GetComponent<Player>();
    }
}
