using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip pressedSound;
    [SerializeField] private AudioSource AudioSource;


    private void Start()
    {

        if (hoverSound == null)
        {
            Debug.Log($"There is no sound effect for button {gameObject.name}");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (AudioSource != null && hoverSound != null)
        {
            AudioSource.PlayOneShot(hoverSound);
        }
        else { Debug.Log($"Cannot play sound effect on {gameObject.name}"); }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && pressedSound != null)
        {
            AudioSource.PlayOneShot(pressedSound);
        }
    }

}
