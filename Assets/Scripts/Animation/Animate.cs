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

    // Start is called before the first frame update
    public virtual void Start()
    {
        down = true;
        Character = GetComponent<Character>();
        CharacterMovement = GetComponent<CharacterMovement>();
        CharacterCombat = GetComponent<CharacterCombat>();

    }
    // Update is called once per frame
    public virtual void Update()
    {
        if (CharacterMovement.CurrentState is Immobile) return;

        Character.Movement.LookDirection = Character.Movement.Movement.To4WayDirection();

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
