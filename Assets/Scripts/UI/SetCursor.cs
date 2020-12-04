using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used for setting the cursor graphics to the specific sprites
/// </summary>
public class SetCursor : MonoBehaviour
{
    public Texture2D mouseSprite, crosshairSprite;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorOffset = new Vector2(mouseSprite.width / 2, mouseSprite.height / 2);
        Cursor.SetCursor(mouseSprite, cursorOffset, CursorMode.Auto);
    }
}
