using System;
using UnityEngine;

public sealed class DialogueTrigger : MonoBehaviour
{
    public Dialogue _Dialogue;
    private bool _canDialogue;
    
    private void Start()
    {
        _Dialogue.WorkAround();
        print("Normal bool: " + _Dialogue._QuestionWillBeAsked + ". Static bool: " + Dialogue._StaticQuestionWillBeAsked);
    }

    private void Update()
    {
        if (_canDialogue && Input.GetKeyDown(KeyCode.T))
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(_Dialogue);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _canDialogue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _canDialogue = false;
        }
    }
    
}
