using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transition : MonoBehaviour
{

    private Animator transicion;
    // Start is called before the first frame update
    void Start()
    {
        transicion = GetComponent<Animator>();
    }

   public void LoadScene(string Scene)
   {
     //  StartCoroutine(movement(transicion));
   }
   IEnumerator movement(string scene)
   {
       transicion.SetTrigger("stop");
       yield return new WaitForSeconds(5);
   }
}
