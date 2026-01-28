using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Gameplay.Factory
{
    public static class SpawnZoneHelper
    {
        public static List<Vector3> DistributeSquares(Vector3 center, Vector3 size, int count)
        {
            List<Vector3> positions = new List<Vector3>();
            float rectWidth = size.x;
            float rectHeight = size.z;
            float rectArea = rectWidth * rectHeight;
            float squareSide = Mathf.Sqrt(rectArea / count);

            int cols = Mathf.FloorToInt(rectWidth / squareSide);
            int rows = Mathf.FloorToInt(rectHeight / squareSide);

            float startX = center.x - rectWidth / 2 + squareSide / 2;
            float startZ = center.z - rectHeight / 2 + squareSide / 2;

            int placed = 0;
            for (int i = 0; i < rows && placed < count; i++)
            {
                for (int j = 0; j < cols && placed < count; j++)
                {
                    float x = startX + j * squareSide;
                    float z = startZ + i * squareSide;
                    positions.Add(new Vector3(x, center.y, z));
                    placed++;
                }
            }

            return positions;
        }
    }
}