using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
    public partial class MoveVRHeadNetworkObject
    {
        protected override bool AllowOwnershipChange(NetworkingPlayer newOwner)
        {
            return true;
            // The newOwner is the NetworkingPlayer that is requesting the ownership change, you can get the current owner with just "Owner"
        }
    }
}