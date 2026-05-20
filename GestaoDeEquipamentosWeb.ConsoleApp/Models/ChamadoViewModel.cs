using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Models;

public record ListarChamadosViewModel(
string Id,
string Titulo,
DateTime DataAbertura,
bool EstaConcluido,
int TempoDecorrido,
string Equipamento);

public record CadastrarChamadoViewModel(
    [Required(ErrorMessage ="O campo Titulo deve ser preenchido!")]
    [StringLength(50,MinimumLength =2, ErrorMessage ="O campo Titulo deve conter entre 2 e 50 caracteres")]
    string Titulo,

    [StringLength(500 , ErrorMessage ="O campo Descrição deve conter no maximo 500 caracteres")]
    string? Descricao,

    [Required(ErrorMessage ="O campo Equipamento deve ser preenchido")]
    string EquipamentoId
);

public record EditarChamadoViewModel(
    string Id,

      [Required(ErrorMessage ="O campo Titulo deve ser preenchido!")]
    [StringLength(50,MinimumLength =2, ErrorMessage ="O campo Titulo deve conter entre 2 e 50 caracteres")]
    string Titulo,

    [StringLength(500 , ErrorMessage ="O campo Descrição deve conter no maximo 500 caracteres")]
    string? Descricao,

    [Required(ErrorMessage ="O campo Equipamento deve ser preenchido")]
    string EquipamentoId,

    bool EstaConcluido
);

public record ExcluirChamadoViewModel(
    string Id,
    string Titulo,
    string Descricao,
    string Equipamento
);