using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public ActionResult Listar(string status)
    {
        List<Chamado> chamados;

        if (string.IsNullOrEmpty(status))
        {
            chamados = repositorioChamado.SelecionarTodos();
            ViewBag.Status = "todos";
        }
        else if (status.ToLower() == "em-aberto")
        {
            chamados = repositorioChamado.Filtrar(c => !c.EstaConcluido);
            ViewBag.Status = "em-aberto";
        }
        else if (status.ToLower() == "concluidos")
        {
            chamados = repositorioChamado.Filtrar(c => c.EstaConcluido);
            ViewBag.Status = "concluidos";
        }
        else
        {
            chamados = repositorioChamado.SelecionarTodos();
            ViewBag.Status = "Todos";
        }

        List<ListarChamadosViewModel> listarvms = new List<ListarChamadosViewModel>();

        foreach (Chamado c in chamados)
        {
            ListarChamadosViewModel viewModel = new ListarChamadosViewModel(
                c.Id,
                c.Titulo,
                c.DataAbertura,
                c.EstaConcluido,
                c.TempoDecorrido,
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
        CadastrarChamadoViewModel cadastrarVm = new CadastrarChamadoViewModel(string.Empty, null, string.Empty);
        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVm)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(cadastrarVm.EquipamentoId);

        if (equipamento == null)
            ModelState.AddModelError(
                nameof(cadastrarVm.EquipamentoId),
                "Selecione um id valido"
            );

        if (!ModelState.IsValid)
        {
            ViewBag.Equipamentos = CarregarEquipamentos();
            return View(cadastrarVm);
        }

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
         chamado.Equipamento.Id,
         chamado.EstaConcluido
        );

        ViewBag.Equipamentos = CarregarEquipamentos();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarChamadoViewModel editarVm)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(editarVm.EquipamentoId);

        if (equipamento == null)
            ModelState.AddModelError(
                nameof(editarVm.EquipamentoId),
                "Selecione um id valido"
            );

        if (!ModelState.IsValid)
        {
            ViewBag.Equipamentos = CarregarEquipamentos();
            return View(editarVm);
        }

        Chamado chamadoAtualizado = new Chamado(editarVm.Titulo, equipamento, editarVm.EstaConcluido, editarVm.Descricao);
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

    private List<SelectListItem> CarregarEquipamentos()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();

        List<SelectListItem> listarvms = new List<SelectListItem>();

        foreach (Equipamento e in equipamentos)
        {
            SelectListItem viewModel = new SelectListItem(
                e.Nome,
                e.Id
            );

            listarvms.Add(viewModel);
        }

        return listarvms;
    }
}