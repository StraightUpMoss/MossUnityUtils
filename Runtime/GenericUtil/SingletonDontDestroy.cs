namespace MossUnityUtils.GenericUtil
{
    public class SingletonDontDestroy<T>: Singleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}