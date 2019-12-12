/* Author(s):           Jack Cummings (Code), Scott McKay (Documentation & Formatting)
 * Group:               The Omnipotent Creators
 * Date of Creation:    Thursday, November 28th, 2019 at 7:59 p.m. M.S.T. (Autumn Semester MMXIX)
 * Last Edit:           Thursday, November 28th, 2019
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Controller for the deck builder
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class manageDeck : MonoBehaviour
{
    public TMP_Dropdown cardDDL;
    public TMP_Dropdown deckLoadDDL;
    public TMP_InputField deckName;
    public GameObject deckCollection;
    public ScrollView sv;

    private Object[] cards;

    private void Start()
    {
        cards = Resources.LoadAll("Cards", typeof(MonsterCard));

        //cardDDL.options.Clear();
        foreach (Object card in cards)
        {
            MonsterCard temp = (MonsterCard)card;
            cardDDL.options.Add(new TMP_Dropdown.OptionData(temp.name));
        }
    }

    public void loadDeck()
    {
        string filePath = Application.persistentDataPath.ToString() + "/" + deckLoadDDL.options[deckLoadDDL.value].text;

        StreamReader reader = new StreamReader(filePath);

        while(!reader.EndOfStream)
        {
            string tempCardName = reader.ReadLine();


            foreach (Object card in cards)
            {
                if (card.name.Equals(tempCardName))
                {
                    MonsterCard temp = (MonsterCard)card;
                    this.addCardToDeckCollection(temp);
                    break;
                }
            }
        }
    }

    public void loadDeckDDL()
    {
        deckLoadDDL.options.Clear();

        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath.ToString());
        FileInfo[] info = dir.GetFiles("*.sav");
        foreach (FileInfo f in info)
        {
            //Debug.Log(f.Name);
            deckLoadDDL.options.Add(new TMP_Dropdown.OptionData(f.Name));
        }
    }

    public void saveDeck()
    {
        Debug.Log(Application.persistentDataPath);
        string saveFilePath = Application.persistentDataPath.ToString() + "/" + deckName.text + ".sav";
        using (TextWriter writer = new StreamWriter(saveFilePath, false))
        {
            foreach (Transform ob in deckCollection.transform)
            {
                //Debug.Log(ob.name);
                writer.WriteLine(ob.name);
            }
            writer.Close();
        }
    }

    public void addButtonClick()
    {
        //cardDDL.options.Clear();
        foreach (Object card in cards)
        {
            MonsterCard temp = (MonsterCard)card;

            if (temp != null && temp.name.Equals(cardDDL.options[cardDDL.value].text))
            {
                addCardToDeckCollection(temp);
                break;
            }
        }
    }

    private void addCardToDeckCollection(MonsterCard card)
    {
        GameObject newCard = new GameObject(card.name);

        UnityEngine.UI.Image newCardImage = newCard.AddComponent<UnityEngine.UI.Image>();
        newCardImage.sprite = card.GetComponent<UnityEngine.UI.Image>().sprite;
        newCardImage.preserveAspect = true;

        //ScrollViewGameObject container object
        if (deckCollection != null)
        {
            newCard.transform.localScale *= 4.0f;
            newCard.transform.position += new Vector3(newCard.transform.localScale.x / 2, newCard.transform.localScale.y / 2, 0f);
            newCard.transform.SetParent(deckCollection.transform, false);

        }
    }

    public void clearDeckCollection()
    {
        foreach (Transform child in deckCollection.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Loads the Unity Scene MenuScene
    /// </summary>
    public void returnToMainMenu() {
        SceneManager.LoadScene("MenuScene");
    }
}
