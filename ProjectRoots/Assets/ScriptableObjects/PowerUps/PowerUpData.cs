using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Economy/PowerUpData")]
public class PowerUpData : ScriptableObject
{
    ///Unique id
    public string id;
    
    ///A brief description of a power up (not needed now)
    public string description;

    public Sprite sprite;

    public PowerUpsType type;

    ///Optional parameter, there might be many more in future (Editor Scripting Required)
    public float incrementValue; //To show (""USE"") only when type == UPGRADE_DEFENSES_ATTACK


    private void OnValidate()
    {
#if UNITY_EDITOR
        if (id == "")
        {
            id = GUID.Generate().ToString();
            EditorUtility.SetDirty(this);
        }
#endif
    }

}
