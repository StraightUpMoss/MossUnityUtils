using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MossUnityUtils.UnlockableTree.Sample
{
    public class SampleTreeNode : TreeNode<SampleNodeData>
    {
        //Can write ui implementation here
        [ContextMenu("Check Node Complete")]
        public override bool NodeComplete()
        {
            return base.NodeComplete();
        }
    }
    [System.Serializable]
    public class SampleNodeData //Data is stored in custom class
    {
        public string name;
        public int value;
    }
    
}