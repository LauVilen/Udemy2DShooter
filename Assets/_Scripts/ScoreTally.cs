using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreTally : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private UIScore uiScore = null;
    [field: SerializeField] public EnemySpawnDataSO EnemySpawnData { get; set; }
    [field:SerializeField] public UnityEvent<int> OnScoreChange { get; set; }
    [field: SerializeField] public UnityEvent OnWin { get; set; }
    public void AddToScore()
    {
        score ++;
        OnScoreChange?.Invoke(score);
    }

    private void Start()
    {
        OnScoreChange.AddListener(uiScore.UpdateScore);
    }

    private void Update()
    {
        if (score == EnemySpawnData.amountToSpawn)
        {
            OnWin?.Invoke();
        }
    }
}
