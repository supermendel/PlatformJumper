using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public MyPatform _platformObject;
    public int platformsToSpawn;
    public Vector3 _moveSpeed;
    public Vector3 _platformSpacing;
    [SerializeField]
    public Vector3 _startPosRandomOffsetRange;

    // Start is called before the first frame update
    void Start()
    {
        var currentPos = Vector3.zero;
        var resetPos = _platformSpacing * platformsToSpawn;
        for (int i = 0; i < platformsToSpawn; i++)
        {
            var cloned = Instantiate(_platformObject,currentPos,Quaternion.identity);
            cloned._moveOffset = _moveSpeed;
            cloned._startingPos = resetPos;
            cloned._startPosRandomOffsetRange = _startPosRandomOffsetRange;
            currentPos += _platformSpacing;
        }
    }

}
