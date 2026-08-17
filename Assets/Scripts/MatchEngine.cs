using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchEngine : MonoBehaviour
{
    // Times importados
    [Header("Info dos times")]
    [SerializeField] private Team team1;
    [SerializeField] private Team team2;
    [SerializeField] private AllStats allstats;

    // Chances de gol por ataque independente do time
    [HideInInspector] public int golsT1, golsT2;
    [HideInInspector] public int vitoriasT1, empates, vitoriasT2;
    [HideInInspector] public int GChancesPerdidasT1, GChancesPerdidasT2;

    [HideInInspector] public string melhorResultadoT1, melhorResultadoT2;
    [HideInInspector] public List<int> listaDiferencaDeGols;
    [HideInInspector] public List<int> listaTotalDeGols;
    [HideInInspector] public List<string> listaGoleadas;

    [HideInInspector] public int diferencaDeGolT1 = 0, diferencaDeGolT2 = 0;
    [HideInInspector] public int totalDeGolsT1 = 0, totalDeGolsT2 = 0;

    public void simulacao()
    {
        golsT1 = 0; golsT2 = 0;
        GChancesPerdidasT1 = 0; GChancesPerdidasT2 = 0;

        allstats.setForcaGeral(team1, team2);
        allstats.getPosses(team1, team2);
        allstats.getPasses(team1, team2);
        allstats.getAtaquesTotais(team1, team2);
        allstats.getTotalChutes(team1, team2);
        allstats.getEscanteios();
        allstats.getGrandeChances(team1, team2);
        allstats.getDesarmes(team1, team2);
        allstats.getFaltas(team1, team2);
        allstats.getCartoes(team1, team2);
        allstats.getChanceDeGols(team1, team2);

        // Calculando os gols do time 1 (somente os chutes "normais")
        for (int i = 0; i < allstats.chutesCertosT1 - allstats.GChancesT1; i++)
        {
            if (Random.value < allstats.chanceGolT1)
            {
                golsT1++;
            }
        }
        if (allstats.GChancesT1 != 0)
        {
            for (int j = 0; j < allstats.GChancesT1; j++)
            {
                if (Random.value < allstats.chanceGol_GChances)
                {
                    golsT1++;
                }
                else
                {
                    GChancesPerdidasT1++;
                }
            }
        }
        // Calculando os gols do time 2
        for (int i = 0; i < allstats.chutesCertosT2 - allstats.GChancesT2; i++)
        {
            if (Random.value < allstats.chanceGolT2)
            {
                golsT2++;
            }
        }
        if (allstats.GChancesT2 != 0)
        {
            for (int j = 0; j < allstats.GChancesT2; j++)
            {
                if (Random.value < allstats.chanceGol_GChances)
                {
                    golsT2++;
                }
                else
                {
                    GChancesPerdidasT2++;
                }
            }
        }
        UIManager.Instance.text_overT1.text = $"({team1.forcaGeral.ToString()})";
        UIManager.Instance.text_overT2.text = $"({team2.forcaGeral.ToString()})";
        UIManager.Instance.text_scores.text = $"{golsT1.ToString()} - {golsT2.ToString()}";

        getResultado();
        verificarResultados();
    }
    void getResultado()
    {
        // Definindo melhor resultado de ambos os times
        if ((vitoriasT1 + empates + vitoriasT2) == 0)
        {
            melhorResultadoT1 = $"- {team1.nome} {golsT1} - {golsT2} {team2.nome}";
            melhorResultadoT2 = $"{team1.nome} {golsT1} - {golsT2} {team2.nome} -";

            // Nao coloquei em modulo porque o valor negativo representa derrota
            diferencaDeGolT1 = golsT1 - golsT2;
            diferencaDeGolT2 = golsT2 - golsT1;
            totalDeGolsT1 = golsT1 + golsT2;
            totalDeGolsT2 = golsT1 + golsT2;
        }

        if (golsT1 > golsT2)
        {
            vitoriasT1++;

            if (diferencaDeGolT1 < golsT1 - golsT2)
            {
                melhorResultadoT1 = $"- {team1.nome} {golsT1} - {golsT2} {team2.nome}";

                diferencaDeGolT1 = golsT1 - golsT2;
                totalDeGolsT1 = golsT1 + golsT2;
            }
        }
        else if (golsT2 > golsT1)
        {
            vitoriasT2++;

            if (diferencaDeGolT2 < golsT2 - golsT1)
            {
                melhorResultadoT2 = $"{team1.nome} {golsT1} - {golsT2} {team2.nome} -";

                diferencaDeGolT2 = golsT2 - golsT1;
                totalDeGolsT2 = golsT1 + golsT2;
            }
        }
        else
        {
            empates++;

            if (diferencaDeGolT1 == 0) // Confirma que o melhor resultado é um empate
            {
                if (totalDeGolsT1 < golsT1 + golsT2)
                {
                    melhorResultadoT1 = $"- {team1.nome} {golsT1} - {golsT2} {team2.nome}";

                    diferencaDeGolT1 = golsT1 - golsT2;
                    totalDeGolsT1 = golsT1 + golsT2;
                }
            }
            if (diferencaDeGolT1 < 0) // Confirma que o melhor resultado é uma derrota
            {
                melhorResultadoT1 = $"- {team1.nome} {golsT1} - {golsT2} {team2.nome}";

                diferencaDeGolT1 = golsT1 - golsT2;
                totalDeGolsT1 = golsT1 + golsT2;
            }

            if (diferencaDeGolT2 == 0)
            {
                if (totalDeGolsT2 < golsT1 + golsT2)
                {
                    melhorResultadoT2 = $"{team1.nome} {golsT1} - {golsT2} {team2.nome} -";

                    diferencaDeGolT2 = golsT1 - golsT2;
                    totalDeGolsT2 = golsT1 + golsT2;
                }
            }
            if (diferencaDeGolT2 < 0)
            {
                melhorResultadoT2 = $"{team1.nome} {golsT1} - {golsT2} {team2.nome} -";

                diferencaDeGolT2 = golsT1 - golsT2;
                totalDeGolsT2 = golsT1 + golsT2;
            }
        }
    }

    void verificarResultados()
    {
        // Listando todos os melhores resultados
        if (listaGoleadas.Count == 0)
        {
            listaDiferencaDeGols.Add(Mathf.Abs(golsT1 - golsT2));
            listaTotalDeGols.Add(golsT1 + golsT2);
            listaGoleadas.Add($"- {team1.nome} {golsT1} - {golsT2} {team2.nome}");
        }
        else
        {
            for (int i = 0; i < listaDiferencaDeGols.Count; i++)
            {
                // Se for maior que a diferenca anterior, entra na frente
                if (Mathf.Abs(golsT1 - golsT2) > listaDiferencaDeGols[i]) 
                {
                    listaDiferencaDeGols.Insert(i, Mathf.Abs(golsT1 - golsT2));
                    listaTotalDeGols.Insert(i, golsT1 + golsT2);
                    listaGoleadas.Insert(i, $"- {team1.nome} {golsT1} - {golsT2} {team2.nome}");
                    break;
                }
                // Se for igual, verifica a quantidade de gols, e se for maior, passa para frente
                else if (Mathf.Abs(golsT1 - golsT2) == listaDiferencaDeGols[i] && (golsT1 + golsT2) > listaTotalDeGols[i])
                {
                    listaDiferencaDeGols.Insert(i, Mathf.Abs(golsT1 - golsT2));
                    listaTotalDeGols.Insert(i, golsT1 + golsT2);
                    listaGoleadas.Insert(i, $"- {team1.nome} {golsT1} - {golsT2} {team2.nome}");
                    break;
                }
                // Se nao é maior nem igual, é menor, entao fica por ultimo
                if (listaDiferencaDeGols.Count < 3)
                {
                    if (listaDiferencaDeGols[i] > Mathf.Abs(golsT1 - golsT2))
                    {
                        listaDiferencaDeGols.Add(Mathf.Abs(golsT1 - golsT2));
                        listaTotalDeGols.Add(golsT1 + golsT2);
                        listaGoleadas.Add($"- {team1.nome} {golsT1} - {golsT2} {team2.nome}");
                    }
                }
                // Quando a lista tiver igual a 4, remove o ultimo
                if (listaGoleadas.Count > 3)
                {
                    listaDiferencaDeGols.RemoveAt(listaDiferencaDeGols.Count - 1);
                    listaTotalDeGols.RemoveAt(listaTotalDeGols.Count - 1);
                    listaGoleadas.RemoveAt(listaGoleadas.Count - 1);
                    break;
                }
            }
        }
    }
    public string inspiracao(Team teamAtual)
    {
        int inspiracao = Random.Range(1, 101);

        if (inspiracao <= 22) // Dia ruim
        {
            teamAtual.forcaAtaque = Mathf.Clamp(teamAtual.forcaAtaque - 10, 1, 100);
            teamAtual.forcaMeio = Mathf.Clamp(teamAtual.forcaMeio - 10, 1, 100);
            teamAtual.forcaDefesa = Mathf.Clamp(teamAtual.forcaDefesa - 10, 1, 100);
            return "Abaixo";
        }
        else if (inspiracao <= 77) // Dia normal
        {
            return "Normal";
        }
        else // Dia inspirado
        {
            teamAtual.forcaAtaque = Mathf.Clamp(teamAtual.forcaAtaque + 10, 1, 100);
            teamAtual.forcaMeio = Mathf.Clamp(teamAtual.forcaMeio + 10, 1, 100);
            teamAtual.forcaDefesa = Mathf.Clamp(teamAtual.forcaDefesa + 10, 1, 100);
            return "Inspirado";
        }
    }
}
