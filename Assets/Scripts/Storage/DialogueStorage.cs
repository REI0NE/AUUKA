using System.Collections.Generic;
using UnityEngine;

public class DialogueStorage : MonoBehaviour
{

    private List<DialogueContainer> _storageDialogue = null;

    private void Awake() => _storageDialogue ??= new List<DialogueContainer>(GetComponentsInChildren<DialogueContainer>());

    public DialogueContainer StorageDialogue(Language language) => _storageDialogue[(int)language];
}
