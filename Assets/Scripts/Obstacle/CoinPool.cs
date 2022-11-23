using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance;

    private List<GameObject> _pooledObjects = new List<GameObject>();
    private int _amountToPool = 10;

    public Transform ScorePlus;

    [SerializeField] private GameObject scorePrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(scorePrefab);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
            obj.transform.SetParent(ScorePlus);
            obj.transform.localPosition = new Vector3(100, 0, 0);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }
}
