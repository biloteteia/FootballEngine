using UnityEngine;

[CreateAssetMenu(fileName = "Mentality")]
public class Mentality : ScriptableObject
{
    // Possiveis upgrades ou downgrades na mentalidade
    public string mental_nome;
    public float bonus_desarmes;
    public float bonus_faltas;
    public float bonus_cAmarelos;
    public float bonus_cvermelhos;
}
