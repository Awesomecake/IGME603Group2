using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] public Resource carriedResource;
    [SerializeField] float harvestRange = 1;
    [SerializeField] Castle myCastle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!carriedResource)
        {
            Resource newResource = collision.GetComponent<Resource>();
            PickUpResource(newResource);
            carriedResource.GatherResource(transform);
        }
    }

    void PickUpResource(Resource newResource)
    {
        carriedResource = newResource;
    }

    public void DepositResource()
    {
        carriedResource.DepositResource(myCastle.transform);
        carriedResource = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, harvestRange);
    }
}
