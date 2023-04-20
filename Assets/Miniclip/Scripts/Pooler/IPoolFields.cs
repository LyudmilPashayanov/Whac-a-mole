namespace Miniclip.Pooler
{
    /// <summary>
    /// Marks a prefab controller as one to be populated with IPoolData. Used in the Scroll View Pooling technique.
    /// </summary>
    public interface IPoolFields
    {
        /// <summary>
        /// Use this to populate with the IPoolData.
        /// </summary>
        /// <param name="objectToUpdate"></param>
        public void UpdateField(IPoolData objectToUpdate);
    }
}