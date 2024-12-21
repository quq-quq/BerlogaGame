using System.Collections;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class WaitAction : ActionNodeParameter
    {
        public override void Do(ObjectForNode go)
        {
            Camera.main.GetComponent<MonoBehaviour>().StartCoroutine(DoCoroutine());;
            IEnumerator DoCoroutine()
            {
                yield return new WaitForSeconds(Value);
                base.Do(go);
            }
        }

        protected override void DoAction(ObjectForNode go)
        {
            
        }
        
        public override bool CanExecute(ObjectNode node)
        {
            return true;
        }
    }
}
