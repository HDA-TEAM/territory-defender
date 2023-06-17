using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RouteSet_", menuName = "ScriptableObject/Stage/RouteSetConfig")]
public class RouteSetConfig : ScriptableObject
{
    public int routeCount;
    [SerializeField] private List<List<Vector3>> routeSet = new List<List<Vector3>>();
    
    public void SaveToOs(List<LineRenderer> lineRouteSet)
    {
        routeSet.Clear();
        for (int i = 0; i < lineRouteSet.Count; i++)
        {
            if (lineRouteSet[i].gameObject.activeSelf == true)
            {
                routeSet.Add(new List<Vector3>());
                for (int j = 0; j < lineRouteSet[i].positionCount; j++)
                {
                    routeSet[i].Add(lineRouteSet[i].GetPosition(j));
                }
            }
        }
        routeCount = routeSet.Count;
    }
    public List<LineRenderer> LoadFromOs(List<LineRenderer> originLineSet)
    {
        originLineSet.ForEach(line => line.gameObject.SetActive(false));
        
        for (int i = 0; i < routeSet.Count; i++)
        {
            if (originLineSet[i].gameObject.activeSelf == false)
            {
                originLineSet[i].gameObject.SetActive(true);
            }
            originLineSet[i].SetVertexCount(routeSet[i].Count);
            
            for (int j = 0; j < originLineSet[i].positionCount; j++)
            {
                originLineSet[i].SetPosition(j,routeSet[i][j]);
            }
        }
        return originLineSet;
    }
}