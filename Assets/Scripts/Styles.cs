using UnityEngine;

[CreateAssetMenu(fileName = "GameStyle")]
public class Styles : ScriptableObject
{
    // Possiveis upgrades ou downgrades das taticas
    public string estilo_nome;
    public float bonus_posse;
    public float bonus_passes;
    public float bonus_ataquesTotais;
    public float bonus_chutes;
    public float bonus_chutesNoGol;
    public float bonus_grandesChances;
    public float bonus_chanceDeMarcar;
    public float bonus_chanceDeSofrerGol;
}

/* para taticas:
Mentalidade
Agressiva - + faltas, + desarmes, + cartoes
Equilibrada - muda nada
Tranquila - - faltas, - desarmes, - cartoes

Taticas
Posse de bola — + posse, + passes certos
Contra-ataque — - posse, + grandes chances, + finalizacoes certas
Jogo direto — - posse, - passes, - passes certos, + finalizacoes
Padrao - muda nada
Pressão alta — + posse, + finalizacoes, + chance de sofrer gol
Retranca — -- posse, -- finalizacoes, -- chance de sofrer gol
Artilharia - ++ finalizacoes, - finalizacoes certas */