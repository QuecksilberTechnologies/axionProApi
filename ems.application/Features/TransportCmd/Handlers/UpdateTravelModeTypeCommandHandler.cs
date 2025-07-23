﻿using AutoMapper;
using ems.application.Features.TransportCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.DTOs.Transport;

namespace ems.application.Features.TransportCmd.Handlers
{
    public class UpdateTravelModeTypeCommandHandler : IRequestHandler<UpdateTravelModeTypeCommand, ApiResponse<List<GetAllTravelModeDTO>>>
    {
        private readonly ITravelRepository travelRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTravelModeTypeCommandHandler(ITravelRepository travelRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.travelRepository = travelRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<List<GetAllTravelModeDTO>>> Handle(UpdateTravelModeTypeCommand request, CancellationToken cancellationToken)
        {
            
            try
            {

                TravelMode travelMode = _mapper.Map<TravelMode>(request.updateTravelModeDTO);
                List<TravelMode> travelModes = await travelRepository.UpdateClientTypeAsync(travelMode);

                if (travelModes == null || !travelModes.Any())
                {
                    return new ApiResponse<List<GetAllTravelModeDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No Travel were created.",
                        Data = new List<GetAllTravelModeDTO>()
                    };
                }

                List<GetAllTravelModeDTO> getAllTravelModeDTOs = _mapper.Map<List<GetAllTravelModeDTO>>(travelModes);

                return new ApiResponse<List<GetAllTravelModeDTO>>
                {
                    IsSucceeded = true,
                    Message = "Travel created successfully",
                    Data = getAllTravelModeDTOs
                };
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, "Error occurred while creating role.");
                return new ApiResponse<List<GetAllTravelModeDTO>>

                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

       
    }
    }