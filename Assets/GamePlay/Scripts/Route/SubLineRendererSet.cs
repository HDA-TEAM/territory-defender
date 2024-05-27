using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.Route
{
    public class SubLineRendererSet : MonoBehaviour
    {
        public List<LineRenderer> CurSubLineRenderers;
        public List<LineRenderer> GetActiveSubLine()
        {
            List<LineRenderer> activeLines = new List<LineRenderer>();
            foreach (var line in CurSubLineRenderers)
            {
                if (line.gameObject.activeSelf)
                {
                    activeLines.Add(line);
                }
            }
            return activeLines;
        }
        public LineRenderer GetCenterSubLineRenderer()
        {
            int lineCount = CurSubLineRenderers.Count;
            return lineCount > 0 ? CurSubLineRenderers[lineCount / 2] : null;
        }
        public void SetMultiplier(float widthMultiplier)
        {
            foreach (LineRenderer line in CurSubLineRenderers)
                line.widthMultiplier = widthMultiplier;
        }
        public LineRenderer GetRandomSubLineRenderer()
        {
            int lineIndex = Random.Range(0, CurSubLineRenderers.Count - 1);
            return CurSubLineRenderers[lineIndex];
        }
    }
}
