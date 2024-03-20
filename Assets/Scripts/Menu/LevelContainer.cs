using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class LevelContainer : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private TMP_Text _text;

        public Transform Container => _container;

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

    }   
}