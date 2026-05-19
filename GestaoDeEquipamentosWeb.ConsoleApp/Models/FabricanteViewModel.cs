using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Models;

public record ListarFabricantesViewModel(
string Id,
string Nome,
string Email,
string Telefone);

public record CadastrarFabricanteViewModel(
[Required(ErrorMessage ="O campo Nome deve ser preenchido!")]
[StringLength(250, MinimumLength =3, ErrorMessage ="O nome deve ter entre 3 e 250 caracteres")]
string Nome,

[Required(ErrorMessage ="O campo Email deve ser preenchido!")]
string Email,

[Required(ErrorMessage ="O campo Telefone deve ser preenchido!")]
string Telefone);

public record EditarFabricanteViewModel(
string Id,

[Required(ErrorMessage ="O campo Nome deve ser preenchido!")]
[StringLength(250, MinimumLength =3, ErrorMessage ="O nome deve ter entre 3 e 250 caracteres")]
string Nome,

[Required(ErrorMessage ="O campo Email deve ser preenchido!")]
string Email,

[Required(ErrorMessage ="O campo Telefone deve ser preenchido!")]
string Telefone);

public record ExcluirFabricanteViewModel(
string Id,
string Nome,
string Email,
string Telefone);