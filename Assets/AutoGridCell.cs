using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AutoGridCell : MonoBehaviour 
{
	GridLayoutGroup mGridLayoutGroup;
	RectTransform mRectTransform;
	void Awake()
	{
		mRectTransform = GetComponent<RectTransform>();
		mGridLayoutGroup = GetComponent<GridLayoutGroup>();


		Debug.Log(mRectTransform.parent.lossyScale.ToString());
	}
}