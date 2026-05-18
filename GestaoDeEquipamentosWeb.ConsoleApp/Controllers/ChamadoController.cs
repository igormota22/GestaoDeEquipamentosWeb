using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers;

public class ChamadoController : Controller
{
    private readonly IRepositorio<Chamado> repositorioChamado;
    private readonly IRepositorio<Equipamento> repositorioEquipamento;
    public ChamadoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioChamado = new RepositorioChamadoEmArquivo(contexto);
        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
    }


    public ActionResult Listar()
    {
        List<Chamado> chamados = repositorioChamado.SelecionarTodos();

        List<ListarChamadosViewModel> listarvms = new List<ListarChamadosViewModel>();

        foreach (Chamado c in chamados)
        {
            ListarChamadosViewModel viewModel = new ListarChamadosViewModel(
                c.Id,
                c.Titulo,
                c.Descricao,
                c.Equipamento.Nome
            );

            listarvms.Add(viewModel);
        }


        return View(listarvms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Equipamentos = CarregarEquipamentos();
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVm)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(cadastrarVm.EquipamentoId);

        if (equipamento == null)
            return RedirectToAction(nameof(Listar));

        Chamado novoChamado = new Chamado(cadastrarVm.Titulo, equipamento, cadastrarVm.Descricao);
        repositorioChamado.Cadastrar(novoChamado);

        return RedirectToAction(nameof(Listar));

    }

    private List<ListarEquipamentosViewModel> CarregarEquipamentos()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();

        List<ListarEquipamentosViewModel> listarvms = new List<ListarEquipamentosViewModel>();

        foreach (Equipamento e in equipamentos)
        {
            ListarEquipamentosViewModel viewModel = new ListarEquipamentosViewModel(
                e.Id,
                e.Nome,
                e.PrecoAquisicao,
                e.DataFabricacao,
                e.Fabricante.Nome
            );

            listarvms.Add(viewModel);
        }

        return listarvms;
    }
}