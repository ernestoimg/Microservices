using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.API.Service.Core.Context;
using Security.API.Service.Core.JWTLogic;
using Security.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Security.API.Service
{
    public class Register
    {
        public class UsuarioRegisterCommand : IRequest<UsuarioDto>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioRegisterValidation: AbstractValidator<UsuarioRegisterCommand>
        {
            public UsuarioRegisterValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UsuarioRegisterHandler : IRequestHandler<UsuarioRegisterCommand, UsuarioDto>
        {
            private readonly SeguridadContexto _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _jwtGenerator;

            public UsuarioRegisterHandler(SeguridadContexto context, UserManager<Usuario> userManager, IMapper mapper, IJwtGenerator jwtGenerator)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerator = jwtGenerator;
            }


            public async Task<UsuarioDto> Handle(UsuarioRegisterCommand request, CancellationToken cancellationToken)
            {
                var exist = await _context.Users.Where(x=>x.Email == request.Email).AnyAsync();

                if (exist)
                {
                    throw new Exception("El email del usuario ya existe en la base de datos");
                }

                exist = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();

                if (exist)
                {
                    throw new Exception("El username del usuario ya existe en la base de datos");
                }

                var usuario = new Usuario()
                {
                    Nombre=request.Nombre,
                    Apellido=request.Apellido,
                    Email=request.Email,
                    UserName=request.Username
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);

                if (resultado.Succeeded)
                {
                    
                    var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);
                    usuarioDto.Token = _jwtGenerator.CreateToken(usuario);
                    return usuarioDto;
                }

                throw new Exception("No se pudo registrar el usuario");
            }
        }
    }
}
