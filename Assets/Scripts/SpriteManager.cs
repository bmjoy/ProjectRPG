using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SpriteManager
{
    private static List<Sprite> sprites;

    public static Sprite GetSprite(string spriteName)
    {
        if(sprites == null)
            LoadSprites();

        var sprite = sprites.First(sp => sp.name == spriteName);
        if (sprite != null)
            return sprite;
        else
        {
            Debug.LogError($"Couldnt find sprite {spriteName}");
            return null;
        }
    }

    private static void LoadSprites()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites").ToList();
    }
}

