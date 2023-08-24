using ChifrWithDB;
using Domain;
using Domain.Entity;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.DTO;

namespace Web.Controllers;
[ApiController]
[Route( "[controller]" )]
public class UserController : ControllerBase
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet( Name = "GetUser" )]
    public IEnumerable<User> Get()
    {
        //qdq
        return _userRepository.GetUsers();
    }

    [HttpPost( Name = "SaveUser" )]
    public IActionResult SaveUser( UserDto user )
    {

        Chifr chifr = new Chifr();

        user.message = chifr.Chifrator(user.message, user.bias);

        User domainUser = new User();
        domainUser.bias = user.bias;
        domainUser.message = user.message;
        _userRepository.AddUser( domainUser );
        _unitOfWork.Commit();

        return new OkResult();
    }
}
