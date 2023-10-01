using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "RouteSetConfig_", menuName = "ScriptableObject/Database/Stage/RouteSetConfig")]
public class RouteSetConfig : ScriptableObject
{
    public int routeCount;
    [SerializeField] private List<List<Vector3>> _routeSet = new List<List<Vector3>>();
    
    public void SaveToConfig(List<LineRenderer> lineRouteSet)
    {
        // Clear current config
        _routeSet.Clear();
        for (int i = 0; i < lineRouteSet.Count; i++)
        {
            // Check if lineRender want to save
            if (lineRouteSet[i].gameObject.activeSelf)
            {
                _routeSet.Add(new List<Vector3>());
                
                for (int j = 0; j < lineRouteSet[i].positionCount; j++)
                    _routeSet[i].Add(lineRouteSet[i].GetPosition(j));
            }
        }
        routeCount = _routeSet.Count;
    }
    public List<LineRenderer> LoadFromConfig(List<LineRenderer> originLineSet)
    {
        // Set default state : not using 
        foreach (LineRenderer line in originLineSet)
            line.gameObject.SetActive(false);
        
        for (int i = 0; i < _routeSet.Count; i++)
        {
            // Check if this lineRender available to save
            if (!originLineSet[i].gameObject.activeSelf)
                originLineSet[i].gameObject.SetActive(true);
            
            // Init lineRender max space
            originLineSet[i].positionCount = _routeSet[i].Count;
            
            // Set position at each point in lineRender
            for (int j = 0; j < originLineSet[i].positionCount; j++)
                originLineSet[i].SetPosition(j,_routeSet[i][j]);
        }
        return originLineSet;
    }
}