using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Models;

public record ListarEquipamentosViewModel(
string Id,
string Nome,
decimal PrecoAquisicao,
DateTime DataFabricacao,
string Fabricante);

public record CadastrarEquipamentoViewModel(
[Required(ErrorMessage ="O campo nome deve ser preenchido!")]
[StringLength(250, MinimumLength =3, ErrorMessage ="O campo nome deve conter entre 3 e 250 caracteres")]
string Nome,

[Required(ErrorMessage ="O campo Preço de Aquisição deve ser preenchido!")]
[Range(0.01,999999.99,ErrorMessage ="O campo preço de aquisição deve ser maior que zero")]
decimal PrecoAquisicao,

DateTime DataFabricacao,

[Required(ErrorMessage ="O campo Fabricante deve ser preenchido!")]
string FabricanteId);

public record EditarEquipamentoViewModel(
string Id,
[Required(ErrorMessage ="O campo nome deve ser preenchido!")]
[StringLength(250, MinimumLength =3, ErrorMessage ="O campo nome deve conter entre 3 e 250 caracteres")]
string Nome,

[Required(ErrorMessage ="O campo Preço de Aquisição deve ser preenchido!")]
[Range(0.01,999999.99,ErrorMessage ="O campo preço de aquisição deve ser maior que zero")]
decimal PrecoAquisicao,

DateTime DataFabricacao,

[Required(ErrorMessage ="O campo Fabricante deve ser preenchido!")]
string FabricanteId);


public record ExcluirEquipamentoViewModel(
    string Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string Fabricante
);