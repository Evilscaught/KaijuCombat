using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.IO;

public class ManageDeck : MonoBehaviour
{
    public TMP_Dropdown cardDDL;
    public TMP_Dropdown deckLoadDDL;
    public TMP_InputField deckName;
    public GameObject deckCollection;
    public ScrollView sv;

    private List<string> guidCardList;

    private void Start()
    {
        guidCardList = new List<string>();

        string[] guids1 = AssetDatabase.FindAssets("Monster");

        //cardDDL.options.Clear();
        foreach (string guid in guids1)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            if (!assetPath.Contains("Old"))
            {
                GameObject tempAsset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (tempAsset != null)
                {
                    string card = tempAsset.name;
                    cardDDL.options.Add(new TMP_Dropdown.OptionData(card));
                }
            }
        }
    }

    public void loadDeck()
    {
        string filePath = Application.persistentDataPath.ToString() + "/" + deckLoadDDL.options[deckLoadDDL.value].text;

        StreamReader reader = new StreamReader(filePath);

        while(!reader.EndOfStream)
        {
            string card = reader.ReadLine();

            string[] guids = AssetDatabase.FindAssets(card);

            this.addCardToDeckCollection(card, guids[0]);
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
        string[] guids1 = AssetDatabase.FindAssets("Monster");

        //cardDDL.options.Clear();
        foreach (string guid in guids1)
        {
            string card = AssetDatabase.LoadAssetAtPath<MonsterCard>(AssetDatabase.GUIDToAssetPath(guid)).name;
            if (card != null && card.Equals(cardDDL.options[cardDDL.value].text))
            {
                guidCardList.Add(card);
                addCardToDeckCollection(card, guid);
                break;
            }
        }
    }

    private void addCardToDeckCollection(string card, string guid)
    {
        GameObject newCard = new GameObject(card);

        UnityEngine.UI.Image newCardImage = newCard.AddComponent<UnityEngine.UI.Image>();
        newCardImage.sprite = AssetDatabase.LoadAssetAtPath<MonsterCard>(AssetDatabase.GUIDToAssetPath(guid)).GetComponent<UnityEngine.UI.Image>().sprite;
        newCardImage.preserveAspect = true;
        //                newCardImage.sprite.texture.width = AssetDatabase.LoadAssetAtPath<MonsterCard>(AssetDatabase.GUIDToAssetPath(guid)).GetComponent<UnityEngine.UI.Image>().sprite.texture.width;
        //                newCardImage.sprite.texture.height = AssetDatabase.LoadAssetAtPath<MonsterCard>(AssetDatabase.GUIDToAssetPath(guid)).GetComponent<UnityEngine.UI.Image>().sprite.texture.height;

        //scroll = GameObject.Find("CardScroll");
        if (deckCollection != null)
        {
            //ScrollViewGameObject container object
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
}
