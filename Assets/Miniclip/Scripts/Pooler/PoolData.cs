namespace Miniclip.Pooler
{
    /// <summary>
    /// Base class to mark a data class so it can be populated in IPoolField. Used in the Scroll View Pooling.
    /// </summary>
    public abstract class PoolData
    {
    }
    
    /// <summary>
    /// This class serves to populate the high score panel with relevant UI data.
    /// </summary>
    public class AttemptDataUI : PoolData
    {
        public int Score { get; set; }
        public string Name { get; set; }
        
        public int Placement { get; set; }
        public bool Highlighted { get; set; }

        public AttemptDataUI(int score, string name, int placement,bool highlighted)
        {
            Score = score;
            Name = name;
            Placement = placement;
            Highlighted = highlighted;
        }
    }
}