using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Manual
{
    public class NodeLink : MonoBehaviour
    {
        [SerializeField] private Transform _prefabContainer;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _title;

        private NodeInfo _nodeInfo;
        private Manual _manual;
        public void Init(NodeInfo nodeInfo, Manual manual)
        {
            var go = Instantiate(nodeInfo.Prefab, _prefabContainer);
            go.transform.localPosition = Vector3.zero;
            _title.text = nodeInfo.Title;
            _manual = manual;
            _nodeInfo = nodeInfo;
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _manual.CreatePage(_nodeInfo);
        }
    }
}