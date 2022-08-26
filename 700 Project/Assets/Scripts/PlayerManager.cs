using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private int arrowType;
    private bool levitateArrowPickedUp;

    public UIManager ui_M;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Levitate Power Up")
        {
            LevitateArrowPickedUp();
            Destroy(other.gameObject);
        }
    }

    public void SetArrowType(int typeIndex)
    {
        arrowType = typeIndex;
    }

    public void LevitateArrowPickedUp()
    {
        levitateArrowPickedUp = true;
        arrowType = 1;

        ui_M.SetArrowText("Levitate", Color.magenta);
    }

    public int GetArrowType()
    {
        return arrowType;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (levitateArrowPickedUp)
            {
                arrowType += 1;
                if (arrowType > 1)
                    arrowType = 0; 
            }

            if (arrowType == 0)
                ui_M.SetArrowText("Normal", Color.white);
            else if(arrowType == 1)
                ui_M.SetArrowText("Levitate", Color.magenta);
        }
    }
}
