using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HousePosition : MonoBehaviour
{
    [Tooltip("The boolean name that triggers the animation used in this position")]
    [SerializeField] private string animationBoolName;

    public string GetAnimationBoolName() {  return animationBoolName; }

    public Vector3 GetPosition() { return transform.position; }
}
