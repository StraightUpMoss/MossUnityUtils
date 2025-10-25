using UnityEngine;

namespace MossUnityUtils.UnlockableTree
{
    [System.Serializable]
    public abstract class NodeRequirement<T> //Simple node
    {
        [field:SerializeField] public bool Complete { get; protected set;}


        public abstract void UpdateNode(T value);
    }
}