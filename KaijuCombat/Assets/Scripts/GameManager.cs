using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject canvas;

    public Player player1, player2;

    public GameObject endTurnButton;

    public GameObject player1HealthText;
    public GameObject player1ManaText;
    public GameObject player1DeckImage;
    public GameObject player2HealthText;
    public GameObject player2ManaText;
    public GameObject player2DeckImage;
    public GameObject endGameText;
    public GameObject endGamePanel;

    public GameObject player1HurtGraphic;
    public GameObject player2HurtGraphic;


    public List<Lane> lanes;

    public int turnNumber = 1;

    public int currentPlayersTurn = 0; //0 = Player 1 , 1 = Player 2

    public Sprite cardBack;

    public bool endGame = false;

    public bool attackPhaseInProgress = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        string deck = PlayerPrefs.GetString("deckSelected");

        if (deck != null)
        {
            player1.deck.Clear();

            string filePath = Application.persistentDataPath.ToString() + "/" + deck;

            StreamReader reader = new StreamReader(filePath);

            while (!reader.EndOfStream)
            {
                string card = reader.ReadLine();

                string[] guids = AssetDatabase.FindAssets(card);

                if (guids[0] != null)
                {
                    player1.deck.Add(AssetDatabase.LoadAssetAtPath<MonsterCard>(AssetDatabase.GUIDToAssetPath(guids[0])));
                }
            }
        }

        for (int i = 0; i < 5; ++i)
        {
            currentPlayersTurn = 0;
            player1.DrawCard();
            currentPlayersTurn = 1;
            player2.DrawCard();
        }
        currentPlayersTurn = 0;
        player1.StartTurn();
        DrawCardsOnScreen();
    }

    private void Update()
    {
        /*if (currentPlayersTurn == 0)
            endTurnButton.SetActive(true);
        else
            endTurnButton.SetActive(false);*/

        if (player1 == null)
        {
            if (!endGame)
            {
                endGameText.transform.SetParent(null);
                endGameText.transform.SetParent(canvas.transform);
            }
            endGamePanel.SetActive(true);
            endGamePanel.transform.SetSiblingIndex(canvas.transform.childCount - 1);
            endGameText.GetComponent<Text>().text = "Defeat!";
            endTurnButton.GetComponent<Button>().interactable = false;
            StopAllCoroutines();
            resetPlayerInstatiatedCards();
            endGame = true;
        }

        if (player2 == null)
        {
            if (!endGame)
            {
                endGameText.transform.SetParent(null);
                endGameText.transform.SetParent(canvas.transform);
            }
            endGamePanel.SetActive(true);
            endGamePanel.transform.SetSiblingIndex(canvas.transform.childCount - 1);
            endGameText.GetComponent<Text>().text = "Victory!";
            endTurnButton.GetComponent<Button>().interactable = false;
            StopAllCoroutines();
            resetPlayerInstatiatedCards();
            endGame = true;
        }

        if (player1)
        {
            if (player1 && player1HealthText.GetComponent<Text>().text != "Health: " + player1.GetComponent<Player>().currentHealth)
                player1HealthText.GetComponent<Text>().text = "Health: " + player1.GetComponent<Player>().currentHealth;

            if (player1ManaText.GetComponent<Text>().text != "Mana: " + player1.GetComponent<Player>().currentMana)
                player1ManaText.GetComponent<Text>().text = "Mana: " + player1.GetComponent<Player>().currentMana;

            if (player1.deck.Count <= 0)
                player1DeckImage.SetActive(false);
            else
                player1DeckImage.SetActive(true);
        }
        else
        {
            player1HealthText.GetComponent<Text>().text = "Health: 0";
        }

        if (player2)
        {
            if (player2HealthText.GetComponent<Text>().text != "Health: " + player2.GetComponent<Player>().currentHealth)
                player2HealthText.GetComponent<Text>().text = "Health: " + player2.GetComponent<Player>().currentHealth;

            if (player2ManaText.GetComponent<Text>().text != "Mana: " + player2.GetComponent<Player>().currentMana)
                player2ManaText.GetComponent<Text>().text = "Mana: " + player2.GetComponent<Player>().currentMana;

            if (player2.deck.Count <= 0)
                player2DeckImage.SetActive(false);
            else
                player2DeckImage.SetActive(true);
        }
        else
        {
            player2HealthText.GetComponent<Text>().text = "Health: 0";
        }

    }

    public void NextPlayersTurn()
    {
        resetPlayerInstatiatedCards();

        if (currentPlayersTurn == 0)
        {
            currentPlayersTurn = 1;
            player2.StartTurn();
            DrawCardsOnScreen();
        }
        else
        {
            currentPlayersTurn = 0;
            StartCoroutine(this.AttackPhase());
        }
    }

    public IEnumerator AllLanesAttack()
    {
        for (int i = 0; i < lanes.Count; ++i)
        {
            Debug.Log("Lane #" + i.ToString() + " attack");
            yield return StartCoroutine(lanes[i].LaneAttackPhase());
        }
        DrawCardsOnScreen();
    }

    public IEnumerator AttackPhase()
    {
        this.endTurnButton.SetActive(false);
        this.attackPhaseInProgress = true;
        Debug.Log("Before All Lanes Attack");
        yield return StartCoroutine(AllLanesAttack());
        Debug.Log("After All Lanes Attack");
        this.attackPhaseInProgress = false;
        turnNumber += 1;
        player1.StartTurn();
        StartCoroutine(playDrawCardAnimations(player1));
        DrawCardsOnScreen();
        this.endTurnButton.SetActive(true);
    }

    public void DrawCardsOnScreen()
    {
        //Player 1's hand
        for (int i = 0; i < player1.instantiatedHandCards.Count; i++)
        {
            player1.instantiatedHandCards[i].GetComponent<Card>().realPlayerCanViewCard = true;

            if (!player1.instantiatedHandCards[i].GetComponent<Card>().inAnimation)
            {
                player1.instantiatedHandCards[i].GetComponent<RectTransform>().position = new Vector2(100 * i + 70, 100);
                if (player1.instantiatedHandCards[i].GetComponent<Card>().zoomedIn)
                    player1.instantiatedHandCards[i].GetComponent<Card>().ToggleZoom();
            }
        }

        //Player 1's graveyard
        for (int i = 0; i < player1.instantiatedGraveyardCards.Count; i++)
        {
            player1.instantiatedGraveyardCards[i].GetComponent<RectTransform>().position = new Vector2(Screen.width - 169, 285);
            player1.instantiatedGraveyardCards[i].GetComponent<Card>().realPlayerCanViewCard = true;
            player1.instantiatedGraveyardCards[i].GetComponent<Card>().currentlyDestroyed = true;
            if (player1.instantiatedGraveyardCards[i].GetComponent<Card>().zoomedIn)
                player1.instantiatedGraveyardCards[i].GetComponent<Card>().ToggleZoom();
        }

        //Player 2's hand
        for (int i = 0; i < player2.instantiatedHandCards.Count; i++)
        {
            player2.instantiatedHandCards[i].GetComponent<RectTransform>().position = new Vector2(100 * i + 70, Screen.height - 100f);
        }

        //Player 2's graveyard
        for (int i = 0; i < player2.instantiatedGraveyardCards.Count; i++)
        {
            player2.instantiatedGraveyardCards[i].GetComponent<RectTransform>().position = new Vector2(Screen.width - 169, Screen.height - 285);
            player2.instantiatedGraveyardCards[i].GetComponent<Card>().realPlayerCanViewCard = true;
            player2.instantiatedGraveyardCards[i].GetComponent<Card>().currentlyDestroyed = true;
            if (player2.instantiatedGraveyardCards[i].GetComponent<Card>().zoomedIn)
                player2.instantiatedGraveyardCards[i].GetComponent<Card>().ToggleZoom();
        }

        //Lanes
        for (int i = 0; i < lanes.Count; ++i)
        {
            for (int j = 0; j < lanes[i].player1Monsters.Count; j++)
            {
                lanes[i].player1Monsters[j].GetComponent<RectTransform>().position = new Vector2(lanes[i].transform.position.x, lanes[i].transform.position.y - 50 - 100 * j);
                if (lanes[i].player1Monsters[j].zoomedIn)
                    lanes[i].player1Monsters[j].ToggleZoom();
            }
            for (int j = 0; j < lanes[i].player2Monsters.Count; j++)
            {
                lanes[i].player2Monsters[j].GetComponent<RectTransform>().position = new Vector2(lanes[i].transform.position.x, lanes[i].transform.position.y + 50 + 100 * j);
                if (lanes[i].player2Monsters[j].zoomedIn)
                    lanes[i].player2Monsters[j].ToggleZoom();
            }
        }
    }

    public IEnumerator playDrawCardAnimations(Player player)
    {
        for (int i = 0; i < player.instantiatedHandCards.Count; i++)
        {
            if (player.instantiatedHandCards[i].GetComponent<Card>().inAnimation)
            {
                player.instantiatedHandCards[i].GetComponent<Card>().transform.position = this.player1DeckImage.transform.position;

                yield return StartCoroutine(this.moveCardAnimation(player.instantiatedHandCards[i].GetComponent<Card>(), 3));

                //player.instantiatedHandCards[i].GetComponent<Card>().inAnimation = false;
            }
        }

    }

    public IEnumerator playerHurtAnimation(int player, float time)
    {        
        float elapsedTime = 0.0f;
        GameObject hurtGraphic;

        if (player == 0)
            hurtGraphic = player1HurtGraphic;
        else
            hurtGraphic = player2HurtGraphic;

        Color originCol = hurtGraphic.GetComponent<Image>().color;
        Color newCol = new Color(1.0f, 1.0f, 1.0f);

        while (elapsedTime < (time/2))
        {
            hurtGraphic.GetComponent<Image>().color = Color.Lerp(originCol, newCol, elapsedTime / (time/2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        elapsedTime = 0.0f;
        while (elapsedTime < (time / 2))
        {
            hurtGraphic.GetComponent<Image>().color = Color.Lerp(newCol, originCol, elapsedTime / (time / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator flashCardAnimation(Card card, float time, float timeBetweenBlinks)
    {
        card.inAnimation = true;
        float elapsedTime = 0.0f;
        float blinkTime = 0.0f;

        while (elapsedTime < time)
        {
            if (blinkTime > timeBetweenBlinks)
            {
                blinkTime = 0.0f;
                card.gameObject.SetActive(!card.gameObject.activeSelf);
            }

            elapsedTime += Time.deltaTime;
            blinkTime += Time.deltaTime;
            yield return null;
        }
        card.gameObject.SetActive(true);
        card.inAnimation = false;
    }

    public IEnumerator pullBackAttackCardAnimation(Card card, Card target, float time, float pullBackDistance, int player)
    {
        Vector3 originPosition = card.transform.position;
        Quaternion originRotation = card.transform.localRotation;

        Vector3 pullBack;

        card.inAnimation = true;
        card.transform.SetAsLastSibling();
        if(player == 0)
            pullBack = card.transform.position + new Vector3(0f, -pullBackDistance, 0f);
        else
            pullBack = card.transform.position + new Vector3(0f, pullBackDistance, 0f);

        float elapsedTime = 0.0f;
        Vector3 randomRot;
            
        randomRot = originRotation.eulerAngles + new Vector3(0f, 0f, Random.Range(0f, 0.2f));

        while (elapsedTime < (time / 2.0f))
        {
            Vector3 newPos = Vector3.Lerp(card.transform.position, pullBack, elapsedTime / (time / 2.0f));
            Vector3 newRot = Vector3.Lerp(card.transform.eulerAngles, randomRot, elapsedTime / (time / 2.0f));

            card.transform.position = newPos;
            card.transform.Rotate(newRot);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0.0f;
        while (elapsedTime < time / 2.0f)
        {
            Vector3 temp = Vector3.Lerp(card.transform.position, target.transform.position, elapsedTime / (time / 2.0f));
            card.transform.position = temp;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        card.transform.position = originPosition;
        card.transform.localRotation = originRotation;
        card.inAnimation = false;
    }

    public IEnumerator shakeCardAnimation(Card card, float strength)
    {
        float shake_decay = 0.02f;
        float shake_intensity = strength;
        Vector3 originPosition = card.transform.localPosition;
        Quaternion originRotation = card.transform.localRotation;

        while (shake_intensity > 0f)
        {
            card.transform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
            card.transform.localRotation = new Quaternion(
                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
            shake_intensity -= shake_decay;
            yield return null;
        }

        card.transform.localPosition = originPosition;
        card.transform.localRotation = originRotation;
    }

    public IEnumerator spinCardAnimation(Card card, float time)
    {
        card.inAnimation = true;
        Vector3 oldRot = card.transform.eulerAngles;
        float elapsedTime = 0.0f;

        Vector3 newRot = new Vector3(0, 0, 720.0f);
        while (elapsedTime < time)
        {

            card.transform.Rotate(Vector3.Lerp(oldRot, newRot, elapsedTime/time));
            //Debug.Log(card.transform.position);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        card.transform.eulerAngles = oldRot;
        card.inAnimation = false;
    }

    private IEnumerator moveCardAnimation(Card card, float time)
    {
        float elapsedTime = 0.0f;

        Vector2 curPos = card.transform.position;
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        Vector3 curScale = card.transform.localScale;
        Vector3 finalScale = card.transform.localScale * 4;

        while (elapsedTime < time)
        {
            curPos = Vector2.Lerp(curPos, screenCenter, elapsedTime / time);
            curScale = Vector3.Lerp(curScale, finalScale, elapsedTime / time);

            card.transform.position = curPos;
            card.transform.localScale = curScale;
            //Debug.Log(card.transform.position);

            //this.player1ManaText.GetComponent<Outline>().effectColor = curColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
    public IEnumerator manaTextGlow(float glowTime)
    {
        yield return StartCoroutine(this.manaTextGlowFadeIn(glowTime));
        yield return StartCoroutine(this.manaTextGlowFadeOut(glowTime));
    }

    private IEnumerator manaTextGlowFadeIn(float glowTime)
    {
        float elapsedTime = 0.0f;

        Color curColor = this.player1ManaText.GetComponent<Outline>().effectColor;
        while (elapsedTime < glowTime)
        {
            curColor.a = Mathf.Lerp(0.0f, 1.0f, elapsedTime / glowTime);
            this.player1ManaText.GetComponent<Outline>().effectColor = curColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator manaTextGlowFadeOut(float glowTime)
    {
        float elapsedTime = 0.0f;

        Color curColor = this.player1ManaText.GetComponent<Outline>().effectColor;
        while (elapsedTime < glowTime)
        {
            curColor.a = Mathf.Lerp(1.0f, 0.0f, elapsedTime / glowTime);
            this.player1ManaText.GetComponent<Outline>().effectColor = curColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    /// <summary>
    /// Loads the Unity Scene MenuScene
    /// </summary>
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void reloadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void resetPlayerInstatiatedCards()
    {
        StopAllCoroutines();
        for (int i = 0; i < player1.instantiatedHandCards.Count; i++)
        {
            if (player1.instantiatedHandCards[i].GetComponent<Card>().inAnimation)
                player1.instantiatedHandCards[i].GetComponent<Card>().resetCard();
        }
        DrawCardsOnScreen();

    }
}
