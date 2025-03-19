using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SnakeScript : MonoBehaviour
{
    public static SnakeScript instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public GameObject snakeParent;
    public GameObject snakeHead;
    public GameObject snakeBodyPrefab;
    public List<GameObject> snakeBodyParts;


    [SerializeField]
    List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        //StartCoroutine( SnakeGrowing(3));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetPositions();
    }

    //public IEnumerator SnakeGrowing(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    Debug.Log("Go");
    //    GameObject go;
    //    if (snakeBodyParts.LastOrDefault() != null) go = Instantiate(snakeBodyPrefab, snakeBodyParts.LastOrDefault().transform.position,Quaternion.identity);
    //    else go = Instantiate(snakeBodyPrefab, snakeHead.transform.position, Quaternion.identity);
    //    snakeBodyParts.Add(go);

    //    //StartCoroutine(SnakeGrowing(time));

    //}

    public void SnakeGrowing()
    {
        GameObject go;
        //if (snakeBodyParts.LastOrDefault() != null)
        
        go = Instantiate(snakeBodyPrefab, snakeBodyParts[snakeBodyParts.Count - 1].transform.position, Quaternion.identity);
        
        //else go = Instantiate(snakeBodyPrefab, snakeHead.transform.position, Quaternion.identity);
        snakeBodyParts.Insert(snakeBodyParts.Count - 1 , go);

        //StartCoroutine(SnakeGrowing(time));

    }


    //public void SnakeGrowing()
    //{
    //    Debug.Log("Go");
    //    GameObject go = Instantiate(snakeBodyPrefab);
    //    snakeBodyParts.Add(go);

    //}

    public int Gap = 500;
    public void SetPositions()
    {

        // Store position history
        
        positions.Insert(0, snakeHead.transform.position);

        // Move body parts
        int index = 0;

        foreach (var body in snakeBodyParts)
        {
            Vector3 point = positions[Mathf.Min(index * Gap, positions.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * snakeHead.GetComponentInChildren<NavMeshAgent>().speed * Time.deltaTime;

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }


        //if(index > 0) positions.RemoveRange(Mathf.Clamp(snakeBodyParts.Count * Gap, 0, positions.Count - 1), positions.Count - 1 - Mathf.Clamp(snakeBodyParts.Count * Gap, 0, positions.Count - 1));
    }
}
