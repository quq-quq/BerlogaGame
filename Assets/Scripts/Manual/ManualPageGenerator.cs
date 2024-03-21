using TMPro;
using UnityEngine;

namespace DefaultNamespace.Manual
{
    public class ManualPageGenerator : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private Transform _prefabContainer;
        [SerializeField] private Transform _linksContainer;

        public void CreateNodePage(NodeInfo nodeInfo)
        {
            _title.text = nodeInfo.Title;
        }
    }
}