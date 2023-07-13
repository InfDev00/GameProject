using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CodeSO", menuName = "GameProject/MemberCodeSO", order = 0)]
public class MemberCodeSO : ScriptableObject {

    [System.Serializable]
    public struct Set
    {
        public string name;
        public int cooperationPoint;
        public int preperationPoint;
        public int powerPoint;
        public string item;
    }

    public Set[] code;
}
