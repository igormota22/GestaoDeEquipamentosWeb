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

public record EditarChamadoViewModel(
    string Id,
    string Titulo,
    string Descricao,
    string EquipamentoId
);

public record ExcluirChamadoViewModel(
    string Id,
    string Titulo,
    string Descricao,
    string Equipamento
);