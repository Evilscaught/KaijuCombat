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
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject escapeUI;
    private bool isShowing;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            escapeUI.SetActive(isShowing);
        }
    }
}
