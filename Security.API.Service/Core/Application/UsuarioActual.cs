using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Security.API.Service.Core.JWTLogic;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Security.API.Service.Core.Application
{
    public class UsuarioActual
    {
        public class UsuarioActualCommand : IRequest<UsuarioDto> 
        {
            public string JWToken { get; set; }
        }

        public class UsuarioActualValidation : AbstractValidator<UsuarioActualCommand>
        {
            public UsuarioActualValidation()
            {
                RuleFor(x => x.JWToken).NotEmpty();
            }
        }

        public class UsuarioActualHandle : IRequestHandler<UsuarioActualCommand, UsuarioDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IMapper _imapper;

            public UsuarioActualHandle(UserManager<Usuario> userManager, IUsuarioSesion usuarioSesion, IJwtGenerator jwtGenerator, IMapper imapper)
            {
                _userManager = userManager;
                _usuarioSesion = usuarioSesion;
                _jwtGenerator = jwtGenerator;
                _imapper = imapper;
            }
            public async Task<UsuarioDto> Handle(UsuarioActualCommand request, CancellationToken cancellationToken)
            {
                
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.GetUserSession(request.JWToken));

                if (usuario != null)
                {
                    var usuarioDto = _imapper.Map<Usuario, UsuarioDto>(usuario);
                    usuarioDto.Token = request.JWToken;

                    return usuarioDto;
                }

                throw new Exception("No se encontró el usuario");
            }
        }


    }
}
