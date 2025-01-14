using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HouseCat : MonoBehaviour
{
    //[SerializeField] private NavMeshAgent agent;

    // Variables for smooth walking
    private float smoothTime = 0.3f;
    private Vector2 velocity = Vector3.zero;

    private bool isMovingToOption = false;

    [SerializeField] private Animator catAnimator;
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
        SetAgentPosition(targetPos);

        // Verify that the cat is currently moving
        if (isMovingToOption)
        {
            catAnimator.SetBool("IsMoving", true);
            
            if (gameObject.transform.position != targetTrans)
            {
                // Creates smooth transition with cat movement
                
            }
            else
            {
                catAnimator.SetBool(targetPos.GetAnimationBoolName(), true);
            }
        }
        else
        {
            catAnimator.SetBool("IsMoving", false);
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
