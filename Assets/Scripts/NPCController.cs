using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class NPCController : MonoBehaviour
{
    public NPCType npcType;
    public Transform destinationA;
    public Transform destinationB;
    public Transform destinationC;
 
    public TagType? currentTag;
    private bool isTagged = false;
 
    public void AttachTag(TagType tag)
    {
        if (isTagged) return;
 
        currentTag = tag;
        isTagged = true;
 
        if (tag == TagType.A && npcType == NPCType.A)
        {
            StartCoroutine(MoveToDestination(destinationA.position));
        }
        else if (tag == TagType.B || tag == TagType.C)
        {
            TransporterManager.Instance.RequestTransport(this, tag);
        }
    }
 
    IEnumerator MoveToDestination(Vector3 target)
    {
        float time = 0;
        Vector3 start = transform.position;
        while (time < 1f)
        {
            transform.position = Vector3.Lerp(start, target, time);
            time += Time.deltaTime * 0.5f;
            yield return null;
        }
        transform.position = target;
    }
}