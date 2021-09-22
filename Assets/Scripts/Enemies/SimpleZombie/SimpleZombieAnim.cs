using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleZombieAnim : MonoBehaviour
{

    private Animator anim;
    private bool chasing, attacking, dying;

    public bool Chasing { get => chasing; set => chasing = value; }
    public bool Attacking { get => attacking; set => attacking = value; }
    public bool Dying { get => dying; set => dying = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chasing)
        {
            anim.SetBool("Chasing", true);
        }
        else { anim.SetBool("Chasing", false); }

        if (attacking)
        {
            anim.SetBool("Attacking", true);
        }
        else { anim.SetBool("Attacking", false); }

        if (Dying)
        {
            anim.SetBool("Dead", true);
        }
        else { anim.SetBool("Dead", false); }
    }
}
