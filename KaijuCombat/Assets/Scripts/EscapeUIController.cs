/* Author(s):           Scott McKay
 * Group:               The Omnipotent Creators
 * Date of Creation:    Thursday, November 14th, 2019 @11:00 p.m. M.S.T. (Autumn Semester MMXIX)
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Implements escape functionality during the game session for the user.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeUIController : MonoBehaviour
{
    public GameObject escapeUI;
    public GameObject obscuringPanel;
    public GameObject settings;
    private bool isShowing;

    // Update is called once per frame
    void Update()
    { 
        //If the user presses the 'escape' key, make quick menu visible.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            escapeUI.SetActive(isShowing);
            obscuringPanel.SetActive(isShowing);
            settings.SetActive(false);

            obscuringPanel.transform.SetSiblingIndex(GameManager.instance.canvas.transform.childCount - 1);
            escapeUI.transform.SetSiblingIndex(GameManager.instance.canvas.transform.childCount - 1);
        }

        if(isShowing)
        {
            obscuringPanel.transform.SetSiblingIndex(GameManager.instance.canvas.transform.childCount - 1);
            escapeUI.transform.SetSiblingIndex(GameManager.instance.canvas.transform.childCount - 1);
        }
    }

    /// <summary>
    /// Hides the quick menu
    /// </summary>
    public void Resume()
    {
        isShowing = !isShowing;
        escapeUI.SetActive(isShowing);
        obscuringPanel.SetActive(isShowing);
        settings.SetActive(false);
    }

    /// <summary>
    /// Exits the Unity Game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Plays button click sound
    /// </summary>
    public void ClickInitiateST()
    {
        FindObjectOfType<AudioManager>().Play("ClickInitiate");
    }
}
