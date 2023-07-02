using UnityEngine;

[CreateAssetMenu(fileName = "TrungNhiSO", menuName = "Champion/TrungNhiScriptableObject", order = 2)]
public class TrungNhiSO : ScriptableObject
{
    //attack property
    [SerializeField] public float attackRange;
    [SerializeField] public float health;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float attackDame;
    [SerializeField] public bool isAttackState;

    //skill property
    public static bool IsChampEmploySkill;
    public static bool IsChampEmployInsticSkill;
    public static bool IsChampEmployActiveSkill;

    //movement property
    private bool _setMoving;
    private bool _moving;
    private Vector2 _lastClickedPos;
    private Vector3 _lastPosition;
    private Vector3 _currentPosition;
}