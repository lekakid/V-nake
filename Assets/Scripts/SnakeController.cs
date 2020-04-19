using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Obejct")]
    public Transform Bush;
    public Character Head;
    public Transform BorderLimit;

    [Header("Delay")]
    public float MoveDelay = 0.12f;
    public float SpawnDelay = 0.12f;

    Vector2 _inputDirection = Vector2.zero;
    Vector2 _walkDirection;
    Vector2 _lastTailPos = Vector2.zero;
    List<Character> _tail = new List<Character>();
    List<Vector2> _tailPositions = new List<Vector2>();

    bool _adding = false;

    // Start is called before the first frame update
    void Start()
    {
        _tail.Add(Head);
        _tailPositions.Add((Vector2)Head.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0 & _walkDirection != Vector2.down) {
            _inputDirection = Vector2.up;
        }

        if (Input.GetAxisRaw("Vertical") < 0 & _walkDirection != Vector2.up) {
            _inputDirection = Vector2.down;
        }

        if (Input.GetAxisRaw("Horizontal") > 0 & _walkDirection != Vector2.left) {
            _inputDirection = Vector2.right;
        }

        if (Input.GetAxisRaw("Horizontal") < 0 & _walkDirection != Vector2.right) {
            _inputDirection = Vector2.left;
        }
    }

    IEnumerator SnakeMove() {
        while(true) {
            yield return new WaitUntil(() => !_adding);

            Vector2 NextPos = _tailPositions[0] + _inputDirection;
            _tailPositions.Insert(0, NextPos);
            _lastTailPos = _tailPositions[_tailPositions.Count - 1];
            _tailPositions.RemoveAt(_tailPositions.Count - 1);

            _walkDirection = _inputDirection;

            for(int i = 0; i < _tailPositions.Count; i++) {
                _tail[i].Walk(_tailPositions[i], MoveDelay);
            }
            yield return new WaitForSeconds(MoveDelay-0.01f);

            if(_tailPositions[0].x > BorderLimit.position.x || _tailPositions[0].x < 0 || 
               _tailPositions[0].y > BorderLimit.position.y || _tailPositions[0].y < 0) {
                StopSnake();
                GameManager.Instance.GameOver();
            }


            for(int i = 4; i < _tailPositions.Count; i++) {
                if(_tailPositions[0] == _tailPositions[i]) {
                    StopSnake();
                    GameManager.Instance.GameOver();
                }
            }

            if(_tailPositions[0] == (Vector2)Bush.position) {
                AddTail();
                MoveBush();
            }
        }
    }

    public void AddTail() {
        _adding = true;

        Character tail = SpawnManager.Instance.SpawnCharacter().GetComponent<Character>();
        tail.transform.SetParent(transform);
        tail.transform.position = Bush.position;
        tail.Spawn(500 - SpawnManager.Instance.TotalRescueCount);
        
        _tail.Add(tail);
        _tailPositions.Add(_lastTailPos);

        _adding = false;
    }

    public void MoveBush() {
        Vector2 pos = new Vector2();

        do {
            float x = Mathf.Round(Random.Range(0, BorderLimit.position.x));
            float y = Mathf.Round(Random.Range(0, BorderLimit.position.y));

            pos.x = x;
            pos.y = y;
        } while(CheckTail(pos));
        
        Bush.transform.position = pos;
    }

    public bool CheckTail(Vector2 pos) {
        foreach (Vector2 i in _tailPositions)
        {
            if(i == pos)
                return true;
        }

        return false;
    }

    public void InitSnake() {
        for(int i = 1; i < _tail.Count; i++) {
            Destroy(_tail[i]);
        }
        _tail.Clear();
        _tailPositions.Clear();

        _tail.Add(Head);
        _tailPositions.Add((Vector2)Head.transform.position);
    }

    public void PlaySnake() {
        StartCoroutine("SnakeMove");
    }

    public void StopSnake() {
        StopCoroutine("SnakeMove");
    }
}
