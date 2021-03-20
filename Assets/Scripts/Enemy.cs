using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using UnityEditor.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform playerTransform;
    [Task]
    private bool playerDetected;
    [Task]
    private bool alerted;
    private Vector3 startPosition;
    private Vector3 lastPlayerPosition;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        playerDetected = false;
        alerted = false;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, (playerTransform.position-transform.position).normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, 100) && hit.transform.CompareTag("Player"))
        {
            playerDetected = true;
            alerted = true;
            lastPlayerPosition = playerTransform.position;
        }
        else
        {
            playerDetected = false;
        }
    }

    [Task]
    void Chase()
    {
        agent.SetDestination(lastPlayerPosition);
        Task.current.Succeed();
    }

    [Task]
    void CheckAround()
    {
        if (playerDetected)
        {
            Task.current.Fail();
            return;
        }
        agent.SetDestination(transform.position + Random.rotation*Vector3.forward*3);
        Task.current.Succeed();
    }

    [Task]
    void CheckLastPosition()
    {
        agent.SetDestination(lastPlayerPosition);
        if (transform.position == lastPlayerPosition)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void ForgetPlayer()
    {
        alerted = false;
        Task.current.Succeed();
    }

    [Task]
    void ReturnToOrigin()
    {
        agent.SetDestination(startPosition);
        if (transform.position == startPosition)
        {
            Task.current.Succeed();
        }
    }
}