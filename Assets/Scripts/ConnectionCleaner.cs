using UI;
using UnityEngine;
namespace DefaultNamespace
{
    public class ConnectionCleaner : MonoBehaviour
    {
        public void ClearControllableConnections()
        {
            var conections = FindObjectsOfType<ControllableConnector>();
            foreach (var connector in conections)
            {
                connector.ClearConnections();
            }
        }
    }
}