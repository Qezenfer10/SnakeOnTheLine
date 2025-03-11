using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class SnakeManager : MonoBehaviour
{

    #region
    public static SnakeManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion



    public List<Transform> PrePositions;

    public GameObject snake;
    public GameObject apple;
    public GameObject target;

    public GameObject foodImage;

    public float SearhDistance;

    private void Start()
    {
        GetNewApple();
    }

    private void Update()
    {
        if(GameManager.instance.snake.activeSelf)
        if (target != null)
        {
            if(snake.GetComponent<NavMeshAgent>() != null ) snake.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            if(target == GameManager.instance.player) foodImage.transform.position = target.transform.position;
        }
        else
        GoToPlayer();
    }

    public void GoToPlayer()
    {
        if (PhoneScript.instance.Mode == Application.Snake) PhoneScript.instance.currentMode.GetComponent<PackmanScript>().playerMarker.SetActive(false);
        target = GameManager.instance.player;
        Debug.Log("whaa");
    }

    public void GetNewApple()
    {
        if(target != null) target = GameManager.instance.player;
        GoToPlayer();

        if (PhoneScript.instance.Mode == Application.Snake) PhoneScript.instance.currentMode.GetComponent<PackmanScript>().playerMarker.SetActive(true);
        List<Transform> pos = PrePositions.Where(a => Vector3.Distance(a.position, GameManager.instance.player.transform.position) > SearhDistance).ToList();
        target = Instantiate(apple, pos[Random.Range(0, pos.Count)].position, Quaternion.identity);
        foodImage.transform.position = target.transform.position;

        Debug.Log(target.transform.position);


    }
}
