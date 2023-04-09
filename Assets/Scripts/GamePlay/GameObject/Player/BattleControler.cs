using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControler : MonoBehaviour {

    /// <summary>
    /// Determine the competitor for each player, who appered in a game when playing
    /// </summary>

    public static List<Player> _evils = new List<Player>();
    public static List<Player> _humans = new List<Player>();
    static public void AddEvil(Player player){
        _evils.Add(player);
    }
    static public void AddHuman(Player player){
        _humans.Add(player);
    }
    Player FindTarget(Player searcher, List<Player> listPlayer){
        Player target = null;
        float  dis = float.MaxValue;
        foreach (var player in listPlayer)
        {   
            if(player != null && searcher != null){
                float currentDis = Vector2.Distance(searcher.transform.position,player.transform.position);
                if(currentDis < searcher._rangeDetecting && currentDis < dis){
                    dis = currentDis;
                    target = player;
                }
            }
        }
        return target;
    }
    void RemovePlayer(Player player){
        if(_evils.Contains(player)){
            _evils.Remove(player);
        }
        else if(_humans.Contains(player)){
            _humans.Remove(player);
        }
        
    }
    bool IsPlayerDeadActive(Player player){
        if(player != null && player.gameObject.activeSelf == false){
            return true;
        }
        return false;
    }
    bool IsPlayerDie(Player player){
        if(player._health <= 0){
            return true;
        }
        return false;
        
    }
    void ActionControl(Player player, Player target){
        if(target != null){
            player.SetTarget(target);
        }
    }
    void RunningBattle(){
        RemovePlayer();
        foreach (var human in _humans){
            //Find target mode
            if(human._target == null){
                Player target = FindTarget(human,_evils);
                ActionControl(human,target);
            }
 
        }
        foreach (var evil in _evils){
            if(evil._target == null){
                Player target = FindTarget(evil,_humans);
                ActionControl(evil,target);
            }
        }
    }
    void RemovePlayer(){
        _evils.RemoveAll(IsPlayerDeadActive);
        _evils.RemoveAll(IsPlayerDie);
        _humans.RemoveAll(IsPlayerDeadActive);
        _humans.RemoveAll(IsPlayerDie);
    }
    private void Update() {
        RunningBattle();
    }
}