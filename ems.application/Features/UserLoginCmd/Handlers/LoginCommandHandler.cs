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

namespace ems.application.Features.UserLoginCmd.Handlers
{

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly IUserLoginReopsitory _userLoginRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IUserLoginReopsitory userLoginRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userLoginRepository = userLoginRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Map LoginCommand to LoginRequestDTO
            var loginRequest = new LoginRequestDTO
            {
                LoginId = request.LoginId,
                Password = request.Password
            };

            // Fetch user details based on LoginId and Password
             var user = await _unitOfWork.UserLoginReopsitory.AuthenticateUser(loginRequest);
           // var user = await context.LoginCredentials.FirstOrDefaultAsync(u => u.LoginId == loginRequest.LoginId);


            if (user == null || user.Success == false)
            {
                return new LoginResponseDTO { Success = false, Message = "Invalid credentials" };
            }

            // Generate JWT token
            string token = GenerateJwtToken(user);

            return new LoginResponseDTO
            {
                Success = true,
                Token = token,
                Message = "Login successful"
            };
        }

        private string GenerateJwtToken(LoginResponseDTO user)
        {
            // JWT token generation logic goes here
            return "sdfsldfsld32423lffw32@#@sdfsdfsSDCSV@#$R@!13eWFSDCSCSDRGWERDWSF#@#@$@#$WDFSDVSDVJMSDJMFCASJMFMSDVMDSXVMCDSV";
        }
    }



}

