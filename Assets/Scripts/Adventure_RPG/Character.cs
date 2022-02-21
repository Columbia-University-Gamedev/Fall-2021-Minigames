namespace CharacterStruct
{
    using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Character
{
    public Sprite characterImage;
    public string characterName;

    public string GetName()
    {
        return characterName;
    }

    public Sprite GetImage()
    {
        return characterImage;
    }
}
}



