using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof (GridLayoutGroup))]
public class DynamicGridSizer : MonoBehaviour {
		
		private GridLayoutGroup mGridLayoutGroup;
		public int columns;
		public int rows;
		public int leftPaddingPercentage = 0;
		public int rightPaddingPercentage = 0;
		public int topPaddingPercentage = 0;
		public int bottomPaddingPercentage = 0;
		public bool dynamicSize = true;
		public enum Order{HorizonalFirst,VerticalFirst};
		public Order dynamicLayout = Order.HorizonalFirst;
		
		
		void Start () {
			mGridLayoutGroup = GetComponent<GridLayoutGroup>();
		}
		
		void SetGridToFitScreen(int columns, int rows)
		{
			float totalWidth = ((RectTransform)mGridLayoutGroup.transform).rect.width;
			float totalHeight =  ((RectTransform)mGridLayoutGroup.transform).rect.height;
			
			if(leftPaddingPercentage>0)
				mGridLayoutGroup.padding.left =  (int)(totalWidth*(0.01f*leftPaddingPercentage));
			if(rightPaddingPercentage>0)
				mGridLayoutGroup.padding.right =  (int)(totalWidth*(0.01f*rightPaddingPercentage));
			if(topPaddingPercentage>0)
				mGridLayoutGroup.padding.top =  (int)(totalHeight*(0.01f*topPaddingPercentage));
			if(bottomPaddingPercentage>0)
				mGridLayoutGroup.padding.bottom =  (int)(totalHeight*(0.01f*bottomPaddingPercentage));
			
			float xSpacing = mGridLayoutGroup.spacing.x * (columns-1);
			float ySpacing = mGridLayoutGroup.spacing.y * (rows-1);
			float newX =(totalWidth - (mGridLayoutGroup.padding.left + mGridLayoutGroup.padding.right + xSpacing))/columns;
			float newY = (totalHeight - (mGridLayoutGroup.padding.top + mGridLayoutGroup.padding.bottom + ySpacing))/rows;
			
			mGridLayoutGroup.cellSize = new Vector2(newX,newY);
			
		}
		int NumberOfEnabledElements()
		{		
			int count=0;
			int numberOfChildren = transform.childCount;
			for(int i = 0;i<numberOfChildren;i++)
				if(transform.GetChild(i).gameObject.activeSelf)
					count++;
			return count;	
		}
		
		void Update () {
			//need to set this all up as a callback
			if(dynamicSize)
			{
				int numberOfElements = NumberOfEnabledElements();
				columns = Mathf.CeilToInt(Mathf.Sqrt(numberOfElements));
				rows = Mathf.RoundToInt(Mathf.Sqrt(numberOfElements));
				if(dynamicLayout == Order.VerticalFirst)
				{
					//flip rows and columns math
					int tmp = rows;
					rows = columns;
					columns = tmp;
				}
			}
			SetGridToFitScreen(columns,rows);
			
		}
	}
	
