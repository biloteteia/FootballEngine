using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.MessageBox;

public class TacticsManager : MonoBehaviour
{
    [SerializeField] private AllStats allStats;

    public void AplicandoMentalidade(Mentality mental)
    {
        GameObject botaoClicado = EventSystem.current.currentSelectedGameObject;

        Team team1 = GameObject.FindGameObjectWithTag("Time1").GetComponent<Team>();
        Team team2 = GameObject.FindGameObjectWithTag("Time2").GetComponent<Team>();
        Team team = (botaoClicado.GetComponent<IDButtonTactics>().botoesTaticasT1) ? team1 : team2;

        if (team.ultimoMentalSelecionado)
        {
            team.bonus_desarmes -= team.ultimoMental.bonus_desarmes;
            team.bonus_faltas -= team.ultimoMental.bonus_faltas;
            team.bonus_cAmarelos -= team.ultimoMental.bonus_cAmarelos;
            team.bonus_cVermelhos -= team.ultimoMental.bonus_cvermelhos;
        }
        team.ultimoMentalSelecionado = true;
        team.ultimoMental = mental;

        team.bonus_desarmes += mental.bonus_desarmes;
        team.bonus_faltas += mental.bonus_faltas;
        team.bonus_cAmarelos += mental.bonus_cAmarelos;
        team.bonus_cVermelhos += mental.bonus_cvermelhos;
    }
    public void AplicandoTaticas(Styles style)
    {
        GameObject botaoClicado = EventSystem.current.currentSelectedGameObject;

        Team team1 = GameObject.FindGameObjectWithTag("Time1").GetComponent<Team>();
        Team team2 = GameObject.FindGameObjectWithTag("Time2").GetComponent<Team>();
        Team team = (botaoClicado.GetComponent<IDButtonTactics>().botoesTaticasT1) ? team1 : team2;

        if (team.ultimaTaticaSelecionada)
        {
            team.bonus_posse -= team.ultimaTatica.bonus_posse;
            team.bonus_passes -= team.ultimaTatica.bonus_passes;
            team.bonus_ataquesTotais -= team.ultimaTatica.bonus_ataquesTotais;
            team.bonus_chutes -= team.ultimaTatica.bonus_chutes;
            team.bonus_chutesNoGol -= team.ultimaTatica.bonus_chutesNoGol;
            team.bonus_grandesChances -= team.ultimaTatica.bonus_grandesChances;
            team.bonus_chanceDeSofrerGol -= team.ultimaTatica.bonus_chanceDeSofrerGol;
        }
        team.ultimaTaticaSelecionada = true;
        team.ultimaTatica = style;

        team.bonus_posse += style.bonus_posse;
        team.bonus_passes += style.bonus_passes;
        team.bonus_ataquesTotais += style.bonus_ataquesTotais;
        team.bonus_chutes += style.bonus_chutes;
        team.bonus_chutesNoGol += style.bonus_chutesNoGol;
        team.bonus_grandesChances += style.bonus_grandesChances;
        team.bonus_chanceDeSofrerGol += style.bonus_chanceDeSofrerGol;
    }
}
