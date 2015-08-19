using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fractal : MonoBehaviour {

	public Mesh mesh;
	public Material material;
	public int maxDepth;
	public float childScale;

	private int depth;
	private Material[,] materials;
	private static int childCount;
	private static int wat;

	private static Vector3[] childDirections = {
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back
	};

	private static List<int> childThresholds = new List<int> { };

	private static Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler (0f, 0f, -90f),
		Quaternion.Euler (0f, 0f, 90f),
		Quaternion.Euler (90f, 0f, 0f),
		Quaternion.Euler (-90f, 0f, 0f)
	};

	private IEnumerator CreateChildren () {
		for (int i = 0; i < childDirections.Length; i++) {
			yield return new WaitForSeconds(Random.Range (0.1f, 0.5f));
			new GameObject("Fractal Child").AddComponent<Fractal>().Initialize (this, i);
		}
	}

	private void Initialize (Fractal parent, int childIndex) {
		childCount++;
		int index = childThresholds.BinarySearch(childCount);
		int highest = index < 0 ? ~index : index;
		int result = childThresholds[highest];
		mesh = parent.mesh;
		materials = parent.materials;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation = childOrientations[childIndex];
	}

	private void InitializeMaterials () {
		materials = new Material[maxDepth + 1, 2];
		for (int i = 0; i <= maxDepth; i++) {
			float t = i / (maxDepth - 1f);
			t *= t;
			materials[i, 0] = new Material(material);
			materials[i, 0].color = Color.Lerp (Color.white, Color.yellow, t);
			materials[i, 1] = new Material(material);
			materials[i, 1].color = Color.Lerp (Color.white, Color.cyan, t);
		}
		materials[maxDepth, 0].color = Color.magenta;
		materials[maxDepth, 1].color = Color.red;
	}

	private void InitializeThreshholds () {
		for (int i = 0; i < (childDirections.Length + 1); i++) {
			if (i == 0) {
				childThresholds.Insert(i, 1);
			} else {
				childThresholds.Insert(i, (5 * childThresholds[i - 1] + 1));
			}
		}
	}

	private void Start () {
		if (materials == null) {
			InitializeMaterials();
		}
		if (childThresholds.Count == 0)
		{
			InitializeThreshholds();
		}
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range (0, 2)];

		if (depth < maxDepth) {
			StartCoroutine(CreateChildren());
		}
	}
}
