﻿using Amazon.Runtime.Internal.Transform;

using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetEdu
{
    /// <summary>
    /// The main page
    /// </summary>
    public partial class Index
    {
        #region Private Members

        /// <summary>
        /// The search text
        /// </summary>
        private string? mSearchText;

        /// <summary>
        /// The universities
        /// </summary>
        private IEnumerable<UniversityResponseModel>? mUniversities;

        /// <summary>
        /// The search bar
        /// </summary>
        private SearchBar? mSearchBar;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// The controller
        /// </summary>
        [Inject]
        protected MeetEduController Controller { get; set; } = default!;

        /// <summary>
        /// The controller
        /// </summary>
        [Inject]
        protected MeetCoreController CoreController { get; set; } = default!;

        /// <summary>
        /// The <see cref="MudBlazor"/> snack bar manager
        /// </summary>
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Index() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
            var response = await Controller.GetUniversitiesAsync(null);

            if (response is null || response.Value.IsNullOrEmpty())
                return;

            mUniversities = response.Value;

        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                //await AddMockData();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates to the respected page of professors or departments of the current department
        /// </summary>
        private void UniversityBox_OnClick(string universityId)
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            NavigationManager.NavigateToDepartmentsPage(new Dictionary<string, string?>()
            {
                new("UniversityId", universityId)
            });
        }

        private async Task AddMockData()
        {
            var entryPoint = new EntryPoint(CoreController);

            // Add the universities
            //await entryPoint.AddUniversitiesAsync();

            if (EntryPoint.Universities.IsNullOrEmpty())
            {
                var universitiesResponse = await Controller.GetUniversitiesAsync(null);

                if (universitiesResponse is null || universitiesResponse.Value.IsNullOrEmpty())
                    return;

                var universities = universitiesResponse.Value;

                EntryPoint.Universities = universities.ToList();
            }

            // Add the departments
            //await entryPoint.AddDepartmentsAsync();

            if (EntryPoint.PaPaDepartments.IsNullOrEmpty())
            {
                var departmentsResponse = await Controller.GetDepartmentsAsync(null);

                if (departmentsResponse is null || departmentsResponse.Value.IsNullOrEmpty())
                    return;

                var papaDepartments = departmentsResponse.Value;

                EntryPoint.PaPaDepartments = papaDepartments!.ToList();
            }

            // Add the secretaries
            //await entryPoint.AddSecretariesAsync();

            // Add the professors
            //await entryPoint.AddProfessorsAsync();


            if (EntryPoint.CeidProfessors.IsNullOrEmpty())
            {
                var professorsResponse = await Controller.GetProfessorsAsync(new()
                {
                    IncludeDepartments = new List<string>() { EntryPoint.PaPaDepartments.First(x => x.Name == "Computer Engineering & Informatics Department").Id, }
                });

                if (professorsResponse is null || professorsResponse.Value.IsNullOrEmpty())
                    return;

                var ceidProfessors = professorsResponse.Value;

                EntryPoint.CeidProfessors = ceidProfessors!.ToList();
            }

            // Adds the appointment rules
            //await entryPoint.AddAppointmentRules();
        }

        #endregion
    }
}
