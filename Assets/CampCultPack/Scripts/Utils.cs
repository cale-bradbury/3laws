using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

	static public void DestroyAllChildrenIn(GameObject obj,bool recurse){
		for(int i = obj.transform.childCount-1;i>=0;i--){
			if(recurse)DestroyAllChildrenIn(obj.transform.GetChild(i).gameObject,true);
			GameObject.Destroy(obj.transform.GetChild(i).gameObject);
		}
	}
}
