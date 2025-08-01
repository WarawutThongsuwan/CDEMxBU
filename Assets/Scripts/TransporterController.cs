using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TransporterController : MonoBehaviour
{
    public bool isBusy = false;
 
    public IEnumerator MoveAndPickup(NPCController npc, Vector3 target)
    {
        isBusy = true;
        yield return new WaitForSeconds(10f);
 
        // Move to NPC
        Vector3 start = transform.position;
        Vector3 npcPos = npc.transform.position;
 
        float t = 0;
        while (t < 1f)
        {
            transform.position = Vector3.Lerp(start, npcPos, t);
            t += Time.deltaTime * 0.5f;
            yield return null;
        }
 
        // Attach NPC
        npc.transform.SetParent(this.transform);
        npc.transform.localPosition = Vector3.up; // Lifted onto transporter
 
        // Move to destination
        Vector3 dest = (npc.currentTag == TagType.B) ? npc.destinationB.position : npc.destinationC.position;
 
        t = 0;
        start = transform.position;
        while (t < 1f)
        {
            transform.position = Vector3.Lerp(start, dest, t);
            t += Time.deltaTime * 0.5f;
            yield return null;
        }
 
        // Drop NPC
        npc.transform.SetParent(null);
        isBusy = false;
    }
}