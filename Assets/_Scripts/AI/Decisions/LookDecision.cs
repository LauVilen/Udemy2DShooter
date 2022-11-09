using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LookDecision : AIDecision
{
    [SerializeField] [Range(1,15)]
    private float distance = 15;
    [SerializeField]
    private LayerMask rayCastMask = new LayerMask();

    private Vector3 direction;

    [field: SerializeField] public UnityEvent OnPlayerSpotted { get; set; }
    public override bool MakeADecision()
    {
        direction = enemyBrain.Target.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, rayCastMask);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnPlayerSpotted?.Invoke();
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject && enemyBrain != null && enemyBrain.Target != null)
        {
            Gizmos.color = Color.red;
            direction = enemyBrain.Target.transform.position - transform.position;
            Gizmos.DrawRay(transform.position,direction.normalized*distance);
        }
    }
}
