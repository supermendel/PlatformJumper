using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPatform : MonoBehaviour
{
    [SerializeField]
    public Vector3 _moveOffset;
    [SerializeField]
    public Vector3 _startingPos;
    [SerializeField]
    public Vector3 _startPosRandomOffsetRange;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(_moveOffset * Time.deltaTime);


        if (this.transform.position.y < 0)
        {
            ResetPlatform();
        }
    }

    private void ResetPlatform()
    {
        this.transform.position = _startingPos + Vector3.Lerp(-_startPosRandomOffsetRange, _startPosRandomOffsetRange,Random.Range(0,1f));
        this.transform.localScale = Vector3.one + Vector3.Lerp(Vector3.zero, Vector3.right * 3, Random.Range(0, 1f));
        spriteRenderer.color = Color.HSVToRGB(Random.Range(0, 1f), 1f, 1);
    }
}
