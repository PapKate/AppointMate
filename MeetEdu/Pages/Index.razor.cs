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
            await base.OnInitializedAsync();

            var response = await Controller.GetUniversitiesAsync(null);

            if (response is null || response.Value.IsNullOrEmpty())
                return;

            mUniversities = response.Value;
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

        /// <summary>
        /// Navigates to the respected page of professors or departments of the current department
        /// </summary>
        private void SearchButton_OnClick()
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            if (mSearchBar!.IsSearchForDepartments)
            {
                NavigationManager.NavigateToDepartmentsPage(new Dictionary<string, string?>()
                {
                    new("SearchText", mSearchText)
                });
            }
            else
            {
                NavigationManager.NavigateToFacultyPage(new Dictionary<string, string?>()
                {
                    new("SearchText", mSearchText)
                });
            }
        }


        #endregion
    }
}
