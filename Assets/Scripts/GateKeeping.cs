using System.Collections.Generic;
using UnityEngine;

public class GateKeeping : MonoBehaviour
{
    
    public List<OnPlateChecker> blockChildren = new List<OnPlateChecker>();
    int blockCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Transform blockParent = transform.GetChild(2);
        foreach(Transform child in blockParent) {
            if (child != null) {
                blockChildren.Add(child.GetComponent<OnPlateChecker>());
            }
        }
        blockCount = blockChildren.Count;
        Debug.Log("blockcount: " + blockCount);
    }

    public void CheckAllPlates() {
        int count = 0;
        foreach(OnPlateChecker c in blockChildren) {
            if (c.isOnPlate) count++;
        }
        Debug.Log("onplate: " + count);
        if (count >= blockCount) OpenGate();
        else CloseGate();
    }

    public void OpenGate() {
        GameObject doorL, doorR;
        doorL = transform.GetChild(0).GetChild(1).gameObject;
        doorR = transform.GetChild(0).GetChild(2).gameObject;
        LeanTween.moveX(doorL, doorL.transform.position.x-1, 3).setEaseOutBounce();
        LeanTween.moveX(doorR, doorR.transform.position.x+1, 3).setEaseOutBounce();
    }
    public void CloseGate() {

    }

}
