using UnityEngine;

namespace DefaultNamespace.Manual
{
    public class Manual : MonoBehaviour
    {
        [SerializeField] private ManualData _data;
        [SerializeField] private ManualPageGenerator _pageGenerator;
        [SerializeField] private NodeLink _prefab;
        [SerializeField] private Transform _triggerContainer;
        [SerializeField] private Transform _objectContainer;
        [SerializeField] private Transform _actionContainer;

        private void Start()
        {
            _pageGenerator.gameObject.SetActive(false);
            foreach (var trigger in _data.Triggers)
            {
                var go = Instantiate(_prefab, _triggerContainer);
                go.Init(trigger, this);
            }
            foreach (var trigger in _data.ObjectNodes)
            {
                var go = Instantiate(_prefab, _objectContainer);
                go.Init(trigger, this);
            }
            foreach (var trigger in _data.ActionNodes)
            {
                var go = Instantiate(_prefab, _actionContainer);
                go.Init(trigger, this);
            }
        }

        public void CreatePage(NodeInfo nodeInfo)
        {
            _pageGenerator.gameObject.SetActive(true);
            _pageGenerator.CreateNodePage(nodeInfo);
        }
    }
}