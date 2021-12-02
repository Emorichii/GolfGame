using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform startPosition;
    
    // create the player before and other Start() functions begin
    void Awake()
    {
        if(startPosition == null)
        {
            Debug.Log("<color=red>you have not selected a start position.</color>");
        }
        Instantiate(playerPrefab, startPosition.position, startPosition.rotation);
    }
}
