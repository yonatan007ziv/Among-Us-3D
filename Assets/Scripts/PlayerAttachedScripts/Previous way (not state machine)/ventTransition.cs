using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ventTransition : MonoBehaviour
{
    public bool fadeOut;
    public bool finished;
    
    void Start()
    {
        GetComponent<Animator>().SetBool("fadeOut", fadeOut);
    }

    public void setFinished()
    {
        finished = true;
    }
}