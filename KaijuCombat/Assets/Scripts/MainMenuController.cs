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

public class MainMenuController : MonoBehaviour
{
    public GameObject escapeUI;
    public bool isShowing;

    struct Scenes
    {
        public int main_menu_scene;
        public int deck_builder_scene;
        public int game_scene;

        public Scenes(int main_menu_scene, int deck_builder_scene, int game_scene)
        {
            this.main_menu_scene = main_menu_scene;
            this.deck_builder_scene = deck_builder_scene;
            this.game_scene = game_scene;
        }
    }

    // Loads the next scene (File -> Build Settings -> Scenes in Build)
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ResumeGame()
    {   
        isShowing = !isShowing;
        escapeUI.SetActive(isShowing);
    }

    // Kills the application
    public void KillGame()
    {
        Application.Quit();
    }

    // Loads the DeckBuilder scene. 
    public void DeckBuilder()
    {
        SceneManager.LoadScene("DeckBuilderScene");
    }
}
