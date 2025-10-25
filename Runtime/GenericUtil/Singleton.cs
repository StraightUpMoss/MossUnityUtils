namespace MossUnityUtils.GenericUtil
{
    using UnityEngine;

    public class Singleton<T> : MonoBehaviour
    {
        public static Singleton<T> Instance { get; private set; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError($"Duplicate Singleton of type {typeof(T)}. Destroying Object");
                Destroy(gameObject);
            }
        }
    }
}
