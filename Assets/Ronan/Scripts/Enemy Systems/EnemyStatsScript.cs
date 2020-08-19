using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScript : MonoBehaviour
{
    public EnemyInfo Info;
    public int Health = 20;
    public GameObject ItemPickup;
    public static GameObject PickupTemplate;

    private void Awake()
    {
        //TODO: Set global item pickup
        if(ItemPickup != null)
        {
            PickupTemplate = ItemPickup;
        }
    }

    private void Start()
    {
        Health = Info.Health;
    }

    public void ApplyDamage(int value)
    {
        if(Health > 0)
        {
            Health -= value;
        //Debug.Log("DAMAGE");

            if(Health <= 0)
            {
                StartCoroutine(DeathLogic());
            }
        }
    }

    public IEnumerator DeathLogic()
    {
        //TODO Death animation

        PlayerController.Instance.AddExperience(Info.Experience);
        MoneyHUDController.Instance.SetAmount(PlayerController.Instance.Money);

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Level Geometry"));
        
        //TODO: Instantiate ground item at hit.point then get random item from treasue table
        if(hit.collider.gameObject != null)
        {
            Debug.Log(Info.TreasureTable.GetItemDrop().Name);
            GameObject go = Instantiate(ItemPickup);
            go.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            go.GetComponent<CollectableItem>().Item = Info.TreasureTable.GetItemDrop();
        }

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }


}
