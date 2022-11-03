using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        //takes the position of the target (the player) and subtracts the current position of the gameObject to get a direction, then sets the Direction property of aiMovementData prop.
        var direction = enemyBrain.Target.transform.position - transform.position; 
        aiMovementData.Direction = direction.normalized;
        //sets PointOfInterest prop of aiMovementData to be the current position of the player (-> to flip enemy sprite)
        aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        //Calls the Move method, passing the vectors, that we just set, as arguments
        enemyBrain.Move(aiMovementData.Direction, aiMovementData.PointOfInterest);
    }
}
