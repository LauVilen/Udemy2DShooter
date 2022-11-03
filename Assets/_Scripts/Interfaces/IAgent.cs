using UnityEngine;
using UnityEngine.Events;

public interface IAgent
{
    int Health { get; } //default value
    UnityEvent OnGetHit { get; set; }
    UnityEvent OnDie { get; set; }
}