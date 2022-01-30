using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Diary : MonoBehaviour
{
    [SerializeField] private Transform _parrent = null;
    [SerializeField] private TextMeshProUGUI _right = null;
    [SerializeField] private Button _butPrefab = null;
    [SerializeField] private int _countNote = 17;

    private Animator _animator = null;
    private Canvas _canvas = null;

    private List<Button> _left = null;
    private List<int> _notes = null;
    private NoteNameStorage _noteName = null;
    private NoteStorage _noteStorage = null;
    private DialogueStorage _dialogueStorage = null;
    private End _end = null;

    private Singleton _singleton = Singleton.GetInstance();

    private int _endNoteRead = 0;

    private void Awake()
    {
        _animator ??= GetComponent<Animator>();
        _canvas ??= GetComponentsInChildren<Canvas>()[1];
        _noteName ??= FindObjectOfType<NoteNameStorage>();
        _noteStorage ??= FindObjectOfType<NoteStorage>();
        _end ??= FindObjectOfType<End>();
        _dialogueStorage ??= FindObjectOfType<DialogueStorage>();
    }

    private void Start() => CreateNote();

    private void CreateNote()
    {
        _left ??= new List<Button>(_countNote);
        for (int i = 0; i < _countNote; i++)
        {
            Button button = Instantiate(_butPrefab, _parrent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = _noteName.StorageNoteName(_singleton.Data.Settings.Language).Notes[0].Description + " " + (i + 1).ToString();
            button.onClick.RemoveAllListeners();
            int index = i;
            button.onClick.AddListener(() => ReadNote(index));
            button.gameObject.SetActive(false);
            _left.Add(button);
        }
        _right.text = null;
    }

    private void ReadNote(int number) => _right.text = _noteStorage.StorageNoteText(_singleton.Data.Settings.Language).Notes[number].Description;

    public void NewNote(int number)
    {
        _notes ??= new List<int>(_countNote);
        _notes.Add(number);
        _left[number].gameObject.SetActive(true);
        Refresh();
    }

    public void Open(int number)
    {
        if (!_canvas.enabled)
            _animator.SetTrigger("Toggle");

        _endNoteRead = number;

        ReadNote(number);
    }

    public void Refresh()
    {
        _right.text = null;
        int index = 0;
        _left.ForEach(but =>
        {
            but.GetComponentInChildren<TextMeshProUGUI>().text = _noteName.StorageNoteName(_singleton.Data.Settings.Language).Notes[0].Description + " " + (index + 1).ToString();
            index++;
        });

        if (_canvas.enabled)
            if (_endNoteRead == _countNote - 1)
                _end.TheEnd(_dialogueStorage.StorageDialogue(_singleton.Data.Settings.Language).GetDialogue("two end").Replicas[0].Content);
    }
}
