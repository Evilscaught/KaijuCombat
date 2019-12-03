/* Author(s):           Scott McKay
 * Group:               The Omnipotent Creators
 * Date of Creation:    Friday, November 29th, 2019 at 4:34 p.m. M.S.T. (Autumn Semester MMXIX)
 * Last Edit:           
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Loads all the sound files into the game.
 */ 


using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 
/// </summary>
// 'Serializable' makes class accessible from Unity editor inspector
[System.Serializable] 
public class Sound
{
    public string name;
    public AudioClip clip;

    // The range specifies the slider properties in the inspector
    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(0.1f, 3.0f)]
    public float pitch;

    public bool loop;

    // 'HideInInspector' makes attribute inaccessible from the Unity editor inspector
    // This variable is populated automatically in the awake method (see AudioManager.cs) 
    // which is why 'HideInInspector' is used.
    [HideInInspector]
    public AudioSource source;

}
