using UnityEngine;
using System;

[Serializable]
public class Dialogues
{
    public string title;
    [TextArea(3, 10)]
    public string description;
    public Sprite ava;
}
