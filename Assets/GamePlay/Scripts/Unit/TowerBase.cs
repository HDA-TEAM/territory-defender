using UnityEngine;
using UnityEngine.UI;

public class TowerBase : MonoBehaviour
{
    [SerializeField] private Button button; 
    private TowerKitManager towerKitParent;
 
    // public void Reset()
    // {
    //     if (button == null)
    //     {
    //         button = this.GetComponent<Button>();
    //     }
    // }
    public void OpenToolKit()
    {
        
    }
    public void TowerUpdate()
    {
        //todo 
        // Fake inventory to check enough coin to update
        
    }
    public void TowerSelling()
    {
        //todo 
        // Fake inventory to add  coin 
        // if sell success
        towerKitParent.ResetTowerKit();
        Destroy(this.gameObject);
    }
    public void TowerBuild(TowerKitManager towerKitManager)
    {
        towerKitParent = towerKitManager;
        this.transform.position = towerKitParent.transform.position;
        this.transform.SetParent(towerKitParent.transform.parent);
        //todo
        // Fake inventory to check enough coin to update
    }
    public void Detail()
    {
        //todo
        // Catch event in this object then show object information
    }
    
    // public abstract void Flag();
}

