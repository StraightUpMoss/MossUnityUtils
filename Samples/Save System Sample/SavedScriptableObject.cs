using UnityEngine;
using MossUnityUtils.GenericUtil;


[CreateAssetMenu(menuName = "SaveSystem/SavableSampleObject",  fileName = "SampleSO")]
public class SavedScriptableObject : ScriptableObject
{
    [field: SerializeField] public SerializableGuid ID { get; set; }

    [ContextMenu("Generate GUID")]
    public void GenerateGuid()
    {
        ID = SerializableGuid.NewGuid();
    }
    //needs an ID or will fail to load when unity tries  to load
    //Need a separate class that will fetch SOs based on their ID if you want to load them
    //If you use a custom editor don't forget to dirty the data
}
