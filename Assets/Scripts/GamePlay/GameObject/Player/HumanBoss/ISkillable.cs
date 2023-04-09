using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillable //Strategy
{
    void FetchSkill(GameObject prefab, Transform transform, Player target);

} 


public class InsticSkill: MonoBehaviour, ISkillable{
    
    public void FetchSkill(GameObject prefab,Transform transform, Player target){
        BurneSkill burneskill = Instantiate(prefab.GetComponent<BurneSkill>(),transform.position, Quaternion.identity);

        burneskill.gameObject.SetActive(true);

        burneskill._target = target;
        //burneskill.Employing();
        
        //burneskill.gameObject.SetActive(false);
        Destroy(burneskill.gameObject, 1f);
        Destroy(this.gameObject, 1f);
    } 
}

public class ActiveSkill: MonoBehaviour, ISkillable{
    
    public void FetchSkill(GameObject prefab,Transform transform, Player target){
        HellFire hellfire = Instantiate(prefab.GetComponent<HellFire>(),transform.position, Quaternion.identity);

        hellfire.gameObject.SetActive(true);

        hellfire._target = target;
        hellfire.Employing();
        
        //hellfire.gameObject.SetActive(false);

        Destroy(hellfire.gameObject, 1f);
        Destroy(this.gameObject, 1f);
        
    }
}
