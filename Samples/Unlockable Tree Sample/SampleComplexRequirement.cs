using System;

namespace MossUnityUtils.UnlockableTree.Sample
{
    [System.Serializable]
    public class SampleComplexRequirement : ComplexNodeRequirement<SampleComplexNodeReq>
    {
        public override void UpdateNode(SampleComplexNodeReq value)
        {
            currentState.Item = value.Item;
            currentState.Quantity += value.Quantity;
        }
        
    }
    [System.Serializable]
    public class SampleComplexNodeReq : IEquatable<SampleComplexNodeReq>
    {
        public string Item;
        public int Quantity;
        

        public bool Equals(SampleComplexNodeReq other)
        {
            if (other == null) return false;
            return Item == other.Item && other.Quantity >= Quantity;
        }
    }
}