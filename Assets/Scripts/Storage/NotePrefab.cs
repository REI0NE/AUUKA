using System.Collections.Generic;
using UnityEngine;

public class NotePrefab : MonoBehaviour
{
    [SerializeField] private List<ItemTemp> _notes = null;
    public List<ItemTemp> Notes => _notes;
}
