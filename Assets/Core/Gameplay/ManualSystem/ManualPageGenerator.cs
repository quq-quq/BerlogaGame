using TMPro;
using UnityEngine;

namespace DefaultNamespace.Manual
{
    public class ManualPageGenerator : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _linkNotFound;
        [SerializeField] private Transform _prefabContainer;
        [SerializeField] private Transform _linksContainer;
        [SerializeField] private NodeLink _nodeLinkPrefab;
        [SerializeField] private Manual _manual;
 
        public void CreateNodePage(NodeInfo nodeInfo)
        {
            _title.text = nodeInfo.Title;
            _mainText.text = nodeInfo.MainText;
            ClearPrefabContainer();
            ClearLinkContainer();
            var go = Instantiate(nodeInfo.Prefab, _prefabContainer);
            go.transform.localPosition = Vector3.zero;
            if (nodeInfo.Links.Count != 0)
            {
                _linkNotFound.gameObject.SetActive(false);
                foreach (var link in nodeInfo.Links)
                {
                    var r = Instantiate(_nodeLinkPrefab, _linksContainer);
                    r.Init(link, _manual);
                }
            }
            else
            {
                _linkNotFound.gameObject.SetActive(true);
            }
        }

        private void ClearPrefabContainer()
        {
            for (int i = 0; i < _prefabContainer.childCount; i++)
            {
                var child = _prefabContainer.GetChild(i);
                Destroy(child.gameObject);
            }
        }
        private void ClearLinkContainer()
        {
            for (int i = 0; i < _linksContainer.childCount; i++)
            {
                var child = _linksContainer.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}