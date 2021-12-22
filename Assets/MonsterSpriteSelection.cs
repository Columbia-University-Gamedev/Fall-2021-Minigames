using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpriteSelection : MonoBehaviour
{
    [SerializeField]
    bool _shouldChooseRandomMonsterOnAwake = true;

    Animator _animator;

    const int MONSTER_COUNT = 3;

    [SerializeField]
    int _monsterIndex = 0;
    public int MonsterIndex
    {
        get { return _monsterIndex; }
        set
        {
            _monsterIndex = value;

            _animator.SetInteger("MonsterIndex", value);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_shouldChooseRandomMonsterOnAwake)
        {
            MonsterIndex = Random.Range(0, MONSTER_COUNT);
        } else
        {
            _animator.SetInteger("MonsterIndex", _monsterIndex);
        }
    }
}
