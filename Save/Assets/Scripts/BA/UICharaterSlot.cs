using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaterSlot : MonoBehaviour
{
    public Button button;
    public Image image;
    public CharacterReal Character { get; private set; }

    public void SetCharacter (CharacterReal character)
    {
        Character = character;
        image.sprite = Character.GetSprite;
    }

}
