using System.Collections.Generic;
using UnityEngine;

public class NoteNameStorage : MonoBehaviour
{
    private List<NotePrefab> _storageItem = null;

    private void Awake() => _storageItem ??= new List<NotePrefab>(GetComponentsInChildren<NotePrefab>());

    public NotePrefab StorageNoteName(Language language) => _storageItem[(int)language];
}
