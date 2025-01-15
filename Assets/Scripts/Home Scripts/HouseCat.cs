using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class HouseCat : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 targetTrans;

    [SerializeField] private HousePosition[] positions;
    [SerializeField] private HousePosition targetPos;

    [SerializeField] private NavMeshAgent agent;

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        Animate();
        SetAgentPosition(targetPos);
    }

    private void Animate()
    {
        if (Vector3.Distance(gameObject.transform.position, targetTrans) > 0.1f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool(targetPos.GetAnimationBoolName(), true);

        }

        if (targetTrans.x < gameObject.transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void SetAgentPosition(HousePosition position)
    {
        targetPos = position;
        Vector3 pos = position.GetPosition();
        agent.SetDestination(new Vector3(pos.x, pos.y, 0));
        targetTrans = pos;
    }
}
