using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_AttackRange : MonoBehaviour
{
    public Vector3 target;
    public List<GameObject> targets = new List<GameObject>();

    public E_unitMove e_unit;
    public UnitController parent;

    string enemy = "Enemy";

    private void OnEnable()
    {
        parent = transform.GetComponentInParent<UnitController>();
        targets.Clear();
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(enemy))
        {
            StartCoroutine("Find_Target");
            targets.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(enemy))
        {
            targets.Remove(col.gameObject);
            e_unit = null;

            if (targets == null)
            {
                parent.u_State = UnitController.unitState.Idle;
                StopCoroutine("Find_Target");
            }
        }
    }

    IEnumerator Find_Target()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        if (targets != null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                target = targets[i].transform.position;
                e_unit = targets[i].GetComponent<E_unitMove>();
                if (e_unit.ehealth > 0 && parent.uhealth > 0)
                {
                    parent.Attack(target, e_unit);
                    parent.u_State = UnitController.unitState.Battle;
                }
                if (e_unit.ehealth <= 0)
                {
                    e_unit = null;
                    targets.Remove(targets[i]);
                    parent.u_State = UnitController.unitState.Idle;
                }
            }
        }

        yield return wait;

        StartCoroutine("Find_Target");
    }


}
