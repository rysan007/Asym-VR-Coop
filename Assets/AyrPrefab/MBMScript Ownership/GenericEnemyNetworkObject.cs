
namespace BeardedManStudios.Forge.Networking.Generated
{
    public partial class GenericEnemyNetworkObject
    {
        protected override bool AllowOwnershipChange(NetworkingPlayer newOwner)
        {
            // The newOwner is the NetworkingPlayer that is requesting the ownership change, you can get the current owner with just "Owner"

            return true;
        }
    }
}