using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAttributes : MonoBehaviour
{
    [SerializeField]
    SpriteAnimatorEvents animatorEvents;

    public int spriteID;

    private void Start()
    {
        animatorEvents.RegisterSprite(this);
    }

    public void SetSortingLayer(string layer)
    {
        GetComponent<SpriteRenderer>().sortingLayerName = layer;
    }

    public void SetSortingOrder(int order)
    {
        GetComponent<SpriteRenderer>().sortingOrder = order;
    }
}
