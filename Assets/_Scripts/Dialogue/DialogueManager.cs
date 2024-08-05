using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public sealed class DialogueManager : MonoBehaviour
{
    public Volume _vol;
    public Animator _DialogueAnimator;
    public TextMeshProUGUI _CharacterName;
    public TextMeshProUGUI _DialogueText;
    public TextMeshProUGUI _ProceduralText;
    public Button _ProceduralButton;
    public Image _Sprite;
    public float _TypingSpeed = 0.05f;
    public float _WaitForContinueButton = 0.05f;
    
    private Queue<string> _lines;
    private bool _isDialogueActive;
    private static readonly int _isOpen = Animator.StringToHash("IsOpen");

    private void Awake()
    {
        _vol.enabled = false;
    }

    private void Start()
    {
        _lines = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _vol.enabled = true;
        _DialogueAnimator.SetBool(_isOpen, true);
        _CharacterName.text = dialogue._Name;
        _Sprite.sprite = dialogue._Sprite.sprite;
        _ProceduralText.text = "Continue >>";
        _ProceduralButton.enabled = true;
        _isDialogueActive = true;
        _lines.Clear();

        foreach (string sentence in dialogue._Lines)
        {
            _lines.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string lines = _lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(lines));
    }

    IEnumerator TypeSentence(string sentence)
    {
        ContinueButton(false, "");
        _DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _DialogueText.text += letter;
            yield return new WaitForSecondsRealtime(_TypingSpeed);
        }
        //yield return new WaitForSecondsRealtime(_WaitForContinueButton);
        if (_lines.Count == 0)
        {
            ContinueButton(true, "End Dialogue >>");
            yield break;
        }
        ContinueButton(true, "Continue >>");
    }

    private void FunctionToCallQuestionsWillBeRefactored()
    {
        if (_lines.Count == 0 && Dialogue._StaticQuestionWillBeAsked)
        {
            /*
             * CALL A QUESTION BLOCK
             * switch (answer) { case: ENUM }
             */
            ContinueButton(false, "");
        }
    }

    private void ContinueButton(bool status, string message)
    {
        _ProceduralButton.enabled = status;
        _ProceduralText.text = message;
    }
    
    private void EndDialogue()
    {
        _isDialogueActive = false;
        _DialogueAnimator.SetBool(_isOpen, false);
        _vol.enabled = false;
    }
}

