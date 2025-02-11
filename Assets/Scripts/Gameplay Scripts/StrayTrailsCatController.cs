using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrayTrailsCatController : MonoBehaviour
{
    [SerializeField] private StrayTrailsInputController inputController;
    [SerializeField] private GameAudioController audioController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle"))
        {
            audioController.PlayDenied();
            inputController.StopStrayTrails();
        }
    }
}
