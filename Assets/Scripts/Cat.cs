using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private TypingInputController typingController;

    [SerializeField] private Animator animator;

    // Variables for smooth walking
    private float smoothTime = 0.3f;
    private Vector2 velocity = Vector3.zero;

    private bool isFacingLeft = false;


    private void Update()
    {

        // Creates smooth transition with cat movement
        gameObject.transform.position = Vector2.SmoothDamp(gameObject.transform.position, typingController.GetTargetPos(), ref velocity, smoothTime);

        Vector2 currentPos = new Vector2(Mathf.Round((transform.position.x * 100)/100), Mathf.Round((transform.position.y * 100) / 100));

        if (currentPos != typingController.GetTargetPos())
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }


        if (currentPos.x > typingController.GetTargetPos().x)
        {
            if (!isFacingLeft)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }
        }
        else
        {
            if (isFacingLeft)
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                isFacingLeft = false;
            }
        }
    }

    public Animator GetAnimator() { return animator; }
}
