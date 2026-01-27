using UnityEngine;

namespace Project.Scripts.Gameplay.Units
{
    public class SpawnZoneGizmo : MonoBehaviour
    {
        [SerializeField]
        private Color _gizmoColor = new Color(0f, 1f, 0f, 0.25f);

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}