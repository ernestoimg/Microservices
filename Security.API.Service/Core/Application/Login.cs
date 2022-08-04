using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Security.API.Service.Core.Context;
using Security.API.Service.Core.JWTLogic;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Security.API.Service.Core.Application
{
    public class Login
    {
        public class UsuarioLoginCommand : IRequest<UsuarioDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }

        public class UsuarioLoginValidator : AbstractValidator<UsuarioLoginCommand>
        {
            public UsuarioLoginValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UsuarioLoginHandler : IRequestHandler<UsuarioLoginCommand, UsuarioDto>
        {
            private readonly SeguridadContexto _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly SignInManager<Usuario> _signInManager;

            public UsuarioLoginHandler(SeguridadContexto context, UserManager<Usuario> userManager, IMapper mapper, IJwtGenerator jwtGenerator, SignInManager<Usuario> signInManager)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerator = jwtGenerator;
                _signInManager = signInManager;
            }
            public async Task<UsuarioDto> Handle(UsuarioLoginCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new Exception("El usuario no existe");
                }

                var signResult = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                if (signResult.Succeeded)
                {
                    var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);
                    usuarioDto.Token = _jwtGenerator.CreateToken(usuario);

                    return usuarioDto;
                }

                throw new Exception("Login incorrecto");
            }
        }
    }
}
