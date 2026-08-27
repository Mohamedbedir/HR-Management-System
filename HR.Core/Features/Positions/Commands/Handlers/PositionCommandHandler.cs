using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Positions.Commands.Models;
using HR.Data.Entities;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Positions.Commands.Handlers
{
    public class PositionCommandHandler : ResponseHandler,
        IRequestHandler<AddPositionCommand, Response<string>>,
        IRequestHandler<EditPositionCommand, Response<string>>,
        IRequestHandler<DeletePositionCommand, Response<string>>
    {
        private readonly IPositionService positionService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public PositionCommandHandler(IPositionService positionService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer) 
        {
            this.positionService = positionService;
            this.mapper = mapper;
            this.localizer = localizer;
        }
        public async Task<Response<string>> Handle(AddPositionCommand request, CancellationToken cancellationToken)
        {
            var PosMapping = mapper.Map<Position>(request);
            var res = await positionService.CreatePositionAsync(PosMapping);
            
            if (res == "Success")
                return Created<string>("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var pos = await positionService.GetPositionByIdAsync(request.Id);

            if (pos == null)
                return NotFound<string>();

            var resdelete = await positionService.DeletePositionAsync(pos);

            if (resdelete != "Success")
                return BadRequest<string>();

            return Deleted<string>();
        }

        public async Task<Response<string>> Handle(EditPositionCommand request, CancellationToken cancellationToken)
        {
            var position = await positionService.GetPositionByIdAsync(request.Id);

            if (position == null)
                return NotFound<string>();

            position.Title = request.Title;
            position.Description=request.Description;
            position.MinSalary = request.MinSalary;
            position.MaxSalary=request.MaxSalary;   
            position.IsActive = request.IsActive;

            var resUpdate = await positionService.UpdatePositionAsync(position);

            if (resUpdate != "Success")
                return BadRequest<string>();

            return Updated<string>("");
        }
    }
}
