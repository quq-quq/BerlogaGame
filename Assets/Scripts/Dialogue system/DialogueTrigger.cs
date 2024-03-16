using Dialogue_system;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueViewer[] _dialogueCanvas;
    private Animator _anim;
    private bool _canPressing;
    private int _indexDialogueCanvas;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    if (_canPressing && Input.GetKeyDown(KeyCode.E))
    //    {
    //        _dialogueCanvas[_indexDialogueCanvas].StartDialogue();
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        _anim.SetBool("IsPressing", true);
    //        _canPressing = true;

    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && !_canPressing)
        {
            _canPressing = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            _anim.SetBool("IsPressing", true);
            if (_canPressing)
            {
                _dialogueCanvas[_indexDialogueCanvas].StartDialogue();
                _canPressing = false;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            _anim.SetBool("IsPressing", false);
            _canPressing = false;
        }
    }

    public void ChangeIndex()
    {
        if(_indexDialogueCanvas+1!= _dialogueCanvas.Length)
            _indexDialogueCanvas += 1;
    }
}
