using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public PlayerMovement player;
    Vector3 arrowScale;

    void Start()
    {
        arrowScale = Vector3.one;
    }


    void LateUpdate()
    {
        this.transform.rotation = Quaternion.LookRotation(player.dir, Vector3.up);
        arrowScale.z = player.dir.magnitude;
        this.transform.localScale = arrowScale;
    }
}
