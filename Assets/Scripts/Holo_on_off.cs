using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float minDistance = 10;

    public bool Active = false;
    protected Animator[] children;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        children = GetComponentsInChildren<Animator>();
        for (int i = 0; i < children.Length; i++){
                children[i].SetBool("Active", Active);
            }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = Camera.main.transform.position - transform.position;
        if (delta.magnitude < minDistance){
            if (Active) return;
            Active = true;
            for (int i = 0; i < children.Length; i++){
                children[i].SetBool("Active", true);
            }
        } else {
            if (!Active) return;
            Active = false;
            for (int i = 0; i < children.Length; i++){
                children[i].SetBool("Active", false);
            }
        }
    }}
