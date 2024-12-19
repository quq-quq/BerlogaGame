using Dialogue_system;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Copilot Phrases Config", menuName = "Copilot Phrases Config")]
public class CopilotPhrasesConfig : ScriptableObject
{
    [Tooltip("Set phrases to the priory meaning of suggestion. If suggestion less helpful, keep it higher")]
    [Space(10)]
    [SerializeField, Multiline] private List<string> _phrases;

    public IReadOnlyList<string> Phrases
    {
        get => _phrases;
    }
}
