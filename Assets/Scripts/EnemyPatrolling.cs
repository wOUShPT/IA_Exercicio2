using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    private Enemy enemyBehaviour;
    private List<EnemyStationary> _enemyStationaries;
    private Transform _playerTransform;

    void Start()
    {
        enemyBehaviour = GetComponent<Enemy>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AwareOfPlayer()
    {
        enemyBehaviour.GlobalDetection = true;
        enemyBehaviour.Alerted = true;
        enemyBehaviour.lastPlayerPosition = _playerTransform.position;
    }
}
