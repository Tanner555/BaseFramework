using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BaseFramework
{
    [CustomEditor(typeof(MyGarbageCollectionTest))]
    public class MyGarbageCollectionTestEditor : Editor
    {
        MyGarbageCollectionTest myGarbageCollectionTest => (MyGarbageCollectionTest)target;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Clear Ram"))
            {
                Debug.Log("Clearing Ram");
                Resources.UnloadUnusedAssets();
                UnityEngine.Scripting.GarbageCollector.CollectIncremental(1000000000000);
            }
        }
    }
}