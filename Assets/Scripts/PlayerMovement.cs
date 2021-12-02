using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Vector3 dir; //this is the direction we want to add force

    public Camera mainCam;

    [Tooltip("Speed multiplier for horizontal and Vertical movement.")]
    [Range(5,50)]  //adds a slider to drag
    public float speed = 10;
    public float jumpPower, dashForce = 5;
    Rigidbody rb;
    public float resetHeight = -5;

    public bool canJump = false;
    public bool canDash = false;
    bool isGrounded = true;
    public Vector3 startPosition;
    int coins = 0;

    public UIController ui;

    [Header("TriggerAudio")]
    public AudioSource aud;
    public AudioClip coinClip;
    public AudioClip powerClip;
    [Range(0f,1f)]
    public float endVolume = .5f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        if(ui == null) ui = GameObject.Find("Canvas").GetComponent<UIController>();
        startPosition = GameObject.Find("Start Here").transform.position;
        if(PlayerPrefs.GetInt("canJump") == 1)
        {
            canJump = true;
        }
        if(PlayerPrefs.GetInt("canDash") == 1)
        {
            canDash = true;
        }
        if(SceneManager.GetActiveScene().buildIndex == 13)
        {
            canDash = true;
        }
        ResetPlayer();
        if(mainCam == null) mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        rb.AddForce(dir * speed);

        //if player falls below the level
        if(this.transform.position.y < resetHeight)
        {
            ResetPlayer();
        }

    }

    public void ResetPlayer()
    {
        this.transform.position = startPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
    }

    public void Jump()
    {
        if(canJump)
        {
            if(isGrounded)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
        
    }

    public void Dash()
    {
        if(canDash)
        {
            //add dash cool down
            //could canel out velocity to move in new dir
            rb.velocity = Vector3.zero;
            rb.AddForce(dir * dashForce, ForceMode.Impulse);
            StartCoroutine(Wait());
        }
        
    }

    IEnumerator Wait(float waitTime = 1f)
    {
        canDash = false;  //if true, now it is not true
        yield return new WaitForSeconds(waitTime);
        canDash = true;  //if false, now it is not false
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("JumpUp"))
        {
            aud.PlayOneShot(powerClip);
            canJump = true;
            PlayerPrefs.SetInt("canJump", 1);  //1 is true, 0 is false
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Dash"))
        {
            aud.PlayOneShot(powerClip);
            canDash = true;
            PlayerPrefs.SetInt("canDash", 1);  //1 is true, 0 is false
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
        if(other.gameObject.CompareTag("Sand"))
        {
            isGrounded = true;
            rb.mass *= 2;
        }
        if(other.gameObject.CompareTag("Coin"))
        {
            aud.PlayOneShot(coinClip);
            ui.UpdateScore();
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("AltCam"))
        {
            mainCam.gameObject.SetActive(false);
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
        if(other.gameObject.CompareTag("AltCam"))
        {
            mainCam.gameObject.SetActive(true);
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
        if(other.gameObject.CompareTag("Sand"))
        {
            isGrounded = true;
            rb.mass /= 2;
        }
    }
}
