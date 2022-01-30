using UnityEngine;

public class InteractionStateWatch : MonoBehaviour, IInteractionState
{
    [SerializeField]
    [TextArea(5, 10)] private string _description = null;

    private void Start() { }

    public void OnClick()
    {
        if (!string.IsNullOrEmpty(_description))
            Debug.Log(_description);
    }
}
