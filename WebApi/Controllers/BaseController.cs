using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController(IUnitOfWork context, IMapper mapper) : ControllerBase
    {
        protected readonly IUnitOfWork context = context;
       
        protected readonly IMapper mapper = mapper;
    }
}
