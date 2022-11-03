using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    //the class is responsible for storing decisions and transitioning from different states of behaviour

    [field:SerializeField] public List<AIDecision> Decisions { get; set; }
    [field: SerializeField] public AIState PositiveResult { get; set; }
    [field: SerializeField] public AIState NegativeResult { get; set; }

    private void Awake()
    {
        Decisions.Clear();
        Decisions = new List<AIDecision>(GetComponents<AIDecision>());
    }

}
