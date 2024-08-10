using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using Features.Common.Scripts;
using Features.Quest.Scripts.Time;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Quest.Scripts.Quest
{
    public class ListQuestViewModel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private List<ItemTaskView> _itemTaskViews;
        [SerializeField] private Image _imgInventoryGet;
        [SerializeField] private TextMeshProUGUI _txtNumberInventory;
        [SerializeField] private Image _imgNotifyOnUI;
        [SerializeField] private TextMeshProUGUI _txtNotifyMessage;
        
        [Header("Data")] 
        [SerializeField] private ListTimeViewModel _listTimeViewModel;
        [SerializeField] private InventoryDataAsset _inventoryDataAsset;
        [SerializeField] private QuestDataController _questDataController;

        private List<InventoryData> _listInventoryReceived;
        private QuestType _preQuestType;
        private bool _validDateTimeChange;
       
        private void OnEnable()
        {
            if (_questDataController != null)
            {
                _validDateTimeChange = true;
                _questDataController.OnDateTimeChange += UpdateData;
            }
            
            if (_listTimeViewModel != null)
            {
                _listTimeViewModel._onUpdateViewAction += UpdateView;
            }
        }
        
        private void OnDisable()
        {
            if (_questDataController != null)
            {
                _validDateTimeChange = false;
                _questDataController.OnDateTimeChange -= UpdateData;
            }
            
            if (_listTimeViewModel != null)
            {
                _listTimeViewModel._onUpdateViewAction -= UpdateView;
            }
        }

        private void Setup()
        {
            _preQuestType = QuestType.DailyQuest;
            _imgInventoryGet.gameObject.SetActive(false);
            _imgNotifyOnUI.gameObject.SetActive(false);
        }

        private void Start()
        {
            Setup();
            UpdateData();
        }

        private void UpdateData()
        {
            //Default setting
            _questDataController.CheckLoginTask();
            UpdateView(_preQuestType);
        }

        private void UpdateView(QuestType questType)
        {
            if (_preQuestType == questType && !_validDateTimeChange)
            {
                return;
            }
            _preQuestType = questType;
       
            var tasks = GetTasksByTimeType(questType);
            IItemSetupView<TaskDataSO> setupItemTask = new SetupItemTask();
            
            for (int i = 0; i < _itemTaskViews.Count; i++)
            {
                if (i < tasks.Count)
                {
                    _itemTaskViews[i].gameObject.SetActive(true);
                    _itemTaskViews[i].Initialize(setupItemTask, tasks[i], OnSelectedGet);
                    
                    if (!tasks[i].IsCompleted)
                    {
                        _itemTaskViews[i].SetUnCompleted("UnCompleted");
                    }
                    else if (tasks[i].IsCompleted && !tasks[i].IsGotten)
                    {
                        _itemTaskViews[i].SetCompleted(""); //Todo
                    }
                    else
                    {
                        _itemTaskViews[i].SetUnCompleted("Received");
                    }
                }
                else
                {
                    _itemTaskViews[i].gameObject.SetActive(false);
                }
            }
        }

        private List<TaskDataSO> GetTasksByTimeType(QuestType questType)
        {
            var questComposite = _questDataController.QuestComposites.Find(quest => quest.Type == questType);
            return questComposite.ListTasks.Count > 0 ? questComposite.ListTasks : new List<TaskDataSO>();
        }

        private void OnSelectedGet(ItemViewBase<TaskDataSO> itemTaskView)
        {
            ItemTaskView taskView = itemTaskView as ItemTaskView;
            if (taskView != null)
            {
                if (!taskView.TaskDataSo.IsCompleted)
                {
                    // Debug.Log("You haven't completed the task...");
                    
                    string msg = "You haven't completed the task";
                    _txtNotifyMessage.text = msg;
                   StartCoroutine(ShowImageNotifyMessage());
                }
                
                else if (taskView.TaskDataSo.IsCompleted && !taskView.TaskDataSo.IsGotten)
                {
                    TaskDataSO foundTask = taskView.TaskDataSo;
                    
                    foundTask.IsCompleted = true; // Mark task as completed
                    foundTask.IsGotten = true;

                    _listInventoryReceived = taskView.InventoryGetAfterCompleteTask;
                    foreach (var item in _listInventoryReceived)
                    {
                        // Update inventory
                        _inventoryDataAsset.TryChangeInventoryData(item.InventoryType, item.Amount);
                    
                        // Update view of rewards
                        _txtNumberInventory.text = item.Amount.ToString();
                
                        // Update view of button Get
                        var btnGet =_itemTaskViews.Find(itemView => itemView == itemTaskView);
                   
                        // Todo: Update Time complete task
                        _questDataController.UpdateTaskCompletedData(btnGet.TaskDataSo._taskId);
                        btnGet.SetUnCompleted("Received");
                        
                        StartCoroutine(ShowImageReceivedReward());
                    }
                }
                
                else
                {
                    //Debug.Log("You received this reward...");
                    string msg = "You received this reward";
                    _txtNotifyMessage.text = msg;
                    StartCoroutine(ShowImageNotifyMessage());
                }
                
            }
        }
        
        private IEnumerator ShowImageReceivedReward()
        {
            _imgInventoryGet.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            _imgInventoryGet.gameObject.SetActive(false);
        }
        
        private IEnumerator ShowImageNotifyMessage()
        {
            _imgNotifyOnUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f); // Wait for 5 seconds
            _imgNotifyOnUI.gameObject.SetActive(false);
        }
    }
}
