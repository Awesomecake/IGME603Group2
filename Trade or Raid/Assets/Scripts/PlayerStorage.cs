using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] public Resource carriedResource;
    //[SerializeField] float harvestRange = 1;
    [SerializeField] Castle myCastle;

    public void PickUpResource(Resource newResource)
    {
        carriedResource = newResource;
        myCastle.TurnOnCircleIndicator();
    }

    public void DepositResource()
    {
        carriedResource.DepositResource(myCastle.transform);
        carriedResource = null;
        myCastle.TurnOffCircleIndicator();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, harvestRange);
    //}
}
