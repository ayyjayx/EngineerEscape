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

    [System.Serializable]
    public class EquationSides
    {
        public EquationList left;
        public EquationList right;

        public List<EquationData> GetListForChosenSide(string side, int difficultyLevel)
        {
            return side switch
            {
                "left" => left.GetList(difficultyLevel),
                "right" => right.GetList(difficultyLevel),
                _ => null,
            };
        }
    }

    public EquationSides equationsSides = new();

    void Start()
    {
        equationsSides = JsonUtility.FromJson<EquationSides>(equationsJsonFile.text);
    }

    public int GetEquationsListCount(string side, int difficultyLevel)
    {
        return equationsSides.GetListForChosenSide(side, difficultyLevel).Count;
    }

    public EquationData GetEquation(string side, int difficultyLevel, int index)
    {
        List<EquationData> equationsForChosenSideAndDifficulty = equationsSides.GetListForChosenSide(side, difficultyLevel);

        if (equationsForChosenSideAndDifficulty != null && index < equationsForChosenSideAndDifficulty.Count)
        {
            EquationData chosenEquation = equationsForChosenSideAndDifficulty[index];
            return chosenEquation;
        }
        return null;
    }
}
