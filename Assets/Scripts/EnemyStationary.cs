using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStationary : MonoBehaviour
{
    private Enemy enemyBehaviour;
    public UnityEvent AlertOthers;
    private List<EnemyPatrolling> _enemyStationaries;
    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = GetComponent<Enemy>();
        AlertOthers = new UnityEvent();
        _enemyStationaries = FindObjectsOfType<EnemyPatrolling>().ToList();
        foreach (var enemy in _enemyStationaries)
        {
            AlertOthers.AddListener(enemy.AwareOfPlayer);
        }
    }
    
    void Update()
    {
        if (enemyBehaviour.PlayerDetected)
        {
            AlarmAll();
        }
    }
    
    void AlarmAll()
    {
        AlertOthers.Invoke();
    }
}
