using ClosedXML.Excel;
using CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute;
using CompetencyFramework.Application.Features.Competency.Commands.CreateCompetency;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup;
using CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CompetencyFramework.API.Utility
{
    public class CompetencyGroupBulkUpload
    {
        private readonly IMediator _mediator;
        private string tmpCG_name = string.Empty;
        private long CG_Result, CM_Result, ErrorLine = 0;
        private List<CreateAttributeCommand> attribueList = new List<CreateAttributeCommand>();
        private List<CompetencyGroupsVm> competencyGroupsList = new List<CompetencyGroupsVm>();
        public CompetencyGroupBulkUpload(IMediator mediator, List<CompetencyGroupsVm> comptencyGroups)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            competencyGroupsList = comptencyGroups ?? throw new ArgumentNullException(nameof(comptencyGroups));
        }
        public async Task<string> CompetencyGroupArrayAsync(IFormFile file)
        {
            try
            {
                var fileextension = Path.GetExtension(file.FileName);
                var filename = DateTime.Now.ToString("yyyyMMdd_hhmmss") + fileextension;
                if (!(fileextension == ".xlsx" || fileextension == ".xls" || fileextension == ".xlsm" || fileextension == ".xlsb"))
                {
                    throw new FormatException("File Extension Not in Correct Format");
                }
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BulkUploadFiles", filename);

                try
                {
                    Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BulkUploadFiles\\"))
                             .Select(f => new FileInfo(f))
                             .Where(f => f.LastWriteTime < DateTime.Now.AddDays(-7))
                             .ToList()
                             .ForEach(f => f.Delete());
                }
                catch
                { }

                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }

                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();

                int lastrow = sheets.LastRowUsed().RowNumber();

                if (rows.Count == 0 || lastrow > 501)
                {
                    {
                        throw new FormatException("File Not in Correct Format");
                    }
                }
                for (int i = 0; i < lastrow - 1; i++)
                {
                    ErrorLine = i;

                    if (i == 0)
                    {
                        if (rows[0].Cell(1).Value.ToString().TrimStart().TrimEnd() != "CompetencyGroup Name" &&
                              rows[0].Cell(2).Value.ToString().TrimStart().TrimEnd() != "CompetencyGroup Description" &&
                              rows[0].Cell(3).Value.ToString().TrimStart().TrimEnd() != "Competency Name" &&
                              rows[0].Cell(4).Value.ToString().TrimStart().TrimEnd() != "Competency Description" &&
                              rows[0].Cell(5).Value.ToString().TrimStart().TrimEnd() != "Attribute Description" &&
                              rows[0].Cell(6).Value.ToString().TrimStart().TrimEnd() != "CompetencyLevel")
                        {
                            throw new FormatException("File Data Not in Correct Format, Check Row Number " + (i + 1));
                        }
                    }
                    else
                    {
                        var CG_name = rows[i].Cell(1).Value.ToString().TrimStart().TrimEnd();
                        var CG_desc = rows[i].Cell(2).Value.ToString().TrimStart().TrimEnd();

                        bool duplicate = false;
                        Parallel.ForEach(competencyGroupsList, group =>
                            {
                                if (group.Name == CG_name)
                                { duplicate = true; }
                            });

                        if (duplicate)
                        { continue; }

                        if (string.IsNullOrEmpty(CG_name))
                        {
                            throw new FormatException("File Data Not in Correct Format, Check Row Number " + (i + 1));
                        }

                        if (CG_name != tmpCG_name)
                        {
                            if (CG_name.Length > 50)
                            {
                                throw new FormatException("File Data exceeds than 50 Characaters, Check Row Number " + (i + 1));
                            }

                            CreateCompetencyGroupCommand CG = new CreateCompetencyGroupCommand();
                            CG.CompetencyGroupName = CG_name;
                            CG.CompetencyGroupDescription = CG_desc;

                            CG_Result = await CreateCompetencyGroup(CG);
                        }

                        if (CG_Result > 0)
                        {
                            var CM_name = rows[i].Cell(3).Value.ToString().TrimStart().TrimEnd();
                            var CM_desc = rows[i].Cell(4).Value.ToString().TrimStart().TrimEnd();
                            var AT_desc = rows[i].Cell(5).Value.ToString().TrimStart().TrimEnd();
                            var AT_CompLevelId = rows[i].Cell(6).Value.ToString().TrimStart().TrimEnd();

                            if (CM_name.Length > 50)
                            {
                                throw new FormatException("File Data exceeds than 50 Characaters, Check Row Number " + (i + 1));
                            }

                            if (string.IsNullOrEmpty(CM_name))
                            {
                                throw new FormatException("File Data Not in Correct Format, Check Row Number " + (i + 1));
                            }

                            CreateCompetencyCommand CM = new CreateCompetencyCommand();
                            CM.CompetencyGroupId = CG_Result;
                            CM.Name = CM_name;
                            CM.Description = CM_desc;

                            if (AT_CompLevelId.ToLower() == "foundational")
                            { AT_CompLevelId = "1"; }
                            if (AT_CompLevelId.ToLower() == "proficient")
                            { AT_CompLevelId = "2"; }
                            if (AT_CompLevelId.ToLower() == "advanced")
                            { AT_CompLevelId = "3"; }

                            CreateAttributeCommand AT = new CreateAttributeCommand();
                            AT.CompetencyLevelId = int.Parse(AT_CompLevelId);
                            AT.Description = AT_desc;

                            attribueList.Add(AT);

                            CM.Attributes = attribueList;

                            if (CM_name != rows[i + 1].Cell(3).Value.ToString().TrimStart().TrimEnd() || CG_name != rows[i + 1].Cell(1).Value.ToString().TrimStart().TrimEnd())
                            {
                                CM_Result = await CreateCompetency(CM);
                                attribueList.Clear();
                            }
                        }
                        tmpCG_name = CG_name;
                    }
                }
                return "Success Uploading File Data";
            }
            catch (Exception ex)
            {
                return ex + "\nCheck Row Number " + (ErrorLine + 1) + "";
            }
        }

        public async Task<long> CreateCompetencyGroup(CreateCompetencyGroupCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<long> CreateCompetency(CreateCompetencyCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
