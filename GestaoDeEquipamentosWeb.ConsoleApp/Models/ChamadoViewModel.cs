namespace GestaoDeEquipamentosWeb.ConsoleApp.Models;

public record ListarChamadosViewModel(
string Id,
string Titulo,
string Descricao,
string Equipamento);

public record CadastrarChamadoViewModel(
    string Titulo,
    string Descricao,
    string EquipamentoId
);