using ems.application.DTOs.UserLogin;
using ems.application.Features.UserLoginCmd.Commands;
using ems.application.Interfaces;
using ems.domain.Entity.UserCredential;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.Interfaces.IRepositories;
using AutoMapper;
using ems.application.Interfaces.ITokenService;
using ems.domain.Entity.CommonMenu;
using ems.application.DTOs.CommonAndRoleBaseMenu;
using ems.application.DTOs.RoleDTO;
using ems.domain.Entity.UserRoleModule;

namespace ems.application.Features.UserLoginCmd.Handlers
{

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly IUserLoginReopsitory _userLoginRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUserLoginReopsitory userLoginRepository, IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _userLoginRepository = userLoginRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Map LoginCommand to LoginRequestDTO
            var loginRequest = new LoginRequestDTO
            {
                LoginId = request.RequestLoginDTO.LoginId,
                Password = request.RequestLoginDTO.Password
            };

            // Fetch user details based on LoginId and Password
             var user = await _unitOfWork.UserLoginReopsitory.AuthenticateUser(loginRequest);
           // var user = await context.LoginCredentials.FirstOrDefaultAsync(u => u.LoginId == loginRequest.LoginId);


            if (user == null || user.Success == false)
            {
                return new LoginResponseDTO { Success = false, Message = "Invalid credentials" };
            }


            // Generate JWT token
            string token = GenerateJwtToken(loginRequest);
            // Fetch the CommonMenus based on the user or role (you can filter it according to your business logic)
            var commonMenus = await _unitOfWork.CommonMenuRepository.GetMenusByUserAndDeviceAsync(1, 2);
            var commonMenuDTOs = _mapper.Map<List<CommonMenuDTO>>(commonMenus);

            var userRoll = await _unitOfWork.UserRoleRepository.GetUsersRoleByIdAsync(1);
            
            var roles = await _unitOfWork.RoleRepository.GetRoleByIdAsync(1);
            
           // var rolesDto = _mapper.Map<List<GetRoleByIdDTO>>(roles);
            var rolesDto = _mapper.Map<GetRoleByIdDTO>(roles); // Change List<GetRoleByIdDTO> to GetRoleByIdDTO

            return new LoginResponseDTO
            {
                Success = true,
                Token = token,
                Message = "Login successful",
                CommonMenus = commonMenuDTOs,
                UserRole = rolesDto
            };
        }

        private string GenerateJwtToken(LoginRequestDTO user)
        {
            // JWT token generation logic goes here
            var token = _tokenService.GenerateToken(user);


                return token;
        }
    }



}

