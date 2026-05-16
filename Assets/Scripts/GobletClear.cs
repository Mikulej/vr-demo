using UnityEngine;

public class GobletClear : MonoBehaviour
{
    bool triggered = false;
    [SerializeField] GobletCounter[] gobletCounters = new GobletCounter[3];
    void Update()
    {
        if(triggered == false){
            bool winCondition = true;
            foreach(GobletCounter gc in gobletCounters){
                if(gc.count != 0){
                    winCondition = false;
                }
            }
            if(winCondition){
                Debug.Log("You Win!");
                triggered = true;
            }
        }
    }
}
