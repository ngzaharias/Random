using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.EventSystems;

public class DragPath : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private List<Vector3> m_waypoints = new List<Vector3>();
	private List<Vector3> m_pathNext = new List<Vector3>();
	private float m_pathDistanceCurrent = 0.0f;
	private float m_pathDistanceMax = 10.0f;

	IEnumerator MoveAlongPath()
	{
		const float distanceMin = 0.1f;
		for (int i = 0; i < m_waypoints.Count; ++i)
		{
			Vector3 positionNext = m_waypoints[i];
			while (Vector3.Distance(transform.position, positionNext) > distanceMin)
			{
				transform.position = Vector3.Lerp(transform.position, positionNext, 0.5f);
				yield return null;
			}
		}
	}

	public void OnBeginDrag(PointerEventData EventData)
	{
		m_pathDistanceCurrent = 0.0f;
		m_pathNext.Clear();
		Debug.Log("OnBeginDrag");
	}

	public void OnDrag(PointerEventData EventData)
	{
		const float distanceMin = 0.1f;

		int count = m_pathNext.Count;
		Vector3 positionLast = count > 0 ? m_pathNext[count-1] : transform.position;

		Vector3 positionCurrent = Camera.main.ScreenToWorldPoint(EventData.position);
		positionCurrent.z = transform.position.z;

		float distance = Vector3.Distance(positionLast, positionCurrent);
		if (distance > distanceMin
		&& m_pathDistanceCurrent < m_pathDistanceMax)
		{
			m_pathDistanceCurrent += distance;
			m_pathNext.Add(positionCurrent);
		}
	}

	public void OnEndDrag(PointerEventData EventData)
	{
		// stop pathing
		StopCoroutine(MoveAlongPath());

		// update the path
		m_waypoints = new List<Vector3>(m_pathNext);
		m_pathNext.Clear();

		// being pathing again
		StartCoroutine(MoveAlongPath());
		Debug.Log("OnEndDrag");
	}

}
