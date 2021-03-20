using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class COMPLETE_PlayerController : MonoBehaviour
{
    [SerializeField]private Camera cam;
    [SerializeField]private NavMeshAgent agent;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        agent.autoTraverseOffMeshLink = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (agent.isOnOffMeshLink)
        {
            StartCoroutine(Warp());
        }
    }

    IEnumerator Warp()
    {
        _animator.SetBool("Warp", true);
        yield return new WaitForSeconds(1f);
        _animator.SetBool("Warp", false);
        agent.CompleteOffMeshLink();
        yield return null;
    }
}