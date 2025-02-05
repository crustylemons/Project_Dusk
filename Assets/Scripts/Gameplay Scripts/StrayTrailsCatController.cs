using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrayTrailsCatController : MonoBehaviour
{
    [SerializeField] private StrayTrailsInputController inputController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle"))
        {
            inputController.StopStrayTrails();
        }
    }
}
