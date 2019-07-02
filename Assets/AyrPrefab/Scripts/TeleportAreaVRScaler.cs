using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAreaVRScaler : MonoBehaviour
{
    [SerializeField]
    [Range(.1f, 50f)]
    private float TPAreaScale = 1;

    //private void OnEnable()
    //{
    //    try
    //    {
    //        GameObject.FindGameObjectWithTag("RightVRController").GetComponent<AyrTeleporter>().UpdateTeleportAreas();
    //    }
    //    catch
    //    {
            
    //    }
       
    //}

    public float GetTeleportAreaScale()
    {
        return TPAreaScale;
    }

    public void SetTeleportAreaScale(float scale)
    {
        TPAreaScale = scale;
    }
}
