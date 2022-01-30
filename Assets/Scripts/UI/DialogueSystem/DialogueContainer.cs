using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using System;

public class DialogueContainer : MonoBehaviour
{
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _contentText = null;
    [SerializeField] private Canvas _gameObject = null;
    [SerializeField] private CanvasGroup _canvasGroup = null;
    [SerializeField] private Transform _dialogueWindow = null;
    [SerializeField] private Transform _toPosition = null;
    [SerializeField] private Transform _fromPosition = null;
    [SerializeField] private float _durationAnimation = 0f;
    [Space]
    [SerializeField] private List<Dialogue> _dialogues = null;

    private Dialogue _currentDialogue;

    private Singleton _singleton = Singleton.GetInstance();

    private int _replicaIndex;


    private void SetClick()
    {
        EventTrigger eventTrigger = _dialogueWindow.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => DisplayNextReplica());
        if (eventTrigger.triggers.Count > 0)
            eventTrigger.triggers.RemoveRange(0, eventTrigger.triggers.Count);
        eventTrigger.triggers.Add(entry);
    }
    public void StartDialogue(string nameOfDialogue)
    {
        Sequence sequence = DOTween.Sequence();
        _gameObject.enabled = true;
        sequence.AppendCallback(() =>
        {
            _dialogueWindow.DOMove(_toPosition.position, _durationAnimation, false);
            _canvasGroup.DOFade(1f, _durationAnimation);
        });
        SetClick();
        _currentDialogue = GetDialogue(nameOfDialogue);
        _replicaIndex = 0;
        DisplayNextReplica();
    }
    public void DisplayNextReplica()
    {
        if (_replicaIndex >= _currentDialogue.Replicas.Length)
        {
            EndDialogue();
            return;
        }

        string copyContentText = _currentDialogue.Replicas[_replicaIndex].Content;
        _nameText.text = _currentDialogue.Replicas[_replicaIndex].Name;
        _contentText.text = "";
        _contentText.DOText(copyContentText, 0.4f);

        _replicaIndex++;
    }
    public Dialogue GetDialogue(string nameOfDialogue)
    {
        foreach (Dialogue item in _dialogues)
            if (item.Name.ToLower().Trim() == nameOfDialogue.ToLower().Trim())
                return item;

        throw new IndexOutOfRangeException();
    }
    private void EndDialogue()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            _dialogueWindow.DOMove(_fromPosition.position, _durationAnimation, false);
            _canvasGroup.DOFade(0f, _durationAnimation);
        });
        sequence.AppendInterval(_durationAnimation);
        sequence.AppendCallback(() =>
        {
            _singleton.Data.Settings.IsPause = _gameObject.enabled = false;
        });
    }
}
