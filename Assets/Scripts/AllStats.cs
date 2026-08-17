using System.Collections.Generic;
using UnityEngine;

public class AllStats : MonoBehaviour
{
    [SerializeField] private MatchEngine matchEng;

    [HideInInspector] public float posseT1, posseT2;
    [HideInInspector] public int passesT1, passesT2, passesCertosT1, passesCertosT2;
    [HideInInspector] public int GChancesT1, GChancesT2;

    [HideInInspector] public float chanceGolT1 = 0f, chanceGolT2 = 0f, chanceGol_GChances = 0f;
    [HideInInspector] public int ataquesTotaisT1, ataquesTotaisT2;
    [HideInInspector] public int totalChutesT1, totalChutesT2, chutesCertosT1, chutesCertosT2;
    [HideInInspector] public int desarmesT1, desarmesT2;
    [HideInInspector] public int faltasT1, faltasT2;
    [HideInInspector] public int cAmarelosT1, cAmarelosT2, cVermelhosT1, cVermelhosT2;
    [HideInInspector] public int escanteiosT1, escanteiosT2;

    public void setForcaGeral(Team team1, Team team2)
    {
        team1.forcaGeral = (int)(team1.forcaAtaque + team1.forcaMeio + team1.forcaDefesa) / 3;
        team2.forcaGeral = (int)(team2.forcaAtaque + team2.forcaMeio + team2.forcaDefesa) / 3;
    }
    public void getChanceDeGols(Team team1, Team team2)
    {
        // Esse calculo é ja considerando as finalizacoes ao gol, portanto a chance vai ser maior
        chanceGolT1 = (Mathf.Abs(team2.forcaDefesa - team1.forcaAtaque) * (1 + team1.bonus_chanceDeMarcar) * (1 + team2.bonus_chanceDeSofrerGol) / 100) * Random.Range(0.25f, 0.5f);
        chanceGolT2 = (Mathf.Abs(team1.forcaDefesa - team2.forcaAtaque) * (1 + team2.bonus_chanceDeMarcar) * (1 + team1.bonus_chanceDeSofrerGol) / 100) * Random.Range(0.25f, 0.5f);

        // Chance de marcar uma grande chance é maior
        chanceGol_GChances = Random.Range(0.3f, 0.6f);
    }
    public void getPosses(Team team1, Team team2)
    {
        float forcaAjustadaT1 = (team1.forcaMeio + (team1.forcaDefesa / 2)) * (1 + team1.bonus_posse);
        float forcaAjustadaT2 = (team2.forcaMeio + (team2.forcaDefesa / 2)) * (1 + team2.bonus_posse);

        // Calcula a posse de forma proporcional
        posseT1 = Mathf.Clamp(forcaAjustadaT1 / (forcaAjustadaT1 + forcaAjustadaT2), 0.15f, 0.85f);
        posseT2 = Mathf.Clamp(forcaAjustadaT2 / (forcaAjustadaT1 + forcaAjustadaT2), 0.15f, 0.85f);

        /*posseT1 = Mathf.Clamp(((team1.forcaMeio + (team1.forcaDefesa / 2)) * (1 + team1.bonus_posse)) / ((team1.forcaMeio + (team1.forcaDefesa / 2)) + (team2.forcaMeio + (team2.forcaDefesa / 2))), 0.15f, 0.85f);
        posseT2 = Mathf.Clamp(((team2.forcaMeio + (team2.forcaDefesa / 2)) * (1 + team2.bonus_posse)) / ((team2.forcaMeio + (team2.forcaDefesa / 2)) + (team1.forcaMeio + (team1.forcaDefesa / 2))), 0.15f, 0.85f);*/
    }
    public void getPasses(Team team1, Team team2)
    {
        int passesTotais = Random.Range(250, 950);

        // Passes totais dos times
        passesT1 = (int)((passesTotais * (1 + team1.bonus_passes)) * posseT1);
        passesT2 = (int)((passesTotais * (1 + team2.bonus_passes)) * posseT2);

        // Passes certos
        passesCertosT1 = (posseT1 > posseT2) 
            ? (int)(passesT1 * Random.Range(0.82f, 0.96f)) 
            : (int)(passesT1 * Random.Range(0.65f, 0.93f));
        passesCertosT2 = (posseT2 > posseT1) 
            ? (int)(passesT2 * Random.Range(0.82f, 0.96f)) 
            : (int)(passesT2 * Random.Range(0.65f, 0.93f));
    }
    public void getAtaquesTotais(Team team1, Team team2)
    {
        // Todas as oportunidades ofensivas, as vezes em que o time possui posse na area adversaria
        // Nao é exatamente isso mas da pra imaginar
        ataquesTotaisT1 = (int)(((team1.forcaGeral + team1.forcaAtaque * (1 + team1.bonus_ataquesTotais)) * posseT1) * Random.Range(0.25f, 0.5f));
        ataquesTotaisT2 = (int)(((team2.forcaGeral + team2.forcaAtaque * (1 + team2.bonus_ataquesTotais)) * posseT2) * Random.Range(0.25f, 0.5f));
    }
    public void getTotalChutes(Team team1, Team team2)
    {
        // Porcentagem de ataques resultando em finalizacao
        totalChutesT1 = (int)((ataquesTotaisT1 * (1 + team1.bonus_chutes)) * Random.Range(0.3f, 0.75f));
        totalChutesT2 = (int)((ataquesTotaisT2 * (1 + team2.bonus_chutes)) * Random.Range(0.3f, 0.75f));

        chutesCertosT1 = (int)(Mathf.Clamp((totalChutesT1 * (1 + team1.bonus_chutesNoGol)) * Random.Range(0.35f, 0.7f), 0, totalChutesT1));
        chutesCertosT2 = (int)(Mathf.Clamp((totalChutesT2 * (1 + team2.bonus_chutesNoGol)) * Random.Range(0.35f, 0.7f), 0, totalChutesT2));
    }
    public void getEscanteios()
    {
        escanteiosT1 = (int)(totalChutesT1 * Random.Range(0.25f, 0.45f));
        escanteiosT2 = (int)(totalChutesT2 * Random.Range(0.25f, 0.45f));
    }
    public void getGrandeChances(Team team1, Team team2)
    {
        // Arredondando para o inteiro mais proximo
        GChancesT1 = Mathf.RoundToInt((totalChutesT1 * (1 + team1.bonus_grandesChances)) * Random.Range(0f, 0.3f));
        GChancesT2 = Mathf.RoundToInt((totalChutesT2 * (1 + team2.bonus_grandesChances)) * Random.Range(0f, 0.3f));
    }
    public void getDesarmes(Team team1, Team team2)
    {
        desarmesT1 = (posseT1 < posseT2) 
            ? Mathf.Clamp((int)((((team1.forcaDefesa * (1 + team1.bonus_desarmes)) * posseT1) + (team2.forcaGeral / 2)) * Random.Range(0.05f, 0.25f)), 5, 28) 
            : Mathf.Clamp((int)(((team1.forcaDefesa * (1 + team1.bonus_desarmes)) * posseT1) * Random.Range(0.05f, 0.25f)), 5, 24);
        desarmesT2 = (posseT2 < posseT1) 
            ? Mathf.Clamp((int)((((team2.forcaDefesa * (1 + team2.bonus_desarmes)) * posseT2) + (team1.forcaGeral / 2)) * Random.Range(0.05f, 0.25f)), 5, 28) 
            : Mathf.Clamp((int)(((team2.forcaDefesa * (1 + team2.bonus_desarmes)) * posseT2) * Random.Range(0.05f, 0.25f)), 5, 24);
    }
    public void getFaltas(Team team1, Team team2)
    {
        faltasT1 = (posseT1 < posseT2) 
            ? (int)((((team1.forcaDefesa * (1 + team1.bonus_faltas)) + (desarmesT1 + team2.forcaGeral)) / 10) * Random.Range(1.01f, 1.2f)) 
            : (int)((((team1.forcaDefesa * (1 + team1.bonus_faltas)) + desarmesT1) / 10) * Random.Range(1.01f, 1.2f));
        faltasT2 = (posseT2 < posseT1) 
            ? (int)((((team2.forcaDefesa * (1 + team2.bonus_faltas)) + (desarmesT2 + team1.forcaGeral)) / 10) * Random.Range(1.01f, 1.2f)) 
            : (int)((((team2.forcaDefesa * (1 + team2.bonus_faltas)) + desarmesT2) / 10) * Random.Range(1.01f, 1.2f));
    }
    public void getCartoes(Team team1, Team team2)
    {
        cAmarelosT1 = 0; cAmarelosT2 = 0;
        cVermelhosT1 = 0; cVermelhosT2 = 0;

        // Verificando cartoes do time 1
        for (int i = 0; i < faltasT1; i++)
        {
            if (Random.value < (Random.Range(0.05f, 0.2f) * (1 + team1.bonus_cAmarelos)))
            {
                cAmarelosT1++;
            }
            else if (Random.value < (Random.Range(0.005f, 0.01f) * (1 + team1.bonus_cVermelhos)))
            {
                cVermelhosT1++;
                chanceGolT2 += 0.15f;
            }
        }
        // Verificando cartoes do time 2
        for (int i = 0; i < faltasT2; i++)
        {
            if (Random.value < (Random.Range(0.05f, 0.2f) * (1 + team2.bonus_cAmarelos)))
            {
                cAmarelosT2++;
            }
            else if (Random.value < (Random.Range(0.005f, 0.01f) * (1 + team2.bonus_cVermelhos)))
            {
                cVermelhosT2++;
                chanceGolT1 += 0.15f;
            }
        }
    }
}
