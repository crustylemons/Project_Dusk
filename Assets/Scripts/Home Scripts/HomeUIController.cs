using System.Collections;
using UnityEngine;

public class HomeUIController : MonoBehaviour
{
    [SerializeField] private GameObject esc;

    void Update()
    {
        if (!esc.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            esc.SetActive(true);
        }
    }

    public void DisableEsc() { esc.SetActive(false); }
}
