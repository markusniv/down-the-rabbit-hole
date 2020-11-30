using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public Sprite Down,
                   Up,
                   Right,
                   Left;

    public bool down,
                up,
                right,
                left;

    public CharacterMovement CharacterMovement;
    public CharacterCombat CharacterCombat;
    public Character Character;
    public Transform Hand;

    public float angle;

    // Start is called before the first frame update
    public virtual void Start()
    {
        down = true;
        Character = GetComponent<Character>();
        CharacterMovement = GetComponent<CharacterMovement>();
        CharacterCombat = GetComponent<CharacterCombat>();

    }

    protected virtual float Angle => Mathf.Round(Mathf.Atan2(CharacterMovement.Movement.y - Vector2.zero.y, CharacterMovement.Movement.x - Vector2.zero.x) * 180 / Mathf.PI / 90);

    // Update is called once per frame
    public virtual void Update()
    {
        if (CharacterMovement.CurrentState is Immobile) return;
        // just Cache
        angle = Angle;

        if (angle == -1)
        {
            down = true;
            up = false;
            left = false;
            right = false;
            GetComponent<SpriteRenderer>().sprite = Down;
        }else
        if (angle == 1)
        {
            down = false;
            up = true;
            left = false;
            right = false;
            GetComponent<SpriteRenderer>().sprite = Up;
        }else
        if (angle == 2)
        {
            down = false;
            up = false;
            left = true;
            right = false;
            GetComponent<SpriteRenderer>().sprite = Left;
        }else
        if (angle == 0)
        {
            down = false;
            up = false;
            left = false;
            right = true;
            GetComponent<SpriteRenderer>().sprite = Right;
        }
    }
}
