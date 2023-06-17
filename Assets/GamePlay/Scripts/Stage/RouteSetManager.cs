using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class RouteSetManager : MonoBehaviour
{
    // [SerializeField] private List<LineRenderer> currentRouteLineRender = new List<LineRenderer>();
    // [SerializeField] private List<RouteSetConfig> routeSetConfigs;
    //
    // private RouteSetConfig FindRouteSetConfig(int stageId)
    // {
    //     foreach (var routeSetConfig in routeSetConfigs)
    //     {
    //         if (routeSetConfig.stageId == stageId)
    //         {
    //             return routeSetConfig;
    //         }
    //     }
    //     return null;
    // }
    // public void OnSaveRouteSetToOs(int stageId)
    // {
    //     RouteSetConfig routeSetConfig = FindRouteSetConfig(stageId);
    //     routeSetConfig.SaveToOs(currentRouteLineRender);
    // }
    //
    //
    // public void OnLoadRouteSetFromOs(int stageId)
    // {
    //     RouteSetConfig routeSetConfig = FindRouteSetConfig(stageId);
    //     routeSetConfig.LoadFromOs(currentRouteLineRender);
    // }
}
[CustomEditor(typeof(RouteSetManager))]
public class RouteSetManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RouteSetManager myScript = (RouteSetManager)target;

        // if (GUILayout.Button("Save route set to OS"))
        // {
        //     myScript.OnSaveRouteSetToOs();
        // }
        // if (GUILayout.Button("Load route set from OS"))
        // {
        //     myScript.OnLoadRouteSetFromOs();
        // }
    }
}


