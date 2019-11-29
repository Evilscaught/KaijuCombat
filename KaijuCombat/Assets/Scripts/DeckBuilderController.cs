/* Author(s):           Scott McKay
 * Group:               The Omnipotent Creators
 * Date of Creation:    Monday, November 11th, 2019 at 7:30 p.m. M.S.T. (Autumn Semester MMXIX)
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Implements functions for the deck builder scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckBuilderController : MonoBehaviour
{
    /// <summary>
    /// Loads the MenuScene
    /// </summary>
    public void back()
    {
        SceneManager.LoadScene("MenuScene");
    }

    //TODO: Add code that creates custom deck. 
    public void play()
    {
        SceneManager.LoadScene("GameScene");
    }
}
