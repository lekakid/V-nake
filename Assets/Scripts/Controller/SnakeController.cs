using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeController : MonoBehaviour
{
    [Header("View")]
    public SnakeView SnakeView;

    [Header("Controller")]
    public PauseController PauseController;
    public ResultController ResultController;
    public DialogueController DialogueController;

    [Header("Model")]
    public CharacterDatabase CharacterDatabase;

    [Header("Obejct")]
    public Transform Bush;
    public Character Head;

    [Header("Audio")]
    public AudioSource BGM;
    public AudioSource BushSFX;
    public AudioSource DeadSFX;

    [Header("Misc")]
    public Transform BorderLimit;

    // Move Delay
    float _delay = 0.12f;

    // Default Position
    Vector2 _defaultPos;
    Vector2 _defaultBushPos;

    // Input Data
    Queue<Vector2> _inputBuffer = new Queue<Vector2>();
    Vector2 _lastInput;
    Vector2 _walkDirection;
    Vector2 _lastTailPos;

    // Snake Position Data
    List<Character> _tail = new List<Character>();
    List<Vector2> _tailPositions = new List<Vector2>();

    bool _adding = false;

    void Awake()
    {
        _tail.Add(Head);
        _tailPositions.Add((Vector2)Head.transform.position);
        _defaultPos = Head.transform.position;
        _defaultBushPos = Bush.transform.position;

        GameManager.SetController(this);
    }

    void Start() {
        SnakeView.Show();
        StartCoroutine("SnakeMove");
    }

    void Update()
    {
        Status.Instance.PlayTime += Time.deltaTime;

        if(Input.GetButtonDown("Cancel")) {
            GameManager.SetController(PauseController);
            GameManager.Pause();
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(x, (x == 0) ? y: 0);
        bool check = (_lastInput.x + input.x) != 0 || (_lastInput.y + input.y) != 0;

        if(input != Vector2.zero && _lastInput != input && check && _inputBuffer.Count < 3) {
            _inputBuffer.Enqueue(input);
            _lastInput = input;
        }
    }

    IEnumerator SnakeMove() {
        Status.Instance.PlayCount++;

        while(true) {
            yield return new WaitUntil(() => !_adding);

            _walkDirection = (_inputBuffer.Count > 0) ? _inputBuffer.Dequeue() : _walkDirection;

            if(_walkDirection.magnitude != 1f) {
                yield return new WaitForSeconds(_delay-0.005f);
                continue;
            }

            Vector2 NextPos = _tailPositions[0] + _walkDirection;
            _tailPositions.Insert(0, NextPos);
            _lastTailPos = _tailPositions[_tailPositions.Count - 1];
            _tailPositions.RemoveAt(_tailPositions.Count - 1);

            if(_tailPositions[0].x > BorderLimit.position.x || _tailPositions[0].x < 0 || 
               _tailPositions[0].y > BorderLimit.position.y || _tailPositions[0].y < 0) {
                GameOver();
                yield return null;
            }

            for(int i = 4; i < _tailPositions.Count; i++) {
                if(_tailPositions[0] == _tailPositions[i]) {
                    GameOver();
                    yield return null;
                }
            }

            for(int i = 0; i < _tailPositions.Count; i++) {
                _tail[i].Walk(_tailPositions[i], _delay);
            }
            Status.Instance.CurrentWalkCount++;
            yield return new WaitForSeconds(_delay-0.005f);

            if(_tailPositions[0] == (Vector2)Bush.position) {
                AddTail();
                MoveBush();
            }
        }
    }

    public void AddTail() {
        _adding = true;

        float r = Random.Range(0f, 100f);

        CharacterScriptableObject data = CharacterDatabase.GetRandomCharacterData();
        Status.Instance.CurrentCharacterRescueCounts[data.name]++;
        Status.Instance.CurrentRescueCount++;

        Character tail = Instantiate(data.CharacterPrefab).GetComponent<Character>();
        tail.transform.SetParent(transform);
        tail.transform.position = Bush.position;
        tail.Spawn(500 - Status.Instance.CurrentRescueCount);
        
        _tail.Add(tail);
        _tailPositions.Add(_lastTailPos);

        SnakeView.SetScore(Status.Instance.CurrentRescueCount);

        _adding = false;

        BushSFX.Play();
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

    bool CheckEndingShow() {
        if(Status.Instance.Ending)
            return false;

        foreach(string name in Status.Instance.CharacterRescueCounts.Keys) {
            if(Status.Instance.CharacterRescueCounts[name] < 1)
                return false;
        }

        if(Status.Instance.CurrentRescueCount < 80){
            return false;
        }

        return true;
    }

    public void GameOver() {
        GameManager.Pause();
        Head.GetComponent<Animator>().SetBool("isDefeat", true);
        DeadSFX.Play();
        BGM.DOFade(0f, 0.6f).SetUpdate(true);
        GameManager.SetController(ResultController);
        ResultController.DrawResult();
        if(CheckEndingShow()) {
            Status.Instance.Ending = true;
            GameManager.SetController(DialogueController);
            DialogueController.RunDialogueScript("Ending");
        }
        Status.Instance.Save();
        Status.Instance.Initialize();
    }

    public void Reset() {
        StopCoroutine("SnakeMove");
        
        _lastInput = Vector2.zero;
        _walkDirection = Vector2.zero;
        _inputBuffer.Clear();

        for(int i = 1; i < _tail.Count; i++) {
            _tail[i].Remove();
        }
        _tail.Clear();
        _tailPositions.Clear();

        Head.transform.position = _defaultPos;
        _tail.Add(Head);
        _tailPositions.Add(_defaultPos);
        Head.GetComponent<Animator>().SetBool("isDefeat", false);
        Head.CancelWalk();

        Bush.transform.position = _defaultBushPos;

        Status.Instance.Initialize();
        SnakeView.SetScore(0);

        BGM.volume = 1f;

        StartCoroutine("SnakeMove");
    }
}
