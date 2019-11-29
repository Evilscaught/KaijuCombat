/* Author(s):           Scott McKay
 * Group:               The Omnipotent Creators
 * Date of Creation:    Saturday, November 9th, 2019 at 1:30 p.m. M.S.T. (Autumn Semester MMXIX)
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Implements button functions for the main menu user interface
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// A class that contains all the methods that provide functionality to the main menu in the Unity Scene MenuScene
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Loads the Unity Scene GameScene
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Kills the Unity application
    /// </summary>
    public void killGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Loads the Unity Scene DeckBuilderScene
    /// </summary>
    public void deckBuilder()
    {
        SceneManager.LoadScene("DeckBuilderScene");
    }
}
