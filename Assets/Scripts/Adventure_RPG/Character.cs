using System.Collections.Generic;

namespace CharacterStruct
{
    using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
public class Character
{
    [SerializeField] private Sprite characterImage;
    [SerializeField] private string characterName;

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



