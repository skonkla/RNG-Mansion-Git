using DefaultNamespace;
using UnityEngine;

public class Notes : MonoBehaviour, IInteractable
{
    public string InteractMessage => objectInteractMessage;

    [SerializeField]
    string objectInteractMessage;

    public GameObject[] noteText;
    public GameObject noteBG;

    public void Interact()
    {
        noteBG.SetActive(true);
        noteText[0].SetActive(true);
    }
}
