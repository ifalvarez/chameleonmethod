using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TimbaTools
{
    public class TimbaTools : Editor
    {
        [MenuItem("Timba/Create Parent &P")]
        static void CreateParent()
        {
            if (Application.isEditor)
            {
                if (Selection.transforms.Length > 0)
                {
                    Transform[] gos = Selection.transforms;
                    float minX = Mathf.Infinity;
                    float minY = Mathf.Infinity;
                    float minZ = Mathf.Infinity;
                    float maxX = -Mathf.Infinity;
                    float maxY = -Mathf.Infinity;
                    float maxZ = -Mathf.Infinity;
                    for (int i = 0; i < gos.Length; i++)
                    {
                        if (minX > gos[i].position.x)
                            minX = gos[i].position.x;

                        if (minY > gos[i].position.y)
                            minY = gos[i].position.y;

                        if (minZ > gos[i].position.z)
                            minZ = gos[i].position.z;

                        if (maxX < gos[i].position.x)
                            maxX = gos[i].position.x;

                        if (maxY < gos[i].position.y)
                            maxY = gos[i].position.y;

                        if (maxZ < gos[i].position.z)
                            maxZ = gos[i].position.z;
                    }

                    GameObject parent = new GameObject();
                    Undo.RegisterCreatedObjectUndo(parent, "Created Parent");
                    parent.name = "Parent" + gos[0].name;
                    Transform commonParent = gos[0].parent;
                    parent.transform.position = new Vector3(((maxX + minX) / 2), ((maxY + minY) / 2), ((maxZ + minZ) / 2));
                    parent.transform.parent = commonParent;

                    for (int i = 0; i < gos.Length; i++)
                    {
                        Undo.SetTransformParent(gos[i], parent.transform, "Create Parent");
                    }
                }
            }
        }

        [MenuItem("Timba/Create Multi Parent &#P")]
        static void CreateParentForEach()
        {
            if (Application.isEditor)
            {
                if (Selection.transforms.Length > 0)
                {
                    Transform[] gos = Selection.transforms;

                    for (int i = 0; i < gos.Length; i++)
                    {
                        GameObject parent = new GameObject();
                        Transform commonParent = gos[i].parent;
                        parent.transform.parent = commonParent;
                        Undo.RegisterCreatedObjectUndo(parent, "Created Parent");
                        parent.name = "Parent" + gos[i].name;
                        parent.transform.position = gos[i].transform.position;
                        Undo.SetTransformParent(gos[i], parent.transform, "Create Multi Parent");
                    }
                }
            }

        }


        [MenuItem("Timba/Rename Selected &R")]
        static void RenameSelected()
        {
            if (Application.isEditor)
            {
                if (Selection.transforms.Length > 0)
                {
                    GameObject firstSelected = Selection.activeGameObject;
                    string name = firstSelected.name;
                    Transform[] gos = Selection.GetTransforms(SelectionMode.Unfiltered);
                    int paddingAmount = (gos.Length).ToString().Length;
                    List<Transform> gosList = new List<Transform>(gos);
                    int currentIndex = 0;
                    while (gosList.Count > 0)
                    {
                        gos = Selection.GetTransforms(SelectionMode.TopLevel);
                        System.Array.Sort(gos, new SortHierarchyUnityObject());
                        for (int i = 0; i < gos.Length; i++)
                        {
                            Undo.RegisterCompleteObjectUndo(gos[i].gameObject, "Rename Object");
                            gos[i].name = name + ((currentIndex + 1)).ToString().PadLeft(paddingAmount, '0');
                            gosList.Remove(gos[i]);
                            currentIndex++;
                        }

                        for (int i = 0; i < gos.Length; i++)
                        {
                            gos[i].SetSiblingIndex(i);
                        }

                        Object[] newSelection = new Object[gosList.Count];

                        for (int i = 0; i < gosList.Count; i++)
                        {
                            newSelection[i] = gosList[i].gameObject;
                            Debug.Log(newSelection[i].name);
                        }

                        Selection.objects = newSelection;
                    }
                }
            }
        }

        [MenuItem("Timba/Alphabetinator &A")]
        static void SortAlphaNumerically()
        {
            if (Application.isEditor)
            {
                if (Selection.transforms.Length > 0)
                {
                    GameObject firstSelected = Selection.activeGameObject;
                    Transform[] gos = Selection.GetTransforms(SelectionMode.TopLevel);
                    System.Array.Sort(gos, new AlphaNumericSortUnityObject());
                    for (int i = 0; i < gos.Length; i++)
                    {
                        gos[i].SetSiblingIndex(i);
                    }
                }
            }
        }

        [MenuItem("Timba/Block Rotation &B")]
        static void BlockRotation()
        {
            if (Application.isEditor)
            {
                if (Selection.transforms.Length > 0)
                {
                    Transform[] gos = Selection.GetTransforms(SelectionMode.Unfiltered);
                    for (int i = 0; i < gos.Length; i++)
                    {
                        BlockRotation block = gos[i].gameObject.GetComponent<BlockRotation>();
                        if (block == null)
                        {
                            block = gos[i].gameObject.AddComponent<BlockRotation>();
                        }

                        block.space = Space.Self;
                        block.FreezeRotation = !block.FreezeRotation;
                    }
                }
            }
        }

        public class AlphaNumericSortUnityObject : IComparer<Object>
        {
            public int Compare(Object lhs, Object rhs)
            {
                if (lhs == rhs) return 0;
                if (lhs == null) return -1;
                if (rhs == null) return 1;

                return EditorUtility.NaturalCompare(lhs.name, rhs.name);
            }
        }


        public class SortHierarchyUnityObject : IComparer<Transform>
        {
            public int Compare(Transform lhs, Transform rhs)
            {
                if (lhs == rhs) return 0;
                if (lhs == null) return -1;
                if (rhs == null) return 1;

                return lhs.GetSiblingIndex().CompareTo(rhs.GetSiblingIndex());
            }
        }

        public class InverseSortHierarchyUnityObject : IComparer<Transform>
        {
            public int Compare(Transform lhs, Transform rhs)
            {
                if (lhs == rhs) return 0;
                if (lhs == null) return -1;
                if (rhs == null) return 1;

                return rhs.GetSiblingIndex().CompareTo(lhs.GetSiblingIndex());
            }
        }

    }
}

