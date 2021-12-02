using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [Tooltip("The name of the level you want to go to.")]
    public string destination = "Level 2";

    public int levelWithJump = 7;
    public int levelWithDash = 11;

    [Header("EndAudio")]
    public AudioSource aud;
    public AudioClip endClip;
    [Range(0f,1f)]
    public float endVolume = .5f;

    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("canJump", 0);
            PlayerPrefs.SetInt("canDash", 0);
            Time.timeScale = 1;
            
        }
        else if(SceneManager.GetActiveScene().buildIndex >= levelWithJump)
        {
            PlayerPrefs.SetInt("canJump", 1);
        }
        else if(SceneManager.GetActiveScene().buildIndex >= levelWithDash)
        {
            PlayerPrefs.SetInt("canDash", 1);
        }
        else if(SceneManager.GetActiveScene().buildIndex >= levelWithDash)
        {
            PlayerPrefs.SetInt("canDash", 1);
        }
    }

    public void ChangeScene() //(string destination = "")
    {
        // use player prefs to save the current level + 1
        PlayerPrefs.SetInt("Progress", SceneManager.GetActiveScene().buildIndex + 1);

        // if(destination == "")
        // {
        //     //dev forgot to add destination, going to main menu
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1)
        // }
        // else
        // {
        //     SceneManager.LoadScene(destination);
        // } 

        SceneManager.LoadScene(destination);
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.startPosition = GameObject.Find("Start Here").transform.position;
        player.ResetPlayer();        

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            aud.PlayOneShot(endClip);
            ChangeScene();
        }
    }


    /*
    at the start of a new level
    1.the game manager
    2.
    3.

    at the end of a level
    1.the ui controller saves score
    2.the player saves powerups
    3.how do we call these functions

    at main menu and credits
    1. non of that stuff happened
    */
}
