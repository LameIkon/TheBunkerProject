using System;
using UnityEngine;

public sealed class DialogueTrigger : MonoBehaviour
{
    public Dialogue _Dialogue;

    private void Start()
    {
        _Dialogue.WorkAround();
        print("Normal bool: " + _Dialogue._QuestionWillBeAsked + ". Static bool: " + Dialogue._StaticQuestionWillBeAsked);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(_Dialogue);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TriggerDialogue();
            }
        }
    }
}
