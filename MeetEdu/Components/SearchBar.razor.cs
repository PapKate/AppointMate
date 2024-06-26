﻿using Amazon.Runtime.Internal.Transform;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetEdu
{
    /// <summary>
    /// The search bar component
    /// </summary>
    public partial class SearchBar
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="IsSearchForDepartments"/> property
        /// </summary>
        private bool mIsSearchForDepartments = true;

        /// <summary>
        /// The search category
        /// </summary>
        private string mSearchCategory = MeetEduConstants.Departments;

        /// <summary>
        /// The search icon
        /// </summary>
        private string mSearchIcon = Icons.Material.Filled.Pages;

        /// <summary>
        /// The search back color
        /// </summary>
        private Color mSearchBackColor = Color.Primary;

        #endregion

        #region Public Properties

        /// <summary>
        /// The search text
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// A flag indicating whether the search regarding departments or not
        /// </summary>
        public bool IsSearchForDepartments
        {
            get => mIsSearchForDepartments;
            set
            {
                mIsSearchForDepartments = value;

                if (mIsSearchForDepartments == true)
                {
                    mSearchCategory = MeetEduConstants.Departments;
                    mSearchBackColor = Color.Primary;
                    mSearchIcon = Icons.Material.Filled.Pages;
                }
                else
                {
                    mSearchCategory = MeetEduConstants.Faculty;
                    mSearchBackColor = Color.Secondary;
                    mSearchIcon = Icons.Material.Filled.Groups3;
                }
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// The search manager service
        /// </summary>
        [Inject]
        protected SearchManager? SearchManager { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchBar() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if(SearchManager is not null)
                IsSearchForDepartments = SearchManager.SearchResultType == SearchResultType.Departments;

        }

        /// <summary>
        /// Handles the text change
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        protected async Task<string> OnTextChanged(string value)
        {
            Text = value;
            await TextChanged.InvokeAsync(value);
            return Text;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the search result type
        /// </summary>
        private void SearchCategoryButton_OnClick()
        {
            SearchManager?.SwitchSearchReultType();
            IsSearchForDepartments = !IsSearchForDepartments;
        }

        /// <summary>
        /// Navigates to the respected page of professors or departments of the current department
        /// </summary>
        private async void SearchButton_OnClick()
        {
            await OnSearch.InvokeAsync();

            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            if (SearchManager is not null)
                SearchManager.Text = Text;
            
            if (IsSearchForDepartments)
            {
                NavigationManager.NavigateToDepartmentsPage(new Dictionary<string, string?>()
                {
                    new("SearchText", Text)
                });
            }
            else
            {
                NavigationManager.NavigateToFacultyPage(new Dictionary<string, string?>()
                {
                    new("SearchText", Text)
                });
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the <see cref="Text"/> is changed
        /// </summary>
        [Parameter]
        public EventCallback<string> TextChanged { get; set; }

        /// <summary>
        /// Fires when the search button is clicked
        /// </summary>
        [Parameter]
        public EventCallback<string> OnSearch { get; set; }

        #endregion
    }
}
