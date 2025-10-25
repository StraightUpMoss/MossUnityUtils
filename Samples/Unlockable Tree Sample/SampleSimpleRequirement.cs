using UnityEngine;

namespace MossUnityUtils.UnlockableTree.Sample
{
    public class SampleSimpleRequirement : NodeRequirement<int>
    {
        [SerializeField] private int _valueThreshold;

        public override void UpdateNode(int value)
        {
            Complete = value >= _valueThreshold;
        }
    }
}