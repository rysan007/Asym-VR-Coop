using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractables
{
    void UseObject(Transform interacter);
    void UnuseObject(Transform interacter);
    void ForceUnuseObject(Transform interacter);
    Transform GetTransform();
}
