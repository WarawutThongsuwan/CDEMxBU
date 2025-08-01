using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 
public class TransporterManager : MonoBehaviour
{
    public static TransporterManager Instance;
    public List<TransporterController> transporters;
 
    void Awake()
    {
        Instance = this;
    }
 
    public void RequestTransport(NPCController npc, TagType tag)
    {
        TransporterController freeTransport = transporters.FirstOrDefault(t => !t.isBusy);
        if (freeTransport != null)
        {
            StartCoroutine(freeTransport.MoveAndPickup(npc, npc.transform.position));
        }
    }
}