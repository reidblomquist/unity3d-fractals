﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

	public GameObject target;
	public Vector3 offset;
	public float speed = 1f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 targetPosition = target.transform.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
	}
}
