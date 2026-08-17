using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("Cenas do jogo")]
    public GameObject cena1;
    public GameObject cena2;
    public GameObject cena3;
    public GameObject cena4;
    public GameObject cenaTaticas;
    public GameObject cenaInfoTaticas;
    public GameObject cenaCasaFora;
    public GameObject cenaErro;

    [Header("Pegar info")]
    [SerializeField] private Team team1;
    [SerializeField] private Team team2;
    public MatchEngine matchEng;
    [SerializeField] private AllStats allstats;
    [SerializeField] private HomeAwayManager homeAway;

    [Header("Configuracao dos times")]
    public Text text_nomeT1;
    public Text text_nomeT2;
    public Text text_inspiracaoT1;
    public Text text_inspiracaoT2;

    [Header("Input dos atributos")]
    // Inputs do time 1
    [SerializeField] private InputField input_NOME1;
    [SerializeField] private InputField input_ATKT1;
    [SerializeField] private InputField input_MEIT1;
    [SerializeField] private InputField input_DEFT1;
    // Inputs do time 2
    [SerializeField] private InputField input_NOME2;
    [SerializeField] private InputField input_ATKT2;
    [SerializeField] private InputField input_MEIT2;
    [SerializeField] private InputField input_DEFT2;

    [Header("Gols, Rating e Historico")]
    public Text text_scores;
    public Text text_overT1;
    public Text text_overT2;
    public Text text_vitoriasT1;
    public Text text_empatesT1;
    public Text text_derrotasT1;
    public Text text_vitoriasT2;
    public Text text_empatesT2;
    public Text text_derrotasT2;
    public Text text_casaForaT1;
    public Text text_casaForaT2;

    [Header("Sliders da sessao de estatisticas")]
    [SerializeField] private Slider slider_posses;
    [SerializeField] private Slider slider_grandesChances;
    [SerializeField] private Slider slider_grandesChancesP;
    [SerializeField] private Slider slider_totalChutes;
    [SerializeField] private Slider slider_chutesNoGol;
    [SerializeField] private Slider slider_faltas;
    [SerializeField] private Slider slider_passes;
    [SerializeField] private Slider slider_passesCertos;
    [SerializeField] private Slider slider_desarmes;
    [SerializeField] private Slider slider_escanteios;

    [Header("Textos da sessao de estatisticas")]
    [SerializeField] private Text text_posseT1;
    [SerializeField] private Text text_posseT2;
    [SerializeField] private Text text_gChancesT1;
    [SerializeField] private Text text_gChancesT2;
    [SerializeField] private Text text_gChancesPT1;
    [SerializeField] private Text text_gChancesPT2;
    [SerializeField] private Text text_totalChutesT1;
    [SerializeField] private Text text_totalChutesT2;
    [SerializeField] private Text text_chutesNoGolT1;
    [SerializeField] private Text text_chutesNoGolT2;
    [SerializeField] private Text text_faltasT1;
    [SerializeField] private Text text_faltasT2;
    [SerializeField] private Text text_passesT1;
    [SerializeField] private Text text_passesT2;
    [SerializeField] private Text text_passesCertosT1;
    [SerializeField] private Text text_passesCertosT2;
    [SerializeField] private Text text_desarmesT1;
    [SerializeField] private Text text_desarmesT2;
    [SerializeField] private Text text_cAmarelosT1;
    [SerializeField] private Text text_cVermelhosT1;
    [SerializeField] private Text text_cAmarelosT2;
    [SerializeField] private Text text_cVermelhosT2;
    [SerializeField] private Text text_escanteiosT1;
    [SerializeField] private Text text_escanteiosT2;

    [Header("Textos da sessao de estatisticas ALL TIME")]
    public Text text_jogosTotais;
    public Text text_golsMarcados;
    public Text text_golsSofridos;
    public Text text_gChances;
    public Text text_gChancesP;
    public Text text_totalChutes;
    public Text text_chutesNoGol;
    public Text text_faltas;
    public Text text_passes;
    public Text text_passesCertos;
    public Text text_desarmes;
    public Text text_cAmarelos;
    public Text text_cVermelhos;
    public Text text_escanteios;
    public Text text_goleada1;
    public Text text_goleada2;
    public Text text_goleada3;
    public Text text_melhorResultadoT1;
    public Text text_melhorResultadoT2;

    [HideInInspector] public int total_GolsMarcadosT1 = 0, total_GolsMarcadosT2 = 0;
    [HideInInspector] public int total_GChancesT1 = 0, total_GChancesT2 = 0;
    [HideInInspector] public int total_GChancesPT1 = 0, total_GChancesPT2 = 0;
    [HideInInspector] public int total_totalChutesT1 = 0, total_totalChutesT2 = 0;
    [HideInInspector] public int total_chutesNoGolT1 = 0, total_chutesNoGolT2 = 0;
    [HideInInspector] public int total_faltasT1 = 0, total_faltasT2 = 0;
    [HideInInspector] public int total_passesT1 = 0, total_passesT2 = 0;
    [HideInInspector] public int total_passesCertosT1 = 0, total_passesCertosT2 = 0;
    [HideInInspector] public int total_desarmesT1 = 0, total_desarmesT2 = 0;
    [HideInInspector] public int total_cAmarelosT1 = 0, total_cAmarelosT2 = 0;
    [HideInInspector] public int total_cVermelhosT1 = 0, total_cVermelhosT2 = 0;
    [HideInInspector] public int total_escanteiosT1 = 0, total_escanteiosT2 = 0;

    public void atribuicaoINPUTS()
    {
        // Time 1
        team1.nome = input_NOME1.text;
        team1.forcaAtaque = int.Parse(input_ATKT1.text);
        team1.forcaMeio = int.Parse(input_MEIT1.text);
        team1.forcaDefesa = int.Parse(input_DEFT1.text);

        // Time 2
        team2.nome = input_NOME2.text;
        team2.forcaAtaque = int.Parse(input_ATKT2.text);
        team2.forcaMeio = int.Parse(input_MEIT2.text);
        team2.forcaDefesa = int.Parse(input_DEFT2.text);

        // Na cena 2, atribuindo os nomes e quem esta jogando em casa ou fora
        text_nomeT1.text = team1.nome;
        text_nomeT2.text = team2.nome;

        text_inspiracaoT1.text = $"{matchEng.inspiracao(team1)}";
        text_inspiracaoT2.text = $"{matchEng.inspiracao(team2)}";

        homeAway.aplicandoBonusCasa();

        text_casaForaT1.text = $"{team1.casaOuForaT1}";
        text_casaForaT2.text = $"{team2.casaOuForaT2}";
    }
    public void atribuicaoHISTORICO()
    {
        text_vitoriasT1.text = $"{matchEng.vitoriasT1}";
        text_empatesT1.text = $"{matchEng.empates}";
        text_derrotasT1.text = $"{matchEng.vitoriasT2}";
        text_vitoriasT2.text = $"{matchEng.vitoriasT2}";
        text_empatesT2.text = $"{matchEng.empates}";
        text_derrotasT2.text = $"{matchEng.vitoriasT1}";
    }
    public void atribuicaoESTATISTICAS()
    {
        // Primeiro os sliders
        slider_posses.maxValue = allstats.posseT1 + allstats.posseT2;
        slider_posses.value = allstats.posseT1;

        slider_grandesChances.maxValue = allstats.GChancesT1 + allstats.GChancesT2;
        slider_grandesChances.value = allstats.GChancesT1;

        slider_grandesChancesP.maxValue = matchEng.GChancesPerdidasT1 + matchEng.GChancesPerdidasT2;
        slider_grandesChancesP.value = matchEng.GChancesPerdidasT2;

        slider_totalChutes.maxValue = allstats.totalChutesT1 + allstats.totalChutesT2;
        slider_totalChutes.value = allstats.totalChutesT1;

        slider_chutesNoGol.maxValue = allstats.chutesCertosT1 + allstats.chutesCertosT2;
        slider_chutesNoGol.value = allstats.chutesCertosT1;

        slider_faltas.maxValue = allstats.faltasT1 + allstats.faltasT2;
        slider_faltas.value = allstats.faltasT1;

        slider_passes.maxValue = allstats.passesT1 + allstats.passesT2;
        slider_passes.value = allstats.passesT1;

        slider_passesCertos.maxValue = allstats.passesCertosT1 + allstats.passesCertosT2;
        slider_passesCertos.value = allstats.passesCertosT1;

        slider_desarmes.maxValue = allstats.desarmesT1 + allstats.desarmesT2;
        slider_desarmes.value = allstats.desarmesT1;

        slider_escanteios.maxValue = allstats.escanteiosT1 + allstats.escanteiosT2;
        slider_escanteios.value = allstats.escanteiosT1;

        // Agora os textos
        text_posseT1.text = $"{(allstats.posseT1 * 100).ToString("F2")}%";
        text_posseT2.text = $"{(allstats.posseT2 * 100).ToString("F2")}%";
        text_gChancesT1.text = $"{allstats.GChancesT1}";
        text_gChancesT2.text = $"{allstats.GChancesT2}";
        text_gChancesPT1.text = $"{matchEng.GChancesPerdidasT1}";
        text_gChancesPT2.text = $"{matchEng.GChancesPerdidasT2}";
        text_totalChutesT1.text = $"{allstats.totalChutesT1}";
        text_totalChutesT2.text = $"{allstats.totalChutesT2}";
        text_chutesNoGolT1.text = $"{allstats.chutesCertosT1}";
        text_chutesNoGolT2.text = $"{allstats.chutesCertosT2}";
        text_faltasT1.text = $"{allstats.faltasT1}";
        text_faltasT2.text = $"{allstats.faltasT2}";
        text_passesT1.text = $"{allstats.passesT1} ({((float)(allstats.passesCertosT1 / (float)allstats.passesT1) * 100).ToString("F2")}%)";
        text_passesT2.text = $"{allstats.passesT2} ({((float)(allstats.passesCertosT2 / (float)allstats.passesT2) * 100).ToString("F2")}%)";
        text_passesCertosT1.text = $"{allstats.passesCertosT1}";
        text_passesCertosT2.text = $"{allstats.passesCertosT2}";
        text_desarmesT1.text = $"{allstats.desarmesT1}";
        text_desarmesT2.text = $"{allstats.desarmesT2}";
        text_cAmarelosT1.text = $"AMARELOS - {allstats.cAmarelosT1}";
        text_cVermelhosT1.text = $"VERMELHOS - {allstats.cVermelhosT1}";
        text_cAmarelosT2.text = $"{allstats.cAmarelosT2} - AMARELOS";
        text_cVermelhosT2.text = $"{allstats.cVermelhosT2} - VERMELHOS";
        text_escanteiosT1.text = $"{allstats.escanteiosT1}";
        text_escanteiosT2.text = $"{allstats.escanteiosT2}";
    }
    public void atribuicaoALLTIMESTATS()
    {
        // Somando os valores para as estatisticas all time
        total_GolsMarcadosT1 += matchEng.golsT1; total_GolsMarcadosT2 += matchEng.golsT2;
        total_GChancesT1 += allstats.GChancesT1; total_GChancesT2 += allstats.GChancesT2;
        total_GChancesPT1 += matchEng.GChancesPerdidasT1; total_GChancesPT2 += matchEng.GChancesPerdidasT2;
        total_totalChutesT1 += allstats.totalChutesT1; total_totalChutesT2 += allstats.totalChutesT2;
        total_chutesNoGolT1 += allstats.chutesCertosT1; total_chutesNoGolT2 += allstats.chutesCertosT2;
        total_faltasT1 += allstats.faltasT1; total_faltasT2 += allstats.faltasT2;
        total_passesT1 += allstats.passesT1; total_passesT2 += allstats.passesT2;
        total_passesCertosT1 += allstats.passesCertosT1; total_passesCertosT2 += allstats.passesCertosT2;
        total_desarmesT1 += allstats.desarmesT1; total_desarmesT2 += allstats.desarmesT2;
        total_cAmarelosT1 += allstats.cAmarelosT1; total_cAmarelosT2 += allstats.cAmarelosT2;
        total_cVermelhosT1 += allstats.cVermelhosT1; total_cVermelhosT2 += allstats.cVermelhosT2;
        total_escanteiosT1 += allstats.escanteiosT1; total_escanteiosT2 += allstats.escanteiosT2;

        // Atribuindo
        text_jogosTotais.text = $"JOGOS TOTAIS: {matchEng.vitoriasT1 + matchEng.empates + matchEng.vitoriasT2}";
        text_golsMarcados.text = $"GOLS MARCADOS: {total_GolsMarcadosT1} / {total_GolsMarcadosT2}";
        text_golsSofridos.text = $"GOLS SOFRIDOS: {total_GolsMarcadosT2} / {total_GolsMarcadosT1}";
        text_gChances.text = $"GRANDES CHANCES: {total_GChancesT1} / {total_GChancesT2}";
        text_gChancesP.text = $"G. CHANCES PERDIDAS: {total_GChancesPT1} / {total_GChancesPT2}";
        text_totalChutes.text = $"TOTAL DE CHUTES: {total_totalChutesT1} / {total_totalChutesT2}";
        text_chutesNoGol.text = $"CHUTES NO GOL: {total_chutesNoGolT1} / {total_chutesNoGolT2}";
        text_faltas.text = $"FALTAS: {total_faltasT1} / {total_faltasT2}";
        text_passes.text = $"PASSES TOTAIS: {total_passesT1} / {total_passesT2}";
        text_passesCertos.text = $"PASSES CERTOS: {total_passesCertosT1} / {total_passesCertosT2}";
        text_desarmes.text = $"DESARMES: {total_desarmesT1} / {total_desarmesT2}";
        text_cAmarelos.text = $"AMARELOS: {total_cAmarelosT1} / {total_cAmarelosT2}";
        text_cVermelhos.text = $"VERMELHOS: {total_cVermelhosT1} / {total_cVermelhosT2}";
        text_escanteios.text = $"ESCANTEIOS: {total_escanteiosT1} / {total_escanteiosT2}";

        if (matchEng.listaGoleadas.Count < 3)
        {
            text_goleada1.text = "Simule ao menos 3 jogos!";
        }
        else
        {
            text_goleada1.text = $"{matchEng.listaGoleadas[0]}";
            text_goleada2.text = $"{matchEng.listaGoleadas[1]}";
            text_goleada3.text = $"{matchEng.listaGoleadas[2]}";
        }

        text_melhorResultadoT1.text = $"{matchEng.melhorResultadoT1}";
        text_melhorResultadoT2.text = $"{matchEng.melhorResultadoT2}";
    }
    public void gerarTimeAleatorio(Dropdown dropdown)
    {
        int opcao = dropdown.value;

        if (dropdown.GetComponent<IDDropdown>().primeiroDropdown)
        {
            string nomeTime = "Time 1";

            switch (opcao)
            {
                case 0: // Fraco
                    input_NOME1.text = $"{nomeTime}";
                    input_ATKT1.text = ((int)(Random.Range(20, 45))).ToString();
                    input_MEIT1.text = ((int)(Random.Range(20, 45))).ToString();
                    input_DEFT1.text = ((int)(Random.Range(20, 45))).ToString();
                    break;
                case 1: // Medio
                    input_NOME1.text = $"{nomeTime}";
                    input_ATKT1.text = ((int)(Random.Range(46, 64))).ToString();
                    input_MEIT1.text = ((int)(Random.Range(46, 64))).ToString();
                    input_DEFT1.text = ((int)(Random.Range(46, 64))).ToString();
                    break;
                case 2: // Forte
                    input_NOME1.text = $"{nomeTime}";
                    input_ATKT1.text = ((int)(Random.Range(65, 77))).ToString();
                    input_MEIT1.text = ((int)(Random.Range(65, 77))).ToString();
                    input_DEFT1.text = ((int)(Random.Range(65, 77))).ToString();
                    break;
                case 3: // Elite
                    input_NOME1.text = $"{nomeTime}";
                    input_ATKT1.text = ((int)(Random.Range(78, 87))).ToString();
                    input_MEIT1.text = ((int)(Random.Range(78, 87))).ToString();
                    input_DEFT1.text = ((int)(Random.Range(78, 87))).ToString();
                    break;
                case 4: // Lendario
                    input_NOME1.text = $"{nomeTime}";
                    input_ATKT1.text = ((int)(Random.Range(88, 96))).ToString();
                    input_MEIT1.text = ((int)(Random.Range(88, 96))).ToString();
                    input_DEFT1.text = ((int)(Random.Range(88, 96))).ToString();
                    break;
            }
        }
        else
        {
            string nomeTime = "Time 2";

            switch (opcao)
            {
                case 0: // Fraco
                    input_NOME2.text = $"{nomeTime}";
                    input_ATKT2.text = ((int)(Random.Range(20, 45))).ToString();
                    input_MEIT2.text = ((int)(Random.Range(20, 45))).ToString();
                    input_DEFT2.text = ((int)(Random.Range(20, 45))).ToString();
                    break;
                case 1: // Medio
                    input_NOME2.text = $"{nomeTime}";
                    input_ATKT2.text = ((int)(Random.Range(46, 64))).ToString();
                    input_MEIT2.text = ((int)(Random.Range(46, 64))).ToString();
                    input_DEFT2.text = ((int)(Random.Range(46, 64))).ToString();
                    break;
                case 2: // Forte
                    input_NOME2.text = $"{nomeTime}";
                    input_ATKT2.text = ((int)(Random.Range(65, 77))).ToString();
                    input_MEIT2.text = ((int)(Random.Range(65, 77))).ToString();
                    input_DEFT2.text = ((int)(Random.Range(65, 77))).ToString();
                    break;
                case 3: // Elite
                    input_NOME2.text = $"{nomeTime}";
                    input_ATKT2.text = ((int)(Random.Range(78, 87))).ToString();
                    input_MEIT2.text = ((int)(Random.Range(78, 87))).ToString();
                    input_DEFT2.text = ((int)(Random.Range(78, 87))).ToString();
                    break;
                case 4: // Lendario
                    input_NOME2.text = $"{nomeTime}";
                    input_ATKT2.text = ((int)(Random.Range(88, 96))).ToString();
                    input_MEIT2.text = ((int)(Random.Range(88, 96))).ToString();
                    input_DEFT2.text = ((int)(Random.Range(88, 96))).ToString();
                    break;
            }
        }
    }
    public void onEndEditInputs(InputField inputAtual)
    {
        if (inputAtual.tag == "InputTagNome")
        {
            if (inputAtual.text.Length < 1 || inputAtual.text.Length > 20)
            {
                cenaErro.SetActive(true);
                inputAtual.text = "";
            }
        }
        if (inputAtual.tag == "InputTagValor")
        {
            if (int.Parse(inputAtual.text) < 1 || int.Parse(inputAtual.text) > 100)
            {
                cenaErro.SetActive(true);
                inputAtual.text = "";
            }
        }
    }
}
