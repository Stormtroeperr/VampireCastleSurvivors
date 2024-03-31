using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IPlayerJoinListener
    {
        void OnPlayerJoined(PlayerInput player);
    }
}
