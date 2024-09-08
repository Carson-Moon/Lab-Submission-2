using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.Client.Gui;

[CustomEditor(typeof(ObjectManager)), CanEditMultipleObjects]
public class ObjectManagerEditor : Editor
{
    private Color cachedCubeColor = Color.green;
    private Color cachedSphereColor = Color.green;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

#region Selection
        EditorGUILayout.BeginHorizontal();

        // Select our Cubes.
        if(GUILayout.Button("Select All Cubes"))
        {
            // Select all of our cubes!
            Selection.objects = FindAllCubes();
        }

        // Select our Spheres.
        if(GUILayout.Button("Select All Spheres"))
        {
            // Select all of our spheres!
            Selection.objects = FindAllSpheres();
        }

        EditorGUILayout.EndHorizontal();

        // Clear our selection.
        if(GUILayout.Button("Clear Selection"))
        {
            Selection.objects = null;
        }
#endregion
#region Enable/Disable
        EditorGUILayout.BeginHorizontal();

        GUI.backgroundColor = cachedCubeColor;

        // Enable/Disable our Cubes.
        if(GUILayout.Button("Enable/Disable all Cubes"))
        {
            GameObject[] allCubes = FindAllCubes();

            foreach(GameObject cube in allCubes)
            {
                cube.gameObject.SetActive(!cube.gameObject.activeSelf);

                if(cube.gameObject.activeSelf && cachedCubeColor != Color.green)
                {
                    cachedCubeColor = Color.green;
                }
                else if(!cube.gameObject.activeSelf && cachedCubeColor != Color.red)
                {
                    cachedCubeColor = Color.red;
                }
            }
        }

        GUI.backgroundColor = cachedSphereColor;

        // Enable/Disable our Spheres.
        if(GUILayout.Button("Enable/Disable all Spheres"))
        {
            GameObject[] allSpheres = FindAllSpheres();

            foreach(GameObject sphere in allSpheres)
            {
                sphere.gameObject.SetActive(!sphere.gameObject.activeSelf);

                if(sphere.gameObject.activeSelf && cachedSphereColor != Color.green)
                {
                    cachedSphereColor = Color.green;
                }
                else if(!sphere.gameObject.activeSelf && cachedSphereColor != Color.red)
                {
                    cachedSphereColor = Color.red;
                }
            }
        }

        EditorGUILayout.EndHorizontal();
#endregion
#region Cube and Sphere Size
        GUI.backgroundColor = Color.grey;

        // Grab our current targeted object manager.
        ObjectManager objManager = (ObjectManager)target;

        if(objManager.cubeSize < 0)
        {
            EditorGUILayout.HelpBox("Cube Size cannot be negative!", MessageType.Warning);
        }

        if(objManager.sphereRadius < 0)
        {
            EditorGUILayout.HelpBox("Sphere Radius cannot be negative!", MessageType.Warning);
        }

#endregion


    }

    private GameObject[] FindAllCubes(){
        // Find all cube behaviours and gameobjects attached to those behaviours.
        Cube[] allCubes = FindObjectsOfType<Cube>(true);
        GameObject[] allCubeObjects = allCubes.Select(cube => cube.gameObject).ToArray();

        return allCubeObjects;
    }

    private GameObject[] FindAllSpheres(){
        // Find all sphere behaviours and gameobjects attached to those behaviours.
        Sphere[] allSpheres = FindObjectsOfType<Sphere>(true);
        GameObject[] allSphereObjects = allSpheres.Select(sphere => sphere.gameObject).ToArray();

        return allSphereObjects;
    }
}
