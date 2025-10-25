using MossUnityUtils.GenericUtil;
using UnityEngine;


public class SampleSaveData : ISaveAble
{
    public SerializableGuid ID { get; set; }
    public string Data; //or whatever other data you want to save
}
