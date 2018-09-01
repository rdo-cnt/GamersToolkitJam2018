/*
 * GMTK Jam 2018
 * MintFox
 * Sigrath
 * Basinga
 * Cuurian
 * Ikkir
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    //Creating singleton
    public static GameController instance { get { return _instance; } }
    private static GameController _instance = null;
    public LevelController levelController;

    //Level managing variables
    public static int levelIndex;

    //audio list
    public AudioSource soundPlayer;
    public AudioSource musicPlayer;
    public AudioClip deathEffect;
    //public AudioClip[] soundEffects;


    void Awake()
    {
        //Additional singleton setup
        _instance = this;
        DontDestroyOnLoad(gameObject);

        //Check our current room index
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        if (Input.GetKeyDown("n"))
        {
            nextScene();
        }
        if (!levelController) //simple for now, add in maybe a check to see if its in a level scene later
        {
            levelController = GameObject.FindObjectOfType<LevelController>();
        }

    }

    void InitValues()
    {
        //insert whatever here later
    }

    public static void nextScene()
    {
        int n = (levelIndex + 1);
        //Go back to the First Scene if this is the last scene
        if (n >= Application.levelCount)
        {
            levelIndex = 0;
        }
        else
        {
            levelIndex = n;
        }
        SceneManager.LoadScene(levelIndex);
    }

    public static void previousScene()
    {
        int n = (levelIndex - 1);
        //edge Case, avoids going lower than 0 as a level index
        if (n <= 0)
        {
            levelIndex = 0;
        }
        else
        {
            levelIndex = n;
        }
        SceneManager.LoadScene(levelIndex);
    }

    public static void restartScene()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void PlaySoundClip(int soundIndex)
    {
        if (soundIndex == 0)
        {
            soundPlayer.PlayOneShot(deathEffect);
        }
    }





}
