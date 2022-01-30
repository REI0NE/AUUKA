using UnityEngine;

public class InteractionStateEnd : MonoBehaviour, IInteractionState
{
    [SerializeField] private string _text = null;

    private DialogueStorage _dialogueStorage = null;

    private End _end = null;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake()
    {
        _dialogueStorage ??= FindObjectOfType<DialogueStorage>();
        _end ??= FindObjectOfType<End>();
    }

    public void OnClick()
    {
        if (_end != null)
            _end.TheEnd(_dialogueStorage.StorageDialogue(_singleton.Data.Settings.Language).GetDialogue(_text).Replicas[0].Content);
    }
}
