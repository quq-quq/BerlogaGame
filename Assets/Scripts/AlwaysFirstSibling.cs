using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class AlwaysFirSibling : MonoBehaviour
    {
        private void Update()
        {
            transform.SetAsFirstSibling();
        }
    }
}