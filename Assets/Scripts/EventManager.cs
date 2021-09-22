using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    [SerializeField] UnityEvent damage;

    public void CallDamage()
    {
        damage.Invoke();
    }

}
