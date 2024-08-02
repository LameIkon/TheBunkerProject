using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public sealed class Dialogue
{
    public Image _Sprite;
    public string _Name;
    
    public bool _QuestionWillBeAsked;
    public static bool _StaticQuestionWillBeAsked;
    
    [TextArea(3, 10)] 
    public string[] _Lines;

    public void WorkAround()
    {
        _StaticQuestionWillBeAsked = _QuestionWillBeAsked;
    }
}
