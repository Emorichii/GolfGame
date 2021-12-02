using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    public PlayerMovement player;

    public bool paused = false;

    public GameObject pauseMenu;

    void Start()
    {
       if(player == null)
       {
           player = GameObject.Find("Player").GetComponent<PlayerMovement>();
       } 

       if(pauseMenu == null) pauseMenu = GameObject.Find("PauseMenu");
       pauseMenu.SetActive(paused);
    }

    void Pause()
    {
        paused = !paused;
        if(paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    //FixedUpdate is called 50 times per second
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
                // #if UNITY_EDITOR
                //     UnityEditor.EditorApplication.isPlaying = false;
                // #endif
            }
        }

        if(player == null)
        {
           player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }
        //call movement every frame and send it axis data
        player.dir.x = Input.GetAxis("Horizontal");
        player.dir.z = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.Dash();
        }
        if(Input.GetKeyDown(KeyCode.F5))
        {
            PlayerPrefs.SetInt("canJump", 0);
        }
    }
}
