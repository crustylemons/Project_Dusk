using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HouseCat : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    [SerializeField] private HousePosition[] positions;
    [SerializeField] private HousePosition targetPos;
    [SerializeField] private Vector3 targetTrans;

    private HousePosition previousPos;


    private void Start()
    {
        // NavMesh Pro
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void StartPositionControl()
    {
        StartCoroutine(PositionControl());
    }

    private IEnumerator PositionControl()
    {
        while (true)
        {
            // If gameobject is at the position
            if (Vector3.Distance(transform.position, targetTrans) < 0.1f && targetPos != null)
            {
                Animate(true);

                // stay in position for random amound of time
                int rnd = Random.Range(30, 45);
                print(rnd + " amount of seconds before new position");
                yield return new WaitForSeconds(rnd);

                // Set new targetPos
                ChooseNewPosition();

                Debug.Log("Going to new position");
            }
            // If gameobject is not at the position
            else
            {
                Animate(false);
            }


            SetAgentPosition(targetPos);
            yield return null;
        }
    }

    private void Animate(bool isAtPosition)
    {
        if (previousPos != null)
        {
            animator.SetBool(previousPos.GetAnimationBoolName(), false);
        }

        // Animation bool control
        if (!isAtPosition)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool(targetPos.GetAnimationBoolName(), true);

        }


        // Sprite rotation control
        if (targetTrans.x < gameObject.transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    // Sets the targetPos by random
    private void ChooseNewPosition()
    {
        // Return if there is one or less value in the array
        if (positions.Length < 2)
        {
            Debug.Log("There are one or less positions");
            return;
        }
        
        while (true)
        {
            // Choose random position
            HousePosition chosenPos = positions[Random.Range(0, positions.Length)];

            // Verify value isn't previous value
            if (chosenPos != targetPos)
            {
                previousPos = targetPos;
                targetPos = chosenPos;
                break;
            }
        }
    }

    private void SetAgentPosition(HousePosition position)
    {
        targetPos = position;
        Vector3 pos = position.GetPosition();
        agent.SetDestination(new Vector3(pos.x, pos.y, 0));
        targetTrans = pos;
    }
}
