using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardInputs : MonoBehaviour
{
    const float pressDownTime = .2f;

    float spaceTrueUntil = -1f;
    float tabKeyTrueUntil = -1f;
    float qKeyTrueUntil = -1f;
    float fKeyTrueUntil = -1f;
    float eKeyTrueUntil = -1f;
    float rKeyTrueUntil = -1f;
    float escKeyTrueUntil = -1f;

    private void Update()
    {
        ReadInput();
    }

    public void ReadInput()
    {
        //Press input keys
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceTrueUntil = Time.time + pressDownTime;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabKeyTrueUntil = Time.time + pressDownTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            qKeyTrueUntil = Time.time + pressDownTime;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            fKeyTrueUntil = Time.time + pressDownTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            eKeyTrueUntil = Time.time + pressDownTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rKeyTrueUntil = Time.time + pressDownTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escKeyTrueUntil = Time.time + pressDownTime;
        }


        //Held input keys
        forwardBackMovement = Input.GetAxis("Vertical");
        leftRightMovement = Input.GetAxis("Horizontal");
        shiftKey = Input.GetKey(KeyCode.LeftShift);
     
    }

    private void FixedUpdate()
    {
        if (Time.time > spaceTrueUntil)
        {
            spaceTrueUntil = -1f;
        }

        if (Time.time > qKeyTrueUntil)
        {
            qKeyTrueUntil = -1f;
        }
    }

    public bool SpacePressed()
    { 
        if(spaceTrueUntil == -1)
        {
            return false;
        }
        else
        {
            spaceTrueUntil = -1;
            return true;
        }
    }

    public bool TabKeyPressed()
    {
        if (tabKeyTrueUntil == -1)
        {
            return false;
        }
        else
        {
            tabKeyTrueUntil = -1;
            return true;
        }
    }

    public bool QKeyPressed()
    {
        if (qKeyTrueUntil == -1)
        {
            return false;
        }
        else
        {
            qKeyTrueUntil = -1;
            return true;
        }
    }

    public bool FKeyPressed()
    {
        if (fKeyTrueUntil == -1)
        {
            return false;
        }
        else
        {
            fKeyTrueUntil = -1;
            return true;
        }
    }

    public bool EKeyPressed()
    {
        if (eKeyTrueUntil == -1)
        {
            return false;
        }
        else
        {
            eKeyTrueUntil = -1;
            return true;
        }
    }

    public bool RKeyPressed()
    {
        if (rKeyTrueUntil == -1)
        {
            return false;
        }
        else
        {
            rKeyTrueUntil = -1;
            return true;
        }
    }

    public bool EscKeyPressed()
    {
        if (escKeyTrueUntil == -1)
        {
            return false;
        }
        else
        {
            escKeyTrueUntil = -1;
            return true;
        }
    }


    public float leftRightMovement { get; private set; }
    public float forwardBackMovement { get; private set; }
    public bool shiftKey { get; private set; }
}

