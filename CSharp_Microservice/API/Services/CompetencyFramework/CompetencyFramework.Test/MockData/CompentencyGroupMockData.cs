using CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute;
using CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute;
using CompetencyFramework.Application.Features.Competency.Commands.CreateCompetency;
using CompetencyFramework.Application.Features.Competency.Commands.UpdateCompetency;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.UpdateCompetencyGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetencyFramework.Test.MockData
{
    internal class CompentencyGroupMockData
    {
        public static CreateCompetencyGroupCommand GetComGroupCommandTest()
        {
            return new CreateCompetencyGroupCommand { CompetencyGroupName = "ABC", CompetencyGroupDescription = "ABCD", Competencies = GetGetCGCCompCommandTest() };


        }

        public static List<CreateCGCompetencyCommand> GetGetCGCCompCommandTest()
        {
            return new List<CreateCGCompetencyCommand>
            {
                new CreateCGCompetencyCommand { Name ="ABC",Description="ABCD",Attributes =GetCompentencyGroupAttributeCommandsTest()}


            };
        }

        public static List<CreateCompentencyGroupAttributeCommand> GetCompentencyGroupAttributeCommandsTest()
        {

            return new List<CreateCompentencyGroupAttributeCommand>
            {
                new CreateCompentencyGroupAttributeCommand { CompetencyLevelId = 0, Description = "ABCD" },
                new CreateCompentencyGroupAttributeCommand { CompetencyLevelId = 0, Description = "ABCD" }
            };
        }


        public static UpdateCompetencyGroupCommand UpdateComGroupCommandTest()
        {
            return new UpdateCompetencyGroupCommand { CompetencyGroupName = "ABC", CompetencyGroupDescription = "ABCD", Competencies = UpdateCGCompetencyCommandTest() };


        }

        public static List<UpdateCGCompetencyCommand> UpdateCGCompetencyCommandTest()
        {
            return new List<UpdateCGCompetencyCommand>
            {
                new UpdateCGCompetencyCommand { Id=1, Name ="ABC",Description="ABCD",Attributes =UpdateCGAttributeCommandTest()}


            };
        }

        public static List<UpdateCGAttributeCommand> UpdateCGAttributeCommandTest()
        {

            return new List<UpdateCGAttributeCommand>
            {
                new UpdateCGAttributeCommand {Id=1, CompetencyLevelId = 0, Description = "ABCD" },
                new UpdateCGAttributeCommand {Id = 1,  CompetencyLevelId = 0, Description = "ABCD" }
            };
        }
    }
}
