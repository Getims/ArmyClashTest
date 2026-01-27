using UnityEngine;

namespace Project.Scripts.Gameplay.Units
{
    public static class SpawnZoneHelper
    {
        public static Vector3 GetRandomPointInZone(Transform zoneTransform)
        {
            Vector3 center = zoneTransform.position;
            Vector3 size = zoneTransform.localScale;

            float randomX = Random.Range(-size.x * 0.5f, size.x * 0.5f);
            float randomZ = Random.Range(-size.z * 0.5f, size.z * 0.5f);

            return new Vector3(center.x + randomX, 0f, center.z + randomZ);
        }
    }
}