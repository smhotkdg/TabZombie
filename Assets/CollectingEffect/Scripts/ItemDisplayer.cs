using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemDisplayer : MonoBehaviour {

	// Reference to the Text component displaying the item collected quantity
	[Tooltip("Reference to the Text component displaying the item collected quantity")]
	public Text _itemDisplay;

	// The current item collected quantity
	private int _itemQuantity = 0;

	// Add 'quantity' items and update the UI text
	public void AddItem(int quantity) {
		_itemQuantity += quantity;
		_itemDisplay.text = _itemQuantity.ToString();
	}
}
