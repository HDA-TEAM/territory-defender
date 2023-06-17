using System.Collections.Generic;
using UnityEngine;

public class RouteSetController : MonoBehaviour
{
    [SerializeField] private List<LineRenderer> currentRouteLineRenders = new List<LineRenderer>();
    public List<LineRenderer> CurrentRouteLineRenderers
    {
        get
        {
            return currentRouteLineRenders;
        } 
        set
        {
            currentRouteLineRenders = value;
        }
    }
}
