using Backend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nucleo.Modelos;
using Nucleo.Seguridad;
using System.ComponentModel.DataAnnotations;
using www.Utilidades;

namespace www.Pages.Login;

public class RecuperarModel : PageModel
{
    private readonly IDatos _datos;

    public RecuperarModel(IDatos datos)
    {
        _datos = datos;
    }

    public string EMail { get; set; } = string.Empty;

    public int PinUsuario { get; set; }

    public bool PinValidado { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Debe introducir el resultado del cálculo del PIN.")]
    [Display(Name = "Resultado del cálculo")]
    public int? ResultadoCalculo { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Debe indicar la nueva contraseña.")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [Display(Name = "Nueva contraseña")]
    public string PasswordNueva { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Debe repetir la nueva contraseña.")]
    [DataType(DataType.Password)]
    [Compare(nameof(PasswordNueva), ErrorMessage = "Las nuevas contraseñas no coinciden.")]
    [Display(Name = "Repetir nueva contraseña")]
    public string ConfirmacionPassword { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        if (!CargarUsuario())
        {
            return RedirectToPage("/Login/Index");
        }

        PinValidado = HttpContext.Session.GetString(ClavesSesion.PinValidado) == "true";
        return Page();
    }

    public IActionResult OnPostValidarPin()
    {
        if (!CargarUsuario())
        {
            return RedirectToPage("/Login/Index");
        }

        PinValidado = false;
        HttpContext.Session.Remove(ClavesSesion.PinValidado);

        if (!ResultadoCalculo.HasValue)
        {
            ModelState.AddModelError(nameof(ResultadoCalculo), "Debe introducir el resultado del cálculo.");
            return Page();
        }

        Usuario? usuario = _datos.LeerUsuario(EMail);
        if (usuario is null)
        {
            return RedirectToPage("/Login/Index");
        }

        int resultadoEsperado = usuario.obtenerResultadoCalculoPin();
        if (ResultadoCalculo.Value != resultadoEsperado)
        {
            ModelState.AddModelError(nameof(ResultadoCalculo), "El resultado del cálculo no es correcto.");
            return Page();
        }

        PinValidado = true;
        HttpContext.Session.SetString(ClavesSesion.PinValidado, "true");
        return Page();
    }

    public IActionResult OnPostCambiarPassword()
    {
        if (!CargarUsuario())
        {
            return RedirectToPage("/Login/Index");
        }

        PinValidado = HttpContext.Session.GetString(ClavesSesion.PinValidado) == "true";
        if (!PinValidado)
        {
            ModelState.AddModelError(string.Empty, "Debe validar primero el PIN para cambiar la contraseña.");
            return Page();
        }

        // Quitar validación de ResultadoCalculo ya que ahora estamos cambiando la contraseña
        ModelState.Remove(nameof(ResultadoCalculo));

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!ServicioContrasenas.EsValida(PasswordNueva))
        {
            ModelState.AddModelError(
                nameof(PasswordNueva),
                "La nueva contraseña debe tener al menos 8 caracteres e incluir mayúsculas, minúsculas, números y al menos un carácter especial.");
            return Page();
        }

        Usuario? usuario = _datos.LeerUsuario(EMail);
        if (usuario is null)
        {
            return RedirectToPage("/Login/Index");
        }

        try
        {
            usuario.CambiaPassword(PasswordNueva);
            _datos.GuardarUsuario(usuario);

            _datos.AgregarLog(
                new EntradaLog(usuario, DateTime.Now, TipoEntradaLog.INFO, $"El usuario {usuario.Email} ha restablecido su contraseña tras validar el PIN."));

            HttpContext.Session.Remove(ClavesSesion.PinValidado);
            HttpContext.Session.Remove(ClavesSesion.EmailPendiente);

            TempData["MensajeExito"] = "Contraseña restablecida correctamente. Ya puedes iniciar sesión.";
            return RedirectToPage("/Login/Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }

    public IActionResult OnPostAtras()
    {
        HttpContext.Session.Remove(ClavesSesion.PinValidado);
        return RedirectToPage("/Login/Index2");
    }

    private bool CargarUsuario()
    {
        EMail = HttpContext.Session.GetString(ClavesSesion.EmailPendiente) ?? string.Empty;
        if (string.IsNullOrEmpty(EMail))
        {
            return false;
        }

        Usuario? usuario = _datos.LeerUsuario(EMail);
        if (usuario is null)
        {
            return false;
        }

        PinUsuario = usuario.Pin;
        return true;
    }
}
