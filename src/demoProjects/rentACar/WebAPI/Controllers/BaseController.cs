using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //cqrs kullandığımız için mediatr kullanıyoruz bu yüzden controller'da mediatr ihtiyacımız var. yapılan şey şu; mediatr varsa onu döndür yoksa gidip servislerden imediatr çöz ve bana bunu dön anlamına geliyor.
    public class BaseController : ControllerBase
    {
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator? _mediator;


    }
}
