using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterOfWords : MonoBehaviour {

    protected int count = 0;

	protected void ChekIfReachedTheEnd()
    {
        if (count >= 6) CleanBlackBoard();
    }

    void CleanBlackBoard()
    {

    }


}
