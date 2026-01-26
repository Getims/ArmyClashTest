using UnityEngine;

namespace Project.Scripts.Core.Infrastructure.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public T Load<T>(string path) where T : Object =>
            Resources.Load<T>(path);
    }
}