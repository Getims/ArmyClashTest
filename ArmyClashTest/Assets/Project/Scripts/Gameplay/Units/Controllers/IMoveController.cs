using UnityEngine;

namespace Project.Scripts.Gameplay.Units.Controllers
{
    public interface IMoveController
    {
        Transform Transform { get; }
        void Initialize(UnitInfo unitInfo, float speedPointValue);
        void MoveTowards(float deltaTime);
        void StopMoving();
    }
}