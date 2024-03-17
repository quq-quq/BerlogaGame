using System;
using Node;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ObjectNode : BaseNode
    {
        [SerializeField] private string _objectName;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private TMP_Text _nameText;

#if UNITY_EDITOR
        [ContextMenu("Apply Text")]
        private void ApplyText()
        {
            if (_nameText == null)
                _nameText = GetComponentInChildren<TMP_Text>();
            _nameText.text = _objectName;
        }
#endif

        public override void Do(GameObject go)
        {
            Connector.GetConnectedNodes().ForEach(i => i.Do(_gameObject));
        }
    }
}
