using MossUnityUtils.GenericUtil;
using UnityEngine;

public class SampleSavedClass : MonoBehaviour, IBind<SampleSaveData>
{
    public SerializableGuid ID { get; set; }
    private string _objData; //other data; 
    public void Bind(SampleSaveData data)
    {
        ID = data.ID;
        _objData = data.Data;
    }
}
