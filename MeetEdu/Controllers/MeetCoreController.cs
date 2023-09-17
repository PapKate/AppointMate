﻿using MeetBase.Web;

using MeetEdu.Helpers;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;

namespace MeetEdu
{
    /// <summary>
    /// Controller used for handing the requests related to a MeetCore related application
    /// </summary>
    public class MeetCoreController : Controller
    {
        #region Public Properties

        /// <summary>
        /// The university id
        /// </summary>
        public ObjectId UniversityId { get; set; } = "65057b282d75604f6c77ff93".ToObjectId();

        /// <summary>
        /// The department id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MeetCoreController() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Register / Login

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.RegisterRoute)]
        public async Task<ActionResult<UserResponseModel>> RegisterUserAsync([FromBody] UserRequestModel model)
            => (await DI.UsersRepository.AddUserAsync(model)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Validates the user credentials sent by the user and returns the user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.LogInRoute)]
        public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LogInRequestModel model)
            => (await DI.AccountsRepository.LoginAsync(model)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Universities

        /// <summary>
        /// Gets the universities
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.UniversitiesRoute)]
        public async Task<ActionResult<IEnumerable<UniversityResponseModel>>> GetUniversitiesAsync([FromQuery] APIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Universities,
                                                    x => x.ToResponseModel(),
                                                    Builders<UniversityEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.Name);

        /// <summary>
        /// Creates a university
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.UniversitiesRoute)]
        public async Task<ActionResult<UniversityResponseModel>> AddUniversityAsync([FromBody] UniversityRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.AddUniversityAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the university with the specified <paramref name="universityId"/>
        /// </summary>
        /// <param name="universityId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.UniversityRoute)]
        public async Task<ActionResult<UniversityResponseModel>?> GetUniversityAsync([FromRoute] string universityId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Universities, x => x.ToResponseModel(), x => x.Id == universityId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the university with the specified <paramref name="universityId"/>
        /// </summary>
        /// <param name="universityId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.UniversityRoute)]
        public async Task<ActionResult<UniversityResponseModel>> UpdateUniversityAsync([FromRoute] string universityId, [FromBody] UniversityRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.UpdateUniversityAsync(universityId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the university with the specified <paramref name="universityId"/>
        /// </summary>
        /// <param name="universityId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.UniversityRoute)]
        public async Task<ActionResult<UniversityResponseModel>> DeleteUniversityAsync([FromRoute] string universityId, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.DeleteUniversityAsync(universityId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #region Labels

        /// <summary>
        /// Gets the university labels
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.UniversityLabelsRoute)]
        public async Task<ActionResult<IEnumerable<LabelResponseModel>>> GetUniversityLabelsAsync([FromQuery] APIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.UniversityLabels,
                                                    x => x.ToResponseModel(),
                                                    Builders<LabelEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.Name);

        /// <summary>
        /// Creates a university label
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.UniversityLabelsRoute)]
        public async Task<ActionResult<LabelResponseModel>> AddUniversityLabelAsync([FromBody] LabelRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.AddUniversityLabelAsync(UniversityId, model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the university label with the specified <paramref name="universityLabelId"/>
        /// </summary>
        /// <param name="universityLabelId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.UniversityLabelRoute)]
        public async Task<ActionResult<LabelResponseModel>?> GetUniversityLabelAsync([FromRoute] string universityLabelId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.UniversityLabels, x => x.ToResponseModel(), x => x.Id == universityLabelId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the university label with the specified <paramref name="universityLabelId"/>
        /// </summary>
        /// <param name="universityLabelId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.UniversityLabelRoute)]
        public async Task<ActionResult<LabelResponseModel>> UpdateUniversityLabelAsync([FromRoute] string universityLabelId, [FromBody] LabelRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.UpdateUniversityLabelAsync(universityLabelId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the university label with the specified <paramref name="universityLabelId"/>
        /// </summary>
        /// <param name="universityLabelId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.UniversityLabelRoute)]
        public async Task<ActionResult<LabelResponseModel>> DeleteUniversityLabelAsync([FromRoute] string universityLabelId, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.DeleteUniversityLabelAsync(universityLabelId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #endregion

        #region Departments

        /// <summary>
        /// Gets the departments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentsRoute)]
        public async Task<ActionResult<IEnumerable<DepartmentResponseModel>>> GetDepartmentsAsync([FromQuery] DepartmentAPIArgs? args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Departments,
                                                    x => x.ToResponseModel(),
                                                    args?.CreateFilters().AggregateFilters(), args,
                                                    cancellationToken, x => x.Name);

        /// <summary>
        /// Creates a department
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.DepartmentsRoute)]
        public async Task<ActionResult<DepartmentResponseModel>> AddDepartmentAsync([FromBody] DepartmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.AddDepartmentAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the department with the specified <paramref name="departmentId"/>
        /// </summary>
        /// <param name="departmentId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>?> GetDepartmentAsync([FromRoute] string departmentId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Departments, x => x.ToResponseModel(), x => x.Id == departmentId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the department with the specified <paramref name="departmentId"/>
        /// </summary>
        /// <param name="departmentId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.DepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>> UpdateDepartmentAsync([FromRoute] string departmentId, [FromBody] DepartmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.UpdateDepartmentAsync(departmentId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the department with the specified <paramref name="departmentId"/>
        /// </summary>
        /// <param name="departmentId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.DepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>> DeleteDepartmentAsync([FromRoute] string departmentId, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.DeleteDepartmentAsync(departmentId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #region Layout 

        /// <summary>
        /// Gets the department layouts
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentLayoutsRoute)]
        public async Task<ActionResult<IEnumerable<DepartmentLayoutResponseModel>>> GetDepartmentLayoutsAsync([FromQuery] DepartmentRelatedAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.DepartmentLayouts,
                                                    x => x.ToResponseModel(),
                                                    Builders<DepartmentLayoutEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.DepartmentId);

        /// <summary>
        /// Creates a department layout
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.DepartmentLayoutsRoute)]
        public async Task<ActionResult<DepartmentLayoutResponseModel>> AddDepartmentLayoutAsync([FromBody] DepartmentLayoutRequestModel model, CancellationToken cancellationToken = default) 
            => (await DI.DepartmentsRepository.AddDepartmentLayoutAsync(DepartmentId, model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the department layout with the specified <paramref name="departmentLayoutId"/>
        /// </summary>
        /// <param name="departmentLayoutId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentLayoutRoute)]
        public async Task<ActionResult<DepartmentLayoutResponseModel>?> GetDepartmentLayoutAsync([FromRoute] string departmentLayoutId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.DepartmentLayouts, x => x.ToResponseModel(), x => x.Id == departmentLayoutId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the department layout with the specified <paramref name="departmentLayoutId"/>
        /// </summary>
        /// <param name="departmentLayoutId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.DepartmentLayoutRoute)]
        public async Task<ActionResult<DepartmentLayoutResponseModel>> UpdateDepartmentLayoutAsync([FromRoute] string departmentLayoutId, [FromBody] DepartmentLayoutRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.UpdateDepartmentLayoutAsync(departmentLayoutId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the department layout with the specified <paramref name="departmentLayoutId"/>
        /// </summary>
        /// <param name="departmentLayoutId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.DepartmentLayoutRoute)]
        public async Task<ActionResult<DepartmentLayoutResponseModel>> DeleteDepartmentLayoutAsync([FromRoute] string departmentLayoutId, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.DeleteDepartmentLayoutAsync(departmentLayoutId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Labels

        /// <summary>
        /// Gets the department labels
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentLabelsRoute)]
        public async Task<ActionResult<IEnumerable<LabelResponseModel>>> GetDepartmentLabelsAsync([FromQuery] DepartmentRelatedAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.DepartmentLabels,
                                                    x => x.ToResponseModel(),
                                                    Builders<LabelEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.Name);

        /// <summary>
        /// Creates a department label
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.DepartmentLabelsRoute)]
        public async Task<ActionResult<LabelResponseModel>> AddDepartmentLabelAsync([FromBody] LabelRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.AddDepartmentLabelAsync(DepartmentId, model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the department label with the specified <paramref name="departmentLabelId"/>
        /// </summary>
        /// <param name="departmentLabelId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentLabelRoute)]
        public async Task<ActionResult<LabelResponseModel>?> GetDepartmentLabelAsync([FromRoute] string departmentLabelId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.DepartmentLabels, x => x.ToResponseModel(), x => x.Id == departmentLabelId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the department label with the specified <paramref name="departmentLabelId"/>
        /// </summary>
        /// <param name="departmentLabelId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.DepartmentLabelRoute)]
        public async Task<ActionResult<LabelResponseModel>> UpdateDepartmentLabelAsync([FromRoute] string departmentLabelId, [FromBody] LabelRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.UpdateDepartmentLabelAsync(departmentLabelId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the department label with the specified <paramref name="departmentLabelId"/>
        /// </summary>
        /// <param name="departmentLabelId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.DepartmentLabelRoute)]
        public async Task<ActionResult<LabelResponseModel>> DeleteDepartmentLabelAsync([FromRoute] string departmentLabelId, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.DeleteDepartmentLabelAsync(departmentLabelId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Contact

        /// <summary>
        /// Gets the department contact messages
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentContactMessagesRoute)]
        public async Task<ActionResult<IEnumerable<DepartmentContactMessageResponseModel>>> GetDepartmentContactMessagesAsync([FromQuery] DepartmentRelatedAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.DepartmentContactMessages,
                                                    x => x.ToResponseModel(),
                                                    Builders<DepartmentContactMessageEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.DepartmentId);

        /// <summary>
        /// Creates a department contact message
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.DepartmentContactMessagesRoute)]
        public async Task<ActionResult<DepartmentContactMessageResponseModel>> AddDepartmentContactMessageAsync([FromBody] DepartmentContactMessageRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.AddDepartmentContactMessageAsync(DepartmentId, model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the department contact message with the specified <paramref name="departmentContactMessageId"/>
        /// </summary>
        /// <param name="departmentContactMessageId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentContactMessageRoute)]
        public async Task<ActionResult<DepartmentContactMessageResponseModel>?> GetDepartmentContactMessageAsync([FromRoute] string departmentContactMessageId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.DepartmentContactMessages, x => x.ToResponseModel(), x => x.Id == departmentContactMessageId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the department contact message with the specified <paramref name="departmentContactMessageId"/>
        /// </summary>
        /// <param name="departmentContactMessageId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.DepartmentContactMessageRoute)]
        public async Task<ActionResult<DepartmentContactMessageResponseModel>> UpdateDepartmentContactMessageAsync([FromRoute] string departmentContactMessageId, [FromBody] DepartmentContactMessageRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.UpdateDepartmentContactMessageAsync(departmentContactMessageId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the department contact message with the specified <paramref name="departmentContactMessageId"/>
        /// </summary>
        /// <param name="departmentContactMessageId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.DepartmentContactMessageRoute)]
        public async Task<ActionResult<DepartmentContactMessageResponseModel>> DeleteDepartmentContactMessageAsync([FromRoute] string departmentContactMessageId, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.DeleteDepartmentContactMessageAsync(departmentContactMessageId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #endregion

        #region Secretaries

        /// <summary>
        /// Gets the secretaries
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.SecretariesRoute)]
        public async Task<ActionResult<IEnumerable<SecretaryResponseModel>>> GetSecretariesAsync([FromQuery] StafMemberAPIArgs? args, CancellationToken cancellationToken = default) 
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Secretaries, 
                                                    x => x.ToResponseModel(), 
                                                    args?.CreateFilters<SecretaryEntity>().AggregateFilters(), args, 
                                                    cancellationToken, x => x.Role);

        /// <summary>
        /// Creates a secretary
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.SecretariesRoute)]
        public async Task<ActionResult<SecretaryResponseModel>> AddSecretaryAsync([FromBody] SecretaryRequestModel model, CancellationToken cancellationToken = default) 
            => (await DI.SecretariesRepository.AddSecretaryAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the secretary
        /// </summary>
        /// <param name="secretaryId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.SecretaryRoute)]
        public async Task<ActionResult<SecretaryResponseModel>?> GetSecretaryAsync([FromRoute] string secretaryId, CancellationToken cancellationToken = default) 
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Secretaries, x => x.ToResponseModel(), x => x.Id == secretaryId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the secretary with the specified <paramref name="secretaryId"/>
        /// </summary>
        /// <param name="secretaryId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.SecretaryRoute)]
        public async Task<ActionResult<SecretaryResponseModel>> UpdateSecretaryAsync([FromRoute] string secretaryId, [FromBody] SecretaryRequestModel model, [FromBody] UserRequestModel user, CancellationToken cancellationToken = default)
            => (await DI.SecretariesRepository.UpdateSecretaryAsync(secretaryId.ToObjectId(), model, user, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the secretary with the specified <paramref name="secretaryId"/>
        /// </summary>
        /// <param name="secretaryId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.SecretaryRoute)]
        public async Task<ActionResult<SecretaryResponseModel>> DeleteSecretaryAsync([FromRoute] string secretaryId, CancellationToken cancellationToken = default)
            => (await DI.SecretariesRepository.DeleteSecretaryAsync(secretaryId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Professors

        /// <summary>
        /// Gets the professors
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.ProfessorsRoute)]
        public async Task<ActionResult<IEnumerable<ProfessorResponseModel>>> GetProfessorsAsync([FromQuery] StafMemberAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Professors,
                                                    x => x.ToResponseModel(),
                                                    args.CreateFilters<ProfessorEntity>().AggregateFilters(), args,
                                                    cancellationToken, x => x.Field);

        /// <summary>
        /// Creates a professor
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.ProfessorsRoute)]
        public async Task<ActionResult<ProfessorResponseModel>> AddProfessorAsync([FromBody] ProfessorRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.ProfessorsRepository.AddProfessorAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the professor
        /// </summary>
        /// <param name="professorId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.ProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>?> GetProfessorAsync([FromRoute] string professorId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Professors, x => x.ToResponseModel(), x => x.Id == professorId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the professor with the specified <paramref name="professorId"/>
        /// </summary>
        /// <param name="professorId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.ProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>> UpdateProfessorAsync([FromRoute] string professorId, [FromBody] ProfessorRequestModel model, [FromBody] UserRequestModel user, CancellationToken cancellationToken = default)
            => (await DI.ProfessorsRepository.UpdateProfessorAsync(professorId.ToObjectId(), model, user, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the professor with the specified <paramref name="professorId"/>
        /// </summary>
        /// <param name="professorId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.ProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>> DeleteProfessorAsync([FromRoute] string professorId, CancellationToken cancellationToken = default)
            => (await DI.ProfessorsRepository.DeleteProfessorAsync(professorId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Appointment Rules

        /// <summary>
        /// Gets the appointment rules
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.AppointmentRulesRoute)]
        public async Task<ActionResult<IEnumerable<AppointmentRuleResponseModel>>> GetAppointmentRulesAsync([FromQuery] AppointmentRuleAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.AppointmentRules,
                                                    x => x.ToResponseModel(),
                                                    Builders<AppointmentRuleEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.DateCreated);

        /// <summary>
        /// Creates an appointment rule
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.AppointmentRulesRoute)]
        public async Task<ActionResult<AppointmentRuleResponseModel>> AddAppointmentRuleAsync([FromBody] AppointmentRuleRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.AddAppointmentRuleAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the appointment rule with the specified <paramref name="appointmentRuleId"/>
        /// </summary>
        /// <param name="appointmentRuleId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.AppointmentRuleRoute)]
        public async Task<ActionResult<AppointmentRuleResponseModel>?> GetAppointmentRuleAsync([FromRoute] string appointmentRuleId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.AppointmentRules, x => x.ToResponseModel(), x => x.Id == appointmentRuleId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the appointment rule with the specified <paramref name="appointmentRuleId"/>
        /// </summary>
        /// <param name="appointmentRuleId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.AppointmentRuleRoute)]
        public async Task<ActionResult<AppointmentRuleResponseModel>> UpdateAppointmentRuleAsync([FromRoute] string appointmentRuleId, [FromBody] AppointmentRuleRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.UpdateAppointmentRuleAsync(appointmentRuleId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the appointment rule with the specified <paramref name="appointmentRuleId"/>
        /// </summary>
        /// <param name="appointmentRuleId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.AppointmentRuleRoute)]
        public async Task<ActionResult<AppointmentRuleResponseModel>> DeleteAppointmentRuleAsync([FromRoute] string appointmentRuleId, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.DeleteAppointmentRuleAsync(appointmentRuleId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Appointments

        /// <summary>
        /// Gets the appointments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.AppointmentsRoute)]
        public async Task<ActionResult<IEnumerable<AppointmentResponseModel>>> GetAppointmentsAsync([FromQuery] APIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Appointments,
                                                    x => x.ToResponseModel(),
                                                    Builders<AppointmentEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.DateCreated);

        /// <summary>
        /// Creates a appointment
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.AppointmentsRoute)]
        public async Task<ActionResult<AppointmentResponseModel>> AddAppointmentAsync([FromBody] AppointmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.AddAppointmentAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the appointment with the specified <paramref name="appointmentId"/>
        /// </summary>
        /// <param name="appointmentId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.AppointmentRoute)]
        public async Task<ActionResult<AppointmentResponseModel>?> GetAppointmentAsync([FromRoute] string appointmentId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Appointments, x => x.ToResponseModel(), x => x.Id == appointmentId.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the appointment with the specified <paramref name="appointmentId"/>
        /// </summary>
        /// <param name="appointmentId">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.AppointmentRoute)]
        public async Task<ActionResult<AppointmentResponseModel>> UpdateAppointmentAsync([FromRoute] string appointmentId, [FromBody] AppointmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.UpdateAppointmentAsync(appointmentId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the appointment with the specified <paramref name="appointmentId"/>
        /// </summary>
        /// <param name="appointmentId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.AppointmentRoute)]
        public async Task<ActionResult<AppointmentResponseModel>> DeleteAppointmentAsync([FromRoute] string appointmentId, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.DeleteAppointmentAsync(appointmentId.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #endregion
    }
}
