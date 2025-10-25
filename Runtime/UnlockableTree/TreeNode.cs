using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace MossUnityUtils.UnlockableTree
{
    public class TreeNode<TData> : MonoBehaviour
    {
        [SerializeField] protected TreeNode<TData>[] nodePreReqs;
        //add in a list of requirements of each type that you need for more specific nodes
        [field:SerializeField] public bool DataHidden { get; private set; } //helpful for masking data when displayed
        [field:SerializeField] public TData NodeData { get; private set; }
        [SerializeField] protected bool complete;

       

        public virtual bool NodeComplete()
        {
            if (complete) return true;
            foreach (TreeNode<TData> node in nodePreReqs)
            {
                if (node.NodeComplete() == false) return false;
            }
            complete = true;
            return true;
        }
    }
}

