using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        //sets the movementdirection to an empty vector
        aiMovementData.Direction = Vector2.zero;
        aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        //calls the move function with movementdirection 0,0, which stops the enemy from moving, but keeps the enemy turning in the right direction
        enemyBrain.Move(aiMovementData.Direction, aiMovementData.PointOfInterest);
        aiActionData.Attack = true;
        enemyBrain.Attack();
    }
}
