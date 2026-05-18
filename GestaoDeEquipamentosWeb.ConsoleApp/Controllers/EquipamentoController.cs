using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers
{

    public class EquipamentoController : Controller
    {
        private readonly IRepositorio<Equipamento> repositorioEquipamento;
        private readonly IRepositorio<Fabricante> repositorioFabricante;

        public EquipamentoController()
        {
            ContextoJson contexto = new ContextoJson();
            contexto.Carregar();

            repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
            repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
        }

        public ActionResult Listar()
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


            return View(listarvms);
        }


        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.Fabricantes = CarregarFabricantes();
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVm)
        {
            Fabricante? fabricante = repositorioFabricante.SelecionarPorId(cadastrarVm.FabricanteId);

            if (fabricante == null)
                return RedirectToAction(nameof(Listar));

            Equipamento novoEquipamento = new Equipamento(cadastrarVm.Nome, cadastrarVm.PrecoAquisicao, cadastrarVm.DataFabricacao, fabricante);

            repositorioEquipamento.Cadastrar(novoEquipamento);


            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult Editar(string id)
        {
            Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(id);

            if (equipamento == null)
                return RedirectToAction(nameof(Listar));

            EditarEquipamentoViewModel editarvms = new EditarEquipamentoViewModel(
            id,
            equipamento.Nome,
            equipamento.PrecoAquisicao,
            equipamento.DataFabricacao,
            equipamento.Fabricante.Id
            );

            ViewBag.fabricantes = CarregarFabricantes();

            return View(editarvms);
        }

        [HttpPost]
        public ActionResult Editar(EditarEquipamentoViewModel editarVm)
        {
            Fabricante? fabricante = repositorioFabricante.SelecionarPorId(editarVm.FabricanteId);

            if (fabricante == null)
                return RedirectToAction(nameof(Listar));

            Equipamento equipamentoAtualizado = new Equipamento(
                editarVm.Nome,
                editarVm.PrecoAquisicao,
                editarVm.DataFabricacao,
                fabricante);

            repositorioEquipamento.Editar(editarVm.Id, equipamentoAtualizado);

            return RedirectToAction(nameof(Listar));
        }

        private List<ListarFabricantesViewModel> CarregarFabricantes()
        {

            List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

            List<ListarFabricantesViewModel> listarvms = new List<ListarFabricantesViewModel>();

            foreach (Fabricante f in fabricantes)
            {
                ListarFabricantesViewModel viewModel = new ListarFabricantesViewModel(
                    f.Id,
                    f.Nome,
                    f.Email,
                    f.Telefone
                );

                listarvms.Add(viewModel);

            }

            return listarvms;
        }
    }

}