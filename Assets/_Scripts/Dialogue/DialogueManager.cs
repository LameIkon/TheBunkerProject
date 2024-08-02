using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class DialogueManager : MonoBehaviour
{
    public Animator _Animator;
    public TextMeshProUGUI _CharacterName;
    public TextMeshProUGUI _DialogueText;
    public TextMeshProUGUI _ProceduralText;
    public Button _ProceduralButton;
    public Image _Sprite;
    public float _TypingSpeed = 0.05f;
    
    private Queue<string> _Lines;
    private bool _IsDialogueActive;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    private void Start()
    {
        _Lines = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _Animator.SetBool(IsOpen, true);
        _CharacterName.text = dialogue._Name;
        _Sprite.sprite = dialogue._Sprite.sprite;
        _ProceduralText.text = "Continue >>";
        _ProceduralButton.enabled = true;
        _IsDialogueActive = true;
        _Lines.Clear();

        foreach (string sentence in dialogue._Lines)
        {
            _Lines.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_Lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        if (_Lines.Count == 1 && Dialogue._StaticQuestionWillBeAsked)
        {
            // Call a question block
            
            /*
             * {
             *   switch (answer)
             *   {
             *      case
             *   }
             * }
             *
             *
             * 
             */
            
            _ProceduralButton.enabled = false;
            _ProceduralText.text = "";
        }

        if (_Lines.Count == 1 && !Dialogue._StaticQuestionWillBeAsked)
        {
            _ProceduralText.text = "End Dialogue >>";
        }
        
        string lines = _Lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(lines));
    }

    IEnumerator TypeSentence(string sentence)
    {
        _DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _DialogueText.text += letter;
            yield return new WaitForSecondsRealtime(_TypingSpeed);
        }
    }

    private void EndDialogue()
    {
        _IsDialogueActive = false;
        _Animator.SetBool(IsOpen, false);
    }
}
