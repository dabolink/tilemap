using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NeighbourManager))]
public class DynamicSpriteUpdater : MonoBehaviour, IUpdateSprite
{
    SpriteRenderer spriteRenderer;
    public Sprite Sprite
    {
        protected get { return spriteRenderer.sprite; }
        set
        {
            spriteRenderer.sprite = value;
        }
    }

    public string spriteNamePrefix;
    public string tileMapName;

    NeighbourManager neighbourManager;
    public void UpdateSprite()
    {
        String spriteName = spriteNamePrefix + "_" + GetDirectionString();

        Sprite sprite = Resources.LoadAll<Sprite>(tileMapName).Single(s => s.name == spriteName);
        Sprite = sprite;
    }

    private string GetDirectionString()
    {
       return neighbourManager.GetDirectionString();
    }

    // Start is called before the first frame update
    void Start()
    {
        neighbourManager = GetComponent<NeighbourManager>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
