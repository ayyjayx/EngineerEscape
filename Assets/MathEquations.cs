using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MathEquations : MonoBehaviour
{
    [SerializeField] TextAsset equationsJsonFile;

    [System.Serializable]
    public class EquationData
    {
        public string equation;
        public int solution;
    }
    
    [System.Serializable]
    public class EquationList
    {
        public EquationData[] easy;
        public EquationData[] medium;
        public EquationData[] hard;

        public List<EquationData> GetList(int difficultyLevel)
        {
            return difficultyLevel switch
            {
                1 => easy.ToList(),
                2 => medium.ToList(),
                3 => hard.ToList(),
                _ => null,
            };
        }
    }

    public EquationList equationsList = new();

    void Start()
    {
        equationsList = JsonUtility.FromJson<EquationList>(equationsJsonFile.text);
    }

    public int GetEquationsListCount(int difficultyLevel)
    {
        return equationsList.GetList(difficultyLevel).Count;
    }

    public EquationData GetEquation(int difficultyLevel, int index)
    {
        List<EquationData> equationsForChosenDifficulty = equationsList.GetList(difficultyLevel);

        if (equationsForChosenDifficulty != null && index < equationsForChosenDifficulty.Count)
        {
            EquationData chosenEquation = equationsForChosenDifficulty[index];
            return chosenEquation;
        }
        return null;
    }
}
