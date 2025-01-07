using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCat : MonoBehaviour
{
    // Variables for smooth walking
    private float smoothTime = 0.3f;
    private Vector2 velocity = Vector3.zero;

    private bool isMovingToOption = false;

    [SerializeField] Animator catAnimator;
    [SerializeField] HousePosition targetPos;

    [SerializeField] HousePosition[] positions;

    private void Update()
    {
        // Verify that the cat is currently moving
        if (isMovingToOption)
        {
            catAnimator.SetBool("isMoving", true);
            
            if (gameObject.transform.position != targetPos.transform.position)
            {
                // Creates smooth transition with cat movement
                gameObject.transform.position = Vector2.SmoothDamp(gameObject.transform.position, targetPos.transform.position, ref velocity, smoothTime);
            }
            else
            {
                catAnimator.SetBool(targetPos.GetAnimationBoolName(), true);
            }
        }
        else
        {
            catAnimator.SetBool("isMoving", false);
        }
    }

    public void MoveToOption(HousePosition position)
    {
        targetPos = position;
        isMovingToOption = true;
    }
}
