using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Scriptable Objects/Integer Value")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    public int currentValue;
    public int maxValue;
    public int defaultMaxValue;

    //When it is unloaded from memory (even during play)
    //Triggered when play is stopped or if the new scene does not reference this script
    public void OnAfterDeserialize()
    {
        //Reset the max to the default on load
        maxValue = defaultMaxValue;

        //Also on load, set the current to the max (start at full health)
        currentValue = maxValue;

    }


    public void OnBeforeSerialize()
    {

    }
}