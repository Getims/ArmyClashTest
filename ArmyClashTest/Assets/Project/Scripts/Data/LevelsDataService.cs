using Project.Scripts.Core.Infrastructure.Data;
using Project.Scripts.Core.Infrastructure.Data.Values;

namespace Project.Scripts.Data
{
    public interface ILevelsDataService
    {
        public DataValue<LevelsData, int> CurrentLevel { get; }
        public int CurrentLevelUI { get; }
    }

    public class LevelsDataService : ADataService<LevelsData>, ILevelsDataService
    {
        public DataValue<LevelsData, int> CurrentLevel { get; private set; }

        public int CurrentLevelUI => CurrentLevel.Value + 1;

        public LevelsDataService(IDatabase database) : base(database)
        {
            CurrentLevel = CreateValue(
                data => data.CurrentLevel,
                (data, value) => data.CurrentLevel = value);
        }
    }
}