using System;

namespace Project.Scripts.Core.Infrastructure.ScenesManager
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
        void Load(Enums.Scenes scene, Action onLoaded = null);
    }
}