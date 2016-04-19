using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

enum DragMode
{
	RIGIDBODY_KINEMATIC,
}

class DragInfo
{
	public GameObject target = null;
	public Vector3 offset = Vector3.zero;
	public DragMode mode = DragMode.RIGIDBODY_KINEMATIC;
	public Rigidbody rigidbody = null;
	public Plane plane = new Plane(Vector3.up, 0.0f);
}

public class PlanarMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	DragInfo info = new DragInfo();

	void Update()
	{
	}

	void MoveObjectToPosition(GameObject gameObject, Vector3 worldPosition)
	{
		if (info.rigidbody != null)
		{
			info.rigidbody.MovePosition(worldPosition);
		}
		else
		{
			gameObject.transform.position = worldPosition;
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		info.target = eventData.pointerDrag;

		info.offset = info.target.transform.position - eventData.pointerPressRaycast.worldPosition;

		info.rigidbody = info.target.GetComponent<Rigidbody>();

		if (info.rigidbody != null)
		{
			info.rigidbody.isKinematic = true;
		}
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
		MoveObjectToPosition(info.target, worldPosition + info.offset);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
		MoveObjectToPosition(info.target, worldPosition + info.offset);

		info = new DragInfo();
	}
}
