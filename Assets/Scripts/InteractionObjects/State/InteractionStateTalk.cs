using UnityEngine;

public class InteractionStateTalk : MonoBehaviour,IInteractionState
{
    [SerializeField] private string _nameDialogue = null;

    private DialogueStorage _dialogueStorage = null;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake() => _dialogueStorage ??= FindObjectOfType<DialogueStorage>();

    private void Start() { }
    public void OnClick()
    {
        if (_dialogueStorage != null)
        {
            _singleton.Data.Settings.IsPause = true;
            _dialogueStorage.StorageDialogue(_singleton.Data.Settings.Language).StartDialogue(_nameDialogue);
        }
    }
}
