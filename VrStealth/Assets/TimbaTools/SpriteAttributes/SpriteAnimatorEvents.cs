using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorEvents : MonoBehaviour
{
    Dictionary<int, SpriteAttributes> spriteAtributtes;

    private void Awake()
    {
        spriteAtributtes = new Dictionary<int, SpriteAttributes>();
    }

    public void RegisterSprite(SpriteAttributes spriteA)
    {
        spriteAtributtes.Add(spriteA.spriteID, spriteA);
    }

    public void SetLayer(AnimationEvent data)
    {
        spriteAtributtes[(int)data.floatParameter].SetSortingLayer(data.stringParameter);
    }

    public void SetOrder(AnimationEvent data)
    {
        spriteAtributtes[(int)data.floatParameter].SetSortingOrder(data.intParameter);
    }

    public void SetOrderAndLayer(AnimationEvent data)
    {
        spriteAtributtes[(int)data.floatParameter].SetSortingLayer(data.stringParameter);
        spriteAtributtes[(int)data.floatParameter].SetSortingOrder(data.intParameter);
    }
}