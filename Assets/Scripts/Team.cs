using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Team : MonoBehaviour
{
    // Atributos dos times
    public string nome;
    public float forcaAtaque;
    public float forcaMeio;
    public float forcaDefesa;
    public float forcaGeral;
    public bool time1;

    // Decidindo se esta jogando em casa ou fora
    public string casaOuForaT1;
    public string casaOuForaT2;
    
    // Bonus das taticas
    [HideInInspector] public float bonus_desarmes;
    [HideInInspector] public float bonus_faltas;
    [HideInInspector] public float bonus_cAmarelos;
    [HideInInspector] public float bonus_cVermelhos;

    [HideInInspector] public float bonus_posse;
    [HideInInspector] public float bonus_passes;
    [HideInInspector] public float bonus_ataquesTotais;
    [HideInInspector] public float bonus_chutes;
    [HideInInspector] public float bonus_chutesNoGol;
    [HideInInspector] public float bonus_grandesChances;
    [HideInInspector] public float bonus_chanceDeMarcar;
    [HideInInspector] public float bonus_chanceDeSofrerGol;

    // Salvando as taticas atuais e as ultimas
    public bool ultimoMentalSelecionado;
    public Mentality ultimoMental;
    public bool ultimaTaticaSelecionada;
    public Styles ultimaTatica;
}
