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

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(id);

        if (chamado == null)
            return RedirectToAction(nameof(Listar));

        EditarChamadoViewModel editarVm = new EditarChamadoViewModel(
         id,
         chamado.Titulo,
         chamado.Descricao,
         chamado.Equipamento.Id
        );

        ViewBag.Equipamentos = CarregarEquipamentos();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarChamadoViewModel editarVm)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(editarVm.EquipamentoId);

        if (equipamento == null)
            return RedirectToAction(nameof(Listar));

        Chamado chamadoAtualizado = new Chamado(editarVm.Titulo, equipamento, editarVm.Descricao);

        repositorioChamado.Editar(editarVm.Id, chamadoAtualizado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(id);

        if (chamado == null)
            return RedirectToAction(nameof(Listar));

        ExcluirChamadoViewModel excluirvms = new ExcluirChamadoViewModel(
        id,
        chamado.Titulo,
        chamado.Descricao,
        chamado.Equipamento.Nome);
        return View(excluirvms);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmado(ExcluirChamadoViewModel excluirVm)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(excluirVm.Id);

        if (chamado != null)
            repositorioChamado.Excluir(chamado);

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