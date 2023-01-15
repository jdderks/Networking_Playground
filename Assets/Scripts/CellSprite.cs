using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(menuName = "Scriptable Objects/CellSpriteList", fileName = "New SpriteList")]
public class CellSprite : ScriptableObject
{
    public Sprite noneSprite;
    public Sprite grassSprite;
    public Sprite wallSprite;
}