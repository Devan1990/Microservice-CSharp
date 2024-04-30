using CompetencyFramework.API.Controllers;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup;
using CompetencyFramework.Domain.Entities;
using CompetencyFramework.Test.MockData;
using MediatR;
using Microsoft.Extensions.Configuration;
using Moq;
namespace CompetencyFramework.Test
{
   
    public class CompentencyGroupTest
    {
       
        //public Mock<ICompetencyGroupRepository> mock = new Mock<ICompetencyGroupRepository>();
        //private readonly IMediator _mediator;
        //public Mock<IMediator> mock1 = new Mock<IMediator>();
        //public CompentencyGroupTest(IMediator mediator)
        //{
        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //}

        //public CompentencyGroupTest(IMediator mediator)
        //{
        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //}

        //[Fact]
        //public async void GetCompetencyGroupById()
        //{

        //    var CompetencyLevelDTO = new Domain.Entities.CompetencyLevel()
        //    {
        //        Name = "Attribute 1",
        //        Weightage=1,
        //        IsDeleted=false,
        //    };
        //    var AttributeDTO = new Domain.Entities.Attribute()
        //    {
        //        Description= "Attribute 1",
        //        CompetencyLevel= CompetencyLevelDTO
        //    };
        //    var CompetencyDTO = new Competency()
        //    {
        //        CompetencyId = "CM00001",
        //        Name = "CM1",
        //        Description = "CM1",
        //        IsDeleted = false,
        //        Attributes = new[] { AttributeDTO },
        //    };
        //    var CompetencyGroupDTO = new CompetencyGroup()
        //    {
        //        CompetencyGroupId = "GG00001",
        //        Name = "CG1",
        //        Description = "CG1",
        //        Competencies=new[] { CompetencyDTO }
        //    };

        //    mock.Setup(p => p.GetCompetencyGroupById(1)).ReturnsAsync(CompetencyGroupDTO);

        //    //CompetencyGroupController emp = new CompetencyGroupController(_mediator);
        //    //var result = await emp.GetCompetencyGroup(1);

        //    CompetencyGroupController emp = new CompetencyGroupController(mock1.Object);
        //    var result = await emp.GetCompetencyGroup(1);

        //    Assert.True(CompetencyGroupDTO.Equals(result));

        //}

        [Fact]
        public async Task CreateCompetencyGroup_Pass()
        {
            //Arrange
            var _mediator = new Moq.Mock<MediatR.IMediator>();
            var _configuration = new Moq.Mock<IConfiguration>();
            CreateCompetencyGroupCommand newCG = CompentencyGroupMockData.GetComGroupCommandTest();
            var sut = new CompetencyGroupController(_mediator.Object, _configuration.Object);
            /// Act
            var result = await sut.CreateCompetencyGroup(newCG);


            /// Assert
            _mediator.Verify(x => x.Send(newCG, It.IsAny<CancellationToken>()), Times.Exactly(1));

        }
    }
}