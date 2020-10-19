using UnityEngine;

[System.Serializable]
public class Dialogues
{
    public string name;
    public Sprite currentAva;
    [TextArea(3,10)]
    public string[] sentences;
}
