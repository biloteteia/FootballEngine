using UnityEngine;
using UnityEngine.UIElements;

public class ButtonsManager : MonoBehaviour
{
    [Header("Botoes principais")]
    [SerializeField] private bool botaoSimular;
    [SerializeField] private bool botaoSimular100x;
    [SerializeField] private bool botaoEstatisticas;
    [SerializeField] private bool botaoAlltimeStats;
    [SerializeField] private bool botaoTaticas;
    [SerializeField] private bool botaoCasaFora;

    [Header("Botoes de voltar")]
    [SerializeField] private bool botaoVoltar_Cena1;
    [SerializeField] private bool botaoVoltar_Cena2;
    [SerializeField] private bool botaoVoltar_Cena3;
    [SerializeField] private bool botaoVoltar_Cena4;
    [SerializeField] private bool botaoVoltar_CenaTaticas;
    [SerializeField] private bool botaoVoltar_CenaInfoTaticas;
    [SerializeField] private bool botaoVoltar_CenaCasaFora;
    [SerializeField] private bool botaoVoltar_CenaErro;

    [Header("Outros")]
    [SerializeField] private bool botaoInfoTaticas;

    public void resetarHistorico()
    {
        UIManager.Instance.matchEng.vitoriasT1 = 0;
        UIManager.Instance.matchEng.empates = 0;
        UIManager.Instance.matchEng.vitoriasT2 = 0;

        UIManager.Instance.text_vitoriasT1.text = "0";
        UIManager.Instance.text_empatesT1.text = "0";
        UIManager.Instance.text_derrotasT1.text = "0";
        UIManager.Instance.text_vitoriasT2.text = "0";
        UIManager.Instance.text_empatesT2.text = "0";
        UIManager.Instance.text_derrotasT2.text = "0";
    }
    public void resetarEstatisticas()
    {
        UIManager.Instance.matchEng.vitoriasT1 = 0; 
        UIManager.Instance.matchEng.empates = 0; 
        UIManager.Instance.matchEng.vitoriasT2 = 0;
        UIManager.Instance.total_GolsMarcadosT1 = 0; UIManager.Instance.total_GolsMarcadosT2 = 0;
        UIManager.Instance.total_GChancesT1 = 0; UIManager.Instance.total_GChancesT2 = 0;
        UIManager.Instance.total_GChancesPT1 = 0; UIManager.Instance.total_GChancesPT2 = 0;
        UIManager.Instance.total_totalChutesT1 = 0; UIManager.Instance.total_totalChutesT2 = 0;
        UIManager.Instance.total_chutesNoGolT1 = 0; UIManager.Instance.total_chutesNoGolT2 = 0;
        UIManager.Instance.total_faltasT1 = 0; UIManager.Instance.total_faltasT2 = 0;
        UIManager.Instance.total_passesT1 = 0; UIManager.Instance.total_passesT2 = 0;
        UIManager.Instance.total_passesCertosT1 = 0; UIManager.Instance.total_passesCertosT2 = 0;
        UIManager.Instance.total_desarmesT1 = 0; UIManager.Instance.total_desarmesT2 = 0;
        UIManager.Instance.total_cAmarelosT1 = 0; UIManager.Instance.total_cAmarelosT2 = 0;
        UIManager.Instance.total_cVermelhosT1 = 0; UIManager.Instance.total_cVermelhosT2 = 0;
        UIManager.Instance.total_escanteiosT1 = 0; UIManager.Instance.total_escanteiosT2 = 0;
        UIManager.Instance.matchEng.listaDiferencaDeGols.Clear();
        UIManager.Instance.matchEng.listaTotalDeGols.Clear();
        UIManager.Instance.matchEng.listaGoleadas.Clear();

        UIManager.Instance.text_jogosTotais.text = $"JOGOS TOTAIS: 0";
        UIManager.Instance.text_golsMarcados.text = $"GOLS MARCADOS: 0 / 0";
        UIManager.Instance.text_golsSofridos.text = $"GOLS SOFRIDOS: 0 / 0";
        UIManager.Instance.text_gChances.text = $"GRANDES CHANCES: 0 / 0";
        UIManager.Instance.text_gChancesP.text = $"G. CHANCES PERDIDAS: 0 / 0";
        UIManager.Instance.text_totalChutes.text = $"TOTAL DE CHUTES: 0 / 0";
        UIManager.Instance.text_chutesNoGol.text = $"CHUTES NO GOL: 0 / 0";
        UIManager.Instance.text_faltas.text = $"FALTAS: 0 / 0";
        UIManager.Instance.text_passes.text = $"PASSES TOTAIS: 0 / 0";
        UIManager.Instance.text_passesCertos.text = $"PASSES CERTOS: 0 / 0";
        UIManager.Instance.text_desarmes.text = $"DESARMES: 0 / 0";
        UIManager.Instance.text_cAmarelos.text = $"AMARELOS: 0 / 0";
        UIManager.Instance.text_cVermelhos.text = $"VERMELHOS: 0 / 0";
        UIManager.Instance.text_escanteios.text = $"ESCANTEIOS: 0 / 0";
        UIManager.Instance.text_goleada1.text = $"Simule ao menos 3 jogos!";
        UIManager.Instance.text_goleada2.text = $"";
        UIManager.Instance.text_goleada3.text = $"";
        UIManager.Instance.text_melhorResultadoT1.text = $"Simule ao menos 1 jogo!";
        UIManager.Instance.text_melhorResultadoT2.text = $"";
    }
    public void onClickBotoesMain()
    {
        // Botao de simular da cena1
        if (GetComponent<ButtonsManager>().botaoSimular)
        {
            UIManager.Instance.atribuicaoINPUTS();

            UIManager.Instance.matchEng.simulacao();
            UIManager.Instance.atribuicaoALLTIMESTATS();

            UIManager.Instance.atribuicaoHISTORICO();
            UIManager.Instance.atribuicaoESTATISTICAS();
            
            UIManager.Instance.cena1.SetActive(false);
            UIManager.Instance.cena2.SetActive(true);
        }
        // Botao de simular 100x da cena1
        else if (GetComponent<ButtonsManager>().botaoSimular100x)
        {
            UIManager.Instance.atribuicaoINPUTS();

            for (int i = 0; i < 100; i++)
            {
                UIManager.Instance.matchEng.simulacao();
                UIManager.Instance.atribuicaoALLTIMESTATS();
            }

            UIManager.Instance.atribuicaoHISTORICO();
            UIManager.Instance.atribuicaoESTATISTICAS();

            UIManager.Instance.cena1.SetActive(false);
            UIManager.Instance.cena2.SetActive(true);
        }
        else if (GetComponent<ButtonsManager>().botaoTaticas)
        {
            UIManager.Instance.cenaTaticas.SetActive(true);
        }
        else if (GetComponent<ButtonsManager>().botaoInfoTaticas)
        {
            UIManager.Instance.cenaInfoTaticas.SetActive(true);
        }
        else if (GetComponent<ButtonsManager>().botaoCasaFora)
        {
            UIManager.Instance.cenaCasaFora.SetActive(true);
        }
        // Botao de ver as estatisticas da cena2
        else if (GetComponent<ButtonsManager>().botaoEstatisticas)
        {
            UIManager.Instance.cena2.SetActive(false);
            UIManager.Instance.cena3.SetActive(true);
        }
        else if (GetComponent<ButtonsManager>().botaoAlltimeStats)
        {
            UIManager.Instance.cena2.SetActive(false);
            UIManager.Instance.cena4.SetActive(true);
        }
    }
    public void onClickVoltar()
    {
        if (GetComponent<ButtonsManager>().botaoVoltar_Cena1)
        {
            UIManager.Instance.cena1.SetActive(false);
            UIManager.Instance.cena2.SetActive(true);
        }
        else if (GetComponent<ButtonsManager>().botaoVoltar_Cena2)
        {
            UIManager.Instance.cena2.SetActive(false);
            UIManager.Instance.cena1.SetActive(true);
        }
        else if (GetComponent<ButtonsManager>().botaoVoltar_Cena3)
        {
            UIManager.Instance.cena3.SetActive(false);
            UIManager.Instance.cena2.SetActive(true);
        }
        else if (GetComponent<ButtonsManager>().botaoVoltar_Cena4)
        {
            UIManager.Instance.cena4.SetActive(false);
            UIManager.Instance.cena2.SetActive(true);
        }
        else if (GetComponent<ButtonsManager>().botaoVoltar_CenaTaticas)
        {
            UIManager.Instance.cenaTaticas.SetActive(false);
        }
        else if (GetComponent<ButtonsManager>().botaoVoltar_CenaInfoTaticas)
        {
            UIManager.Instance.cenaInfoTaticas.SetActive(false);
        }
        else if (GetComponent<ButtonsManager>().botaoVoltar_CenaCasaFora)
        {
            UIManager.Instance.cenaCasaFora.SetActive(false);
        }
        else if (GetComponent<ButtonsManager>().botaoVoltar_CenaErro)
        {
            UIManager.Instance.cenaErro.SetActive(false);
        }
    }
}
