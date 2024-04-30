using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.UpdateCompetencyGroup
{
    public class UpdateCompetencyGroupCommandHandler : IRequestHandler<UpdateCompetencyGroupCommand, long>
    {
        private readonly ICompetencyGroupRepository _competencyGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCompetencyGroupCommandHandler> _logger;
        private readonly ICompetencyLevelRepository _competencyLevelRepository;
       
        public UpdateCompetencyGroupCommandHandler(ICompetencyGroupRepository competencyGroupRepository, IMapper mapper, ILogger<UpdateCompetencyGroupCommandHandler> logger, ICompetencyLevelRepository competencyLevelRepository)
        {
            _competencyGroupRepository = competencyGroupRepository ?? throw new ArgumentNullException(nameof(competencyGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _competencyLevelRepository = competencyLevelRepository ?? throw new ArgumentNullException(nameof(competencyLevelRepository));
            
        }

        public async Task<long> Handle(UpdateCompetencyGroupCommand request, CancellationToken cancellationToken)
        {
            var competencyGroupToUpdate = await _competencyGroupRepository.GetCompetencyGroupById(request.Id);
            if (competencyGroupToUpdate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.CompetencyGroup), request.Id);
            }
            var competency = _mapper.Map<ICollection<Domain.Entities.Competency>>(request.Competencies);
            //var cg = competency.Select(s => s).Where(s => s.Id != 0).ToList();

            //competencyGroupToUpdate.Competencies = cg;
            var cg = request.Competencies.ToList();
            var Notexists = competencyGroupToUpdate.Competencies.ToList().Where(b => cg.All(a => a.Id != b.Id)).ToList();
            var exists = cg.Where(b => competencyGroupToUpdate.Competencies.ToList().Any(a => a.Id == b.Id)).ToList();
            for (int i = 0; i < exists.Count; i++)
            {
                competencyGroupToUpdate.Competencies.ToList()[i].Description = exists[i].Description;
                competencyGroupToUpdate.Competencies.ToList()[i].Name = exists[i].Name;
                var att = exists[i].Attributes.ToList();
                var exitstattb = att.Where(b => competencyGroupToUpdate.Competencies.ToList()[i].Attributes.ToList().Any(a => a.Id == b.Id)).ToList();
                for (int a = 0; a < exitstattb.Count; a++)
                {
                    var competencyLevel = await _competencyLevelRepository.GetCompetencyLevelById(exitstattb[a].CompetencyLevelId);
                    if (competencyLevel == null)
                    {
                        throw new NotFoundException(nameof(Domain.Entities.CompetencyLevel), exitstattb[a].CompetencyLevelId);
                    }
                    else
                    {
                        if (competencyGroupToUpdate.Competencies.ToList()[i].Attributes.ToList()[a].Id == exitstattb[a].Id)
                        {
                            competencyGroupToUpdate.Competencies.ToList()[i].Attributes.ToList()[a].CompetencyLevel = competencyLevel;
                            competencyGroupToUpdate.Competencies.ToList()[i].Attributes.ToList()[a].Description = exitstattb[a].Description;
                        }
                    }
                }
                var Attributenotexists = _mapper.Map<ICollection<Domain.Entities.Attribute>>(att.Select(s => s).Where(s => s.Id == 0).ToList());
                competencyGroupToUpdate.Competencies.ToList()[i].Attributes.ToList()
                                        .Where(b => att.All(a => a.Id != b.Id)).ToList()
                                        .ForEach
                                         (
                                            a => a.IsDeleted = true
                                         );
                foreach (var newattb in Attributenotexists)
                {
                    if (newattb.Id == 0)
                    {
                        var newcompetencyLevel = await _competencyLevelRepository.GetCompetencyLevelById(newattb.CompetencyLevel.Id);
                        if (newcompetencyLevel == null)
                        {
                            throw new NotFoundException(nameof(Domain.Entities.CompetencyLevel), newattb.CompetencyLevel.Id);
                        }
                        else
                        {
                            newattb.CompetencyLevel = newcompetencyLevel;
                            newattb.Description = newattb.Description;
                        }
                        competencyGroupToUpdate.Competencies.ToList()[i].Attributes.Add((Domain.Entities.Attribute)newattb);
                    }
                }
            }
            var competencynotexists = _mapper.Map<ICollection<Domain.Entities.Competency>>(request.Competencies.Select(s => s).Where(s => s.Id == 0).ToList());
            foreach(var compntex in competencynotexists)
            {
                var att = compntex.Attributes.ToList();
                for (int a = 0; a < att.Count; a++)
                {
                    var competencyLevel = await _competencyLevelRepository.GetCompetencyLevelById(att[a].CompetencyLevel.Id);
                    if (competencyLevel == null)
                    {
                        throw new NotFoundException(nameof(Domain.Entities.CompetencyLevel), att[a].CompetencyLevel.Id);
                    }
                    else
                    {
                        compntex.Attributes.ToList()[a].CompetencyLevel = competencyLevel;
                        compntex.Attributes.ToList()[a].Description = att[a].Description;
                    }
                }
                competencyGroupToUpdate.Competencies.Add((Domain.Entities.Competency)compntex);
            }
            for(int i = 0; i < competencyGroupToUpdate.Competencies.Count; i++)
            {
                
                for (int j=0; j< Notexists.Count; j++)
                {
                    if (competencyGroupToUpdate.Competencies.ToList()[i].Id == Notexists[j].Id)
                    {
                        competencyGroupToUpdate.Competencies.ToList()[i].IsDeleted = true;
                        var isdeleattrb = competencyGroupToUpdate.Competencies.ToList()[i].Attributes.ToList();
                        for (int atb = 0; atb < isdeleattrb.Count; atb++)
                        {
                            if (competencyGroupToUpdate.Competencies.ToList()[i].Attributes.ToList()[atb].Id == isdeleattrb[atb].Id)
                            {
                                competencyGroupToUpdate.Competencies.ToList()[i].Attributes.ToList()[atb].IsDeleted = true;
                            }
                        }
                    }
                }
            }
            // _mapper.Map(request, competencyGroupToUpdate, typeof(UpdateCompetencyGroupCommand), typeof(Domain.Entities.CompetencyGroup));
            competencyGroupToUpdate.Description = request.CompetencyGroupDescription;
            competencyGroupToUpdate.Name = request.CompetencyGroupName;

            
            var updnvalue=await _competencyGroupRepository.UpdateCompetencyGroup(competencyGroupToUpdate);
            //_logger.LogInformation($"Competency Group {competencyGroupToUpdate.Id} is successfully updated.");

            return updnvalue;
        }
    }
}
