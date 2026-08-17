using UnityEngine;
using UnityEngine.EventSystems;

public class HomeAwayManager : MonoBehaviour
{
    [SerializeField] private Team team1, team2;

    // 0 = neutro, 1 = T1 casa, 2 = T2 casa
    [HideInInspector] public int ultimoSelecionado = 0;

    public void aplicandoBonusCasa()
    {
        if (ultimoSelecionado == 1)
        {
            team1.forcaAtaque = Mathf.Min(team1.forcaAtaque + 5, 100);
            team1.forcaMeio = Mathf.Min(team1.forcaMeio + 5, 100);
            team1.forcaDefesa = Mathf.Min(team1.forcaDefesa + 5, 100);
        }
        else if (ultimoSelecionado == 2)
        {
            team2.forcaAtaque = Mathf.Min(team2.forcaAtaque + 5, 100);
            team2.forcaMeio = Mathf.Min(team2.forcaMeio + 5, 100);
            team2.forcaDefesa = Mathf.Min(team2.forcaDefesa + 5, 100);
        }
    }

    public void casaOuFora()
    {
        GameObject botaoClicado = EventSystem.current.currentSelectedGameObject;

        if (botaoClicado.GetComponent<IDButtonHomeAway>().botaoT1)
        {
            ultimoSelecionado = 1;
            team1.casaOuForaT1 = "Casa";
            team2.casaOuForaT2 = "Fora";
        }
        else if (botaoClicado.GetComponent<IDButtonHomeAway>().botaoT2)
        {
            ultimoSelecionado = 2;
            team1.casaOuForaT1 = "Fora";
            team2.casaOuForaT2 = "Casa";
        }
        else
        {
            ultimoSelecionado = 0;
            team1.casaOuForaT1 = "Neutro";
            team2.casaOuForaT2 = "Neutro";
        }
    }
}
