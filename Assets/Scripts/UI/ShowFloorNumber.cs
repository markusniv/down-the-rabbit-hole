using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFloorNumber : MonoBehaviour
{
    string Format = "Floor {0}";

    Text Text;
    Animator Animator;
    private void Awake()
    {
        Text = GetComponent<Text>();
        Animator = GetComponent<Animator>();
        GameController.Instance.CurrentFloor.OnFloorCreated += Show;
    }

    private void Start()
    {
        
    }

    void Show(int floor)
    {
        Text.text = string.Format(Format, floor);
        Animator.SetTrigger("Show");
    }
}
