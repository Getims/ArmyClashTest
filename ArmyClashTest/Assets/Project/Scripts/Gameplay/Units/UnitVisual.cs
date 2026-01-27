using System;
using Project.Scripts.Core.Utilities;
using UnityEngine;

namespace Project.Scripts.Gameplay.Units
{
    [Serializable]
    public class UnitVisual
    {
        [SerializeField]
        private Transform _modelContainer;

        [SerializeField]
        private MeshFilter _meshFilter;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        public void SetModelSize(float scale)
        {
            _modelContainer.SetLocalSize(scale);
            _modelContainer.localPosition = new Vector3(0, scale * 0.5f, 0);
        }

        public void SetModelSize(Vector3 scale)
        {
            _modelContainer.localScale = scale;
            _modelContainer.localPosition = new Vector3(0, scale.y * 0.5f, 0);
        }

        public void SetMesh(Mesh mesh, Vector3 scale)
        {
            _meshFilter.mesh = mesh;
            _meshFilter.transform.localScale = scale;
        }

        public void SetColor(Color color)
        {
            _meshRenderer.material.color = color;
        }
    }
}