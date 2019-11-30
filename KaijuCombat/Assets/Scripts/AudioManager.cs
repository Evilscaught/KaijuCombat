/* Author(s):           Scott McKay
 * Group:               The Omnipotent Creators
 * Date of Creation:    Friday, November 29th, 2019 at 4:34 p.m. M.S.T. (Autumn Semester MMXIX)
 * Last Edit:           
 * Class:               Game Engines
 * Instructor:          Michael Cassens
 * Institution:         The University of Montana
 * 
 * Purpose of Class:    Audio Manager for Kaiju Combat. 
 * Note: use `FindObjectOfType<AudioManager>().Play("<song_name>"); in any script to play music.
 */

using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Static reference to the audio manager in the current scene
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        // Ensures that there is only one instance of the audio manager at any given time
        if  (instance == null)
        {
            instance = this;
        }
        // Ensures that when switching scenes the audio manager is not removed
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            //Store the audio source and its properties into sounds array
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        } 
    }

    /// <summary>
    /// Starts playing the game soundtrack on start
    /// </summary>
    void Start()
    {
        Play("Frontend");
    }

    /// <summary>
    /// Plays the song by name
    /// </summary>
    /// <param name="name">string: name of the song</param>
    public void Play (string name)
    {
        //Find the sound in the sounds array by name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //Print warning to console if name is not matched to any files.
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {

    }
}