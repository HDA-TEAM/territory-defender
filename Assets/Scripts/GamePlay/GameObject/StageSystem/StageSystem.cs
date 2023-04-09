using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSystem : MonoBehaviour
{
    /// <summary>
    /// 1) Mapping data from Stage.xml to a stage and spawn object depend on data loaded
    /// 2) Provide Method to draw route, determine tower place position then write down to XML for create a new stage
    /// </summary>

    [SerializeField] private  List<GameObject> _playerTypeInBattle;
    [SerializeField] private  List<int> _numbers;
    private  List<LineRenderer> _gates;
    [SerializeField] private int _stageId = 1;
    [SerializeField] private  RouteSet _routeSet;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnWaveInterval;
    [SerializeField] private List<ObjectPooler> _poolers = new List<ObjectPooler>();



    private StageData _stageData;
    private WaveData _currentWaveData;
    private SubwaveData _currentSubwaveData;

    
    public void SaveGateSet(){

    }
    protected bool MappingStageData(int stageId, string difficultyType){
        _stageData = XMLControler._stageDataList.FindStageData(stageId);
        return true;
    }


    private void Start() {
        MappingStageData(_stageId,"");
        GameControl.CurrentWave = 1;
        GameControl.MaxWave = _stageData.waveList.Count;
        _gates = _routeSet.GetGatesOfStage(_stageId);

        BuidingPlaceController.MappingTowerPlaceData(_stageId);

        // BuidingPlaceController.WriteDownTowerPlaceSetForStage(_stageId);

        // RouteSet.WriteDownGateSetForStage(_stageId);



        // Spawner instance = new Spawner(_poolers[0],_gates[1]);
        // instance.SpawnerSingleObject();

    
    }
   
    public void CallWave(){
        StartCoroutine(SpawnWaveOfStage());
        
    }
    IEnumerator SpawnWaveOfStage(){
        foreach (var wave in _stageData.waveList)
        {   
            _currentWaveData = wave;
            StartCoroutine(SpawnSubwaveOfWave());
            float totalTimeWave = _currentWaveData.GetTotalTimeWave(_spawnInterval) + _spawnWaveInterval;
            yield return new WaitForSeconds(totalTimeWave);
            
            if(GameControl.CurrentWave  < GameControl.MaxWave){
                GameControl.CurrentWave ++;
            }
        }
    }
    IEnumerator SpawnSubwaveOfWave(){
        for (int i = 0; i < _currentWaveData.subwaveList.Count; i++)
        {
            _currentSubwaveData = _currentWaveData.subwaveList[i];
            StartCoroutine(SpawnObjectOfSubwave());
            float timeNextSubwave = 0f;
            if( i + 1 < _currentWaveData.subwaveList.Count){
                timeNextSubwave = _currentWaveData.subwaveList[i+1].appearTime;
            }
            yield return new WaitForSeconds(timeNextSubwave);
        }
    }
    IEnumerator SpawnObjectOfSubwave(){
        int gateId = _currentSubwaveData.gateId;
        int numberObject =_currentSubwaveData.number;
        int typeObjectId = _currentSubwaveData.unitID;
        for (int i = 0; i < numberObject; i++)
        {
            SpawnObject(typeObjectId,_gates[gateId]);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
    void SpawnObject(int type,LineRenderer gate){
        Spawner instance = new Spawner(_poolers[type],gate);
        instance.SpawnerSingleObject();
    }
    
}
