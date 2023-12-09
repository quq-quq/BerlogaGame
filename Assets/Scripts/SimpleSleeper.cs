using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InterfaceNode;
using UnityEngine;

public class SimpleSleeper : ISleeper
    {
        private List<object> _callers = new List<object>();
        private Action _switcher;
        private MonoBehaviour _context;
        
        public SimpleSleeper(Action switcher, MonoBehaviour context)
        {
            _switcher = switcher;
            _context = context;
        }
            
        public void Sleep(object caller)
        {
            if(_callers.All(i => i != caller))
            {
                _callers.Add(caller);
                _switcher.Invoke();
            }
        }

        public void Sleep(float t, object caller)
        {
            _context.StartCoroutine(SleepCoroutine());
            
            IEnumerator SleepCoroutine()
            {
                Sleep(caller);
                yield return new WaitForSeconds(t);
                WakeUp(caller);
            }
        }

        public void WakeUp(object caller)
        {
            if(_callers.Remove(caller) && _callers.Count == 0)
                _switcher.Invoke();
        }
    }
