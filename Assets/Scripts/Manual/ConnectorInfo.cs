using System;
using UnityEngine;

namespace DefaultNamespace.Manual
{
    [Serializable]
    public class ConnectorInfo
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _title;
        [SerializeField] private string _mainText;
    }
}