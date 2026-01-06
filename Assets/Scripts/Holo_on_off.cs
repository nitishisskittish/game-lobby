using UnityEngine;
using System.Collections;

public class Holo_on_off : MonoBehaviour
{
    public bool Active = false;
    protected Animator[] children;

    void Awake()
    {
        children = GetComponentsInChildren<Animator>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetBool("Active", Active);
        }
    }

    public void On()
    {
        Active = true;
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetBool("Active", true);
        }
    }

    public void Off()
    {
        Active = false;
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetBool("Active", false);
        }
    }
}