using System.Collections.Generic;
using UnityEngine;

public class ComplexGridElement : MonoBehaviour
{
    public List<Vector2> Shape;  // List of positions relative to the base position of the element that make up the shape of the element.

    public void PositionElement(int baseGridX, int baseGridY, float cellWidth, float cellHeight)
    {
        foreach (Vector2 relativePosition in Shape)
        {
            GameObject sprite = new GameObject(); // Create a new GameObject for each part of the shape.
            sprite.transform.parent = this.transform;  // Set the new GameObject's parent to the complex element.

            Vector3 scale = new Vector3(cellWidth, cellHeight, 1);
            sprite.transform.localScale = scale;

            // Position each part of the complex shape relative to the base position.
            Vector3 position = new Vector3((baseGridX + relativePosition.x) * cellWidth + scale.x / 2, 
                                           (baseGridY + relativePosition.y) * cellHeight + scale.y / 2, 
                                           0);
            sprite.transform.position = position;

            // Here, add your sprite renderer and set the sprite you want to use.
            SpriteRenderer spriteRenderer = sprite.AddComponent<SpriteRenderer>();
            // spriteRenderer.sprite = yourSprite;  // Set the sprite to whatever you want to use.
        }
    }
}