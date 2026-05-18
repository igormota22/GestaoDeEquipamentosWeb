using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers
{

    public class FabricanteController : Controller
    {
        private readonly IRepositorio<Fabricante> repositorioFabricante;
        public FabricanteController()
        {
            ContextoJson contexto = new ContextoJson();
            contexto.Carregar();

            repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
        }

        // GET: FabricanteController
        public ActionResult Listar()
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


            return View(listarvms);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarFabricanteViewModel cadastrarVm)
        {
            Fabricante novoFabricante = new Fabricante(cadastrarVm.Nome, cadastrarVm.Email, cadastrarVm.Telefone);

            repositorioFabricante.Cadastrar(novoFabricante);


            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult Editar(string id)
        {
            Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

            if (fabricante == null)
                return RedirectToAction(nameof(Listar));

            EditarFabricanteViewModel editarvms = new EditarFabricanteViewModel(id, fabricante.Nome, fabricante.Email, fabricante.Telefone);

            return View(editarvms);
        }

        [HttpPost]
        public ActionResult Editar(EditarFabricanteViewModel editarVm)
        {
            Fabricante fabricanteAtualizado = new Fabricante(editarVm.Nome, editarVm.Email, editarVm.Telefone);

            repositorioFabricante.Editar(editarVm.Id, fabricanteAtualizado);

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult Excluir(string id)
        {
            Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

            if (fabricante == null)
                return RedirectToAction(nameof(Listar));

            ExcluirFabricanteViewModel excluirvms = new ExcluirFabricanteViewModel(id, fabricante.Nome, fabricante.Email, fabricante.Telefone);
            return View(excluirvms);
        }

        [HttpPost]
        [ActionName("Excluir")]
        public ActionResult ExcluirConfirmado(ExcluirFabricanteViewModel excluir)
        {
            Fabricante? fabricante = repositorioFabricante.SelecionarPorId(excluir.Id);

            if (fabricante == null)
                return RedirectToAction(nameof(Listar));

            repositorioFabricante.Excluir(fabricante);

            return RedirectToAction(nameof(Listar));
        }
    }
}
