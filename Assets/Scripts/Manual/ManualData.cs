using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Manual
{
    [CreateAssetMenu]
    public class ManualData : ScriptableObject
    {
        [SerializeField] private List<NodeInfo> _triggers;
        [SerializeField] private List<NodeInfo> _objectNodes;
        [SerializeField] private List<NodeInfo> _actionNodes;
        [SerializeField] private List<ConnectorInfo> _connectors;

    }
}