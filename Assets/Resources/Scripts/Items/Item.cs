using System;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeReference] private Skill skill;

    [SerializeField] private Sprite itemSprite;

    public Skill Skill => skill;
    public Sprite ItemSprite => itemSprite;
}
