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

public class MenuContainer : MonoBehaviour
{
    public void PlayGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
