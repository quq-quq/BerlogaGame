using System;
using System.Collections.Generic;
using Node;
using UnityEngine;

namespace DefaultNamespace.Manual
{
    [CreateAssetMenu]
    public class NodeInfo : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private string _mainText;
        [SerializeField] private BaseNode _prefab;
        [SerializeField] private List<NodeInfo> _links;

        public IReadOnlyCollection<NodeInfo> Links => _links;

        public string Title => _title;

        public string MainText => _mainText;

        public BaseNode Prefab => _prefab;
    }
}