using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorDOGOption : DOGOption
{
    public List<OutdoorDOGOption> accessibleOutdoors = new List<OutdoorDOGOption>();

    public DOGOption enterPoint;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerBodyTag)
        {
            NavigationManager.instance.currentPlayerOutdoor = this;
        }
    }

    public List<OutdoorDOGOption> getAccessibleOutdoors()
    {
        return accessibleOutdoors;
    }
}
