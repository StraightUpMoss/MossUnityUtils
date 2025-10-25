using System.Linq;
using UnityEngine;

namespace MossUnityUtils.UnlockableTree.Sample
{
    public class ComplexTreeNode : TreeNode<SampleNodeData>
    {
        [SerializeField] private SampleComplexRequirement[] dataPreReqs;
        
        [ContextMenu("Check Node Complete")]

        public override bool NodeComplete()
        {
            if (complete) return true;
            for (int i = 0; i < dataPreReqs.Length; i++)
            {
                if (!dataPreReqs[i].Complete) return false;
            }
            if (nodePreReqs.Any(node => node.NodeComplete() == false))
            {
                return false;
            }
            complete = true;
            return true;
        }
    }
}