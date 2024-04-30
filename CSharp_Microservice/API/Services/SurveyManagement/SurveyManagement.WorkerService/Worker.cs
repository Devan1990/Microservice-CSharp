using AutoMapper;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Domain.Entities;
using SurveyManagement.WorkerService.GrpcServices;
namespace SurveyManagement.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
       
        private readonly ISurveyRepository _surveyrepository;
        private readonly IUserSurveyRepository _usersurveyrepository;
        private readonly IUserSurveyAssessmentRepository _usersurveyAssessmentrepository;
        private readonly UserSurveyGrpcService _usersurveyGrpcService;
        public Worker(ILogger<Worker> logger, ISurveyRepository surveyrepository,  UserSurveyGrpcService usersurveygrpcservice, IUserSurveyRepository usersurveyrepository, IUserSurveyAssessmentRepository usersurveyAssessmentrepository)
        {
            _logger = logger;
      
            _surveyrepository = surveyrepository ?? throw new ArgumentNullException(nameof(surveyrepository));
            _usersurveyGrpcService = usersurveygrpcservice ?? throw new ArgumentNullException(nameof(usersurveygrpcservice));
            _usersurveyrepository = usersurveyrepository ?? throw new ArgumentNullException(nameof(usersurveyrepository));
            _usersurveyAssessmentrepository = usersurveyAssessmentrepository ?? throw new ArgumentNullException(nameof(usersurveyAssessmentrepository));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string CurrFY = GetCurrentFinancialYear();
                var SurveyExists = await _surveyrepository.GetSurveyByCurrentFinancialYear(CurrFY);
                var RoleMappingData = await _usersurveyGrpcService.GetRoleMappingLst(0);
                var RoleMappingDataList = RoleMappingData.RoleMappingData.ToList();
                var Surveylst = SurveyExists.ToList();
                for (int i = 0; i < Surveylst.Count; i++)
                {
                    List<Survey> Surveylstnew = new List<Survey>();
                    Surveylstnew.Add(Surveylst[i]);
                    var UserwiseRolevalues = await _usersurveyGrpcService.GetUserByRoleIdLst(Surveylstnew[0].RoleId);
                    var MatchedRoles = RoleMappingDataList.
                                Where(b => Surveylstnew.Any(a => a.RoleId == b.RoleId)).FirstOrDefault();
                    var userList = UserwiseRolevalues.Userrolesdetails.ToList();
                    var assrole = MatchedRoles.AssessorRole.AssessorRole.ToList();
                    for (int user = 0; user < userList.Count; user++)
                    {
                        var usersSurveryExists = await _usersurveyrepository.GetUserSurveyListBySurveyIdUserid(Surveylstnew[0].Id, userList[user].Id);
                        if (usersSurveryExists == null)
                        {
                            UserSurvey us = new UserSurvey();
                            //var AI = await _usersurveyrepository.GetUserSurveyOrderById(null, b => b.OrderByDescending(b => b.Id));
                            //var AI_ID = AI == null ? 1 : AI.Id + 1;
                            //us.AssessmentID = "AI" + AI_ID.ToString().PadLeft(AI_ID.ToString().Length + 5 - AI_ID.ToString().Length, '0');
                            us.SurveyId = Surveylstnew[0].Id;
                            us.UserId = userList[user].Id;
                            List<UserSurveyAssessment> userSurveyAssessments = new List<UserSurveyAssessment>();
                            var assessmentype = await _usersurveyrepository.GetAssessmentTypeById(1);
                            AssessmentType asstype = new AssessmentType();
                            asstype = assessmentype;
                            for (int assdet = 0; assdet < assrole.Count; assdet++)
                            {
                                UserSurveyAssessment usa = new UserSurveyAssessment();
                                usa.AssessorRoleId = assrole[assdet].RoleId;
                                usa.AssessmentType = asstype;
                                usa.CreatedBy = "swn";
                                usa.CreatedDate = DateTime.Now;
                                userSurveyAssessments.Add(usa);
                            }
                            us.UserSurveyAssessments = userSurveyAssessments;
                            us.CreatedBy = "swn";
                            us.CreatedDate = DateTime.Now;
                            var SuccessResult = await _usersurveyrepository.AddUserSurvey(us);
                        }
                        else
                        {
                            var usersurveyToUpdate = await _usersurveyrepository.GetUserSurveyById(usersSurveryExists.Id);
                            var initialcount = usersurveyToUpdate.UserSurveyAssessments.Count;
                            var assessmentype = await _usersurveyrepository.GetAssessmentTypeById(1);
                            AssessmentType asstype = new AssessmentType();
                            asstype = assessmentype;
                            for (int assdet = 0; assdet < assrole.Count; assdet++)
                            {
                                var Assessmentusersurveyquery = await _usersurveyAssessmentrepository.GetUserSurveyQuery(a => a.UserSurvey.Id == usersurveyToUpdate.Id && a.AssessorRoleId == assrole[assdet].RoleId);
                                if (Assessmentusersurveyquery.Count > 0)
                                {
                                    Console.WriteLine("User survey already exists");
                                }
                                else
                                {
                                    UserSurveyAssessment us1 = new UserSurveyAssessment();
                                    us1.AssessorRoleId = assrole[assdet].RoleId;
                                    us1.AssessmentType = asstype;
                                    usersurveyToUpdate.UserSurveyAssessments.Add(us1);
                                }
                            }
                            if (initialcount != usersurveyToUpdate.UserSurveyAssessments.Count)
                            {
                                await _usersurveyrepository.UpdateAsync(usersurveyToUpdate);
                            }
                        }
                    }

                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }

        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;
            FinYear = (DateTime.Today.Month > 3 ? NexYear : PreYear);
            return FinYear.Trim();
        }
    }
}