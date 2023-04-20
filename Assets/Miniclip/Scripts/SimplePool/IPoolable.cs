using UnityEngine;

namespace SimplePool
{
    public interface IPoolable
    {
        /// <summary>
        /// This method is called when the object is instantiated for the first time and when it is returned to the pool it belongs.
        /// </summary>
        void ResetInstance();
        /// <summary>
        /// Returns this instance to the pool it belongs to.
        /// </summary>
        void ReturnToPool();
        /// <summary>
        /// Sets the given <see cref="Transform"/> as the parent the instance will be assigned to when returned to the <see cref="IPool"/> it belongs to.
        /// </summary>
        /// <param name="parent">The <see cref="Transform"/> to set as original parent.</param>
        void SetOriginalParent(Transform parent);
        /// <summary>
        /// Called by the <see cref="IPool"/> when the object is instantiated.
        /// </summary>
        /// <param name="pool">The <see cref="IPool"/> the object will belong to.</param>
        void SetPool(IPool pool);
    }
}
