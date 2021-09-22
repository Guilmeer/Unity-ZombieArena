using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public static PlayerAnim instance;
    private Animator anim;

    private bool aiming, walking, running, dead;

    public bool Aiming { get => aiming; set => aiming = value; }
    public bool Running { get => running; set => running = value; }
    public bool Dead { get => dead; set => dead = value; }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Aiming animator bool
        if (aiming)
        {
            anim.SetBool("Aiming", true);
            transform.GetComponent<PlayerMovements>().SetSpeedAmount(.25f);
        }
        else
        {
            anim.SetBool("Aiming", false);
            transform.GetComponent<PlayerMovements>().SetSpeedAmount(1f);
        }

        // Running animator bool
        if (Running)
        {
            anim.SetBool("Running", true);
        }
        else { anim.SetBool("Running", false); }

        // Dying
        if (Dead)
        {
            anim.SetBool("Dead", true);
        }
        else { anim.SetBool("Dead", false); }
    }
}
