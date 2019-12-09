using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public GameObject packTitle;
    public GameObject packDescription;
    public GameObject packImage;
    public GameObject buyButton;
    public GameObject buyText;
    public GameObject infoPanel;
    public GameObject playerWallet;
    public GameObject overlayPanel;
    public GameObject overlayTitle;
    public GameObject overlayDescription;

    private double funds = 0.00;
    private double currentPrice = 0.00;

    private void Start()
    {

    }
    public void Back()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Update()
    {
        playerWallet.GetComponent<Text>().text = "Wallet: $"+String.Format("{0:0.00}",funds);
    }

    public void Pack1_Select()
    {
        infoPanel.SetActive(true);
        currentPrice = 0.99;

        // change pack title
        packTitle.GetComponent<TextMeshProUGUI>().SetText("Basic Pack");

        // change pack description & price
        packDescription.GetComponent<TextMeshProUGUI>().SetText("Basic Description");
        buyText.GetComponent<TextMeshProUGUI>().SetText("$0.99");
    }

    public void Pack2_Select()
    {
        infoPanel.SetActive(true);
        currentPrice = 1.99;

        // change pack title
        packTitle.GetComponent<TextMeshProUGUI>().SetText("Rare Pack");
        // change pack description & price
        packDescription.GetComponent<TextMeshProUGUI>().SetText("Rare Description");
        buyText.GetComponent<TextMeshProUGUI>().SetText("$1.99");
    }

    public void Pack3_Select()
    {
        infoPanel.SetActive(true);
        currentPrice = 4.99;

        // change pack title
        packTitle.GetComponent<TextMeshProUGUI>().SetText("Ultra-Rare Pack");
        // change pack description & price
        packDescription.GetComponent<TextMeshProUGUI>().SetText("Ultra-Rare Description");
        buyText.GetComponent<TextMeshProUGUI>().SetText("$4.99");
    }

    public void buyPack()
    {
        if (funds >= currentPrice)
        {
            // buy pack
            funds -= currentPrice;
            overlayTitle.GetComponent<TextMeshProUGUI>().SetText("Success!");
            overlayDescription.GetComponent<TextMeshProUGUI>().SetText("Card pack has been purchased.");
        }
        else
        {
            // insufficient funds
            overlayTitle.GetComponent<TextMeshProUGUI>().SetText("Error!");
            overlayDescription.GetComponent<TextMeshProUGUI>().SetText("Insufficient funds.");
        }

        enableOverlay();
    }

    public void addFunds()
    {
        funds += 20.00;
    }

    public void packOverlay(int packType)
    {
        // select three cards based on pack type
            // if 0 -> 3 uncommon/common
            // if 1 -> 1 rare, 2 rare/uncommon/common
            // if 2 -> 1 ultra-rare, 2 random cards (ultra, rare, common, uncommon)

        // display cards & rarity

        // show overlay
    }

    public void disableOverlay()
    {
        // disable overlay
        overlayPanel.SetActive(false);
        buyButton.GetComponent<Button>().interactable = true;

    }

    public void enableOverlay()
    {
        // enable overlay
        overlayPanel.SetActive(true);
        buyButton.GetComponent<Button>().interactable = false;

    }
}
