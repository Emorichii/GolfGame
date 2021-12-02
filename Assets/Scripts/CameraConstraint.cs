using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraConstraint : MonoBehaviour
{
    PositionConstraint pC;
    public Transform player;
    ConstraintSource source;

    void Start()
    {
        //ref to position constraint
        pC = this.GetComponent<PositionConstraint>();
        //find player
        if(player == null)
        {
            player = GameObject.Find("Player").transform;
        }
        //add player to the position constraint sources
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = player;
        source.weight = 1;
        //activate the position constraint
        pC.AddSource(source);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
