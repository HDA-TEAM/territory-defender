public class TowerStats : Stats
{
    protected virtual float StatCalculatePerLevel(StatId statId, int level, float value)
    {
        return value;
    }
}
