using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public int StartingTailCnt = 4;
    public GameObject TailPrefab;

    public float RecordFreuency = 1.0f;
    private float _recordPosTimer;

    private List<GameObject> _tailPieces = new List<GameObject>();
    private IndexedQueue<Vector2> _positionQueue = new IndexedQueue<Vector2>();

    Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();

        for(int i = 0; i < StartingTailCnt; ++i)
        {
            _tailPieces.Add(GameObject.Instantiate(TailPrefab, transform.position, Quaternion.identity));
        }

        _recordPosTimer = RecordFreuency;

        int queueMaxSize = GetQueueMaxSize();
        for(int i = 0; i < queueMaxSize; ++i)
        {
            _positionQueue.Enqueue(transform.position.ToVec2XY());
        }
    }

    void Update()
    {
        _recordPosTimer -= Time.deltaTime;
        if(_recordPosTimer <= 0)
        {
            _recordPosTimer = RecordFreuency + _recordPosTimer;

            if(_positionQueue.Count >= GetQueueMaxSize())
            {
                _positionQueue.Dequeue();
                _positionQueue.Enqueue(transform.position.ToVec2XY());
            }
        }

        for(int i = 0; i < _tailPieces.Count; ++i)
        {
            Physics2D.IgnoreCollision(_collider, _tailPieces[i].GetComponent<Collider2D>(), true);// i == 0);
           _tailPieces[i].transform.position = _positionQueue[_positionQueue.Count - 1 - i].ToVec3XY();
        }
    }

    int GetQueueMaxSize()
    {
        return _tailPieces.Count;
    }
}
