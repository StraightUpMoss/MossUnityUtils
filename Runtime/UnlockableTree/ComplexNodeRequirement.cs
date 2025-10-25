using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MossUnityUtils.UnlockableTree
{
    [System.Serializable]
    public class ComplexNodeRequirement<T> : NodeRequirement<T> where T : IEquatable<T>
    {
        [SerializeField] protected T completeState;
        [SerializeField] protected T currentState;

        public override void UpdateNode(T value)
        {
            currentState = value; 
        }

        public new bool Complete => currentState.Equals(completeState);
    }
}