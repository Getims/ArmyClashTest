using UnityEngine;

namespace Project.Scripts.Core.Infrastructure.Assets
{
    public interface IAssetProvider
    {
        T Load<T>(string path) where T : Object;
    }
}