using System;
using Project.Scripts.Core.Infrastructure.Data;
using Project.Scripts.DebugModule.Core;

namespace Project.Scripts.DebugModule.Cheats
{
    public class ResetProgressionCheat : ICheat
    {
        private static IDatabase _database;
        private event Action OnNeedSceneRestart;

        public CheatGroupType GroupType => CheatGroupType.Base;

        public string Name => "Reset progression";

        public ResetProgressionCheat(IDatabase database)
        {
            _database = database;
        }

        public void Execute()
        {
            _database.DeleteData();
            _database.SaveData();
            _database.ReloadData();

            OnNeedSceneRestart?.Invoke();
        }

        public void Initialize(Action restartScene)
        {
            OnNeedSceneRestart = restartScene;
        }
    }
}