using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoreController : MonoBehaviour
{
    public GameEvent test;

    private void OnEnable()
    {
        test.AddListener(OnTestEvent);
    }

    private void OnDisable()
    {
        test.RemoveListener(OnTestEvent);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Event Handle

    private void OnTestEvent()
    {
        Debug.Log("Do something");
    }

    #endregion
}
