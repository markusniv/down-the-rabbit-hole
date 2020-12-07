using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles changing character sprites depending on the movement direction and saves the directions into booleans to use while attacking
/// </summary>
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

    public virtual void Start()
    {
        down = true;
        Character = GetComponent<Character>();
        CharacterMovement = GetComponent<CharacterMovement>();
        CharacterCombat = GetComponent<CharacterCombat>();

    }

    protected virtual Vector2 LookDirection => Character.Movement.Movement.To4WayDirection();

    public virtual void Update()
    {
        if (CharacterMovement.CurrentState is Immobile) return;

        Character.Movement.LookDirection = LookDirection;

        if (Character.Movement.LookDirection == Vector2.down)
        {
            down = true;
            up = false;
            left = false;
            right = false;
            GetComponent<SpriteRenderer>().sprite = Down;
        }else
        if (Character.Movement.LookDirection == Vector2.up)
        {
            down = false;
            up = true;
            left = false;
            right = false;
            GetComponent<SpriteRenderer>().sprite = Up;
        }else
        if (Character.Movement.LookDirection == Vector2.left)
        {
            down = false;
            up = false;
            left = true;
            right = false;
            GetComponent<SpriteRenderer>().sprite = Left;
        }else
        if (Character.Movement.LookDirection == Vector2.right)
        {
            down = false;
            up = false;
            left = false;
            right = true;
            GetComponent<SpriteRenderer>().sprite = Right;
        }
    }
}
