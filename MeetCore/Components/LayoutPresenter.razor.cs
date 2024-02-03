﻿using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The presenter for a layout room
    /// </summary>
    public partial class LayoutPresenter
    {
        #region Private Members

        private string mLayoutImageTheme = "layoutRoomRightContainer";

        /// <summary>
        /// The member of the <see cref="Layout"/> property
        /// </summary>
        private DepartmentLayoutResponseModel? mLayout;

        #endregion

        #region Public Properties

        /// <summary>
        /// The layout room
        /// </summary>
        [Parameter]
        public DepartmentLayoutResponseModel? Layout 
        {
            get => mLayout;
            set
            {
                mLayout = value;
                SetLayoutRoomTheme();
            }
        }

        /// <summary>
        /// A flag indicating whether it is editable or not
        /// </summary>
        [Parameter]
        public bool IsEditable { get; set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// The client
        /// </summary>
        [Inject]
        protected MeetCoreClient Client { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LayoutPresenter() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Re-renders the component
        /// </summary>
        public void ReplaceLayout(DepartmentLayoutResponseModel layout)
        {
            Layout = layout;
        }

        #endregion

        #region Private Methods

        private void SetLayoutRoomTheme()
        {
            if (Layout is not null)
            {
                if (Layout.DisplayTheme == ImageDisplayTheme.Left)
                    mLayoutImageTheme = "layoutRoomLeftContainer";
                else if (Layout.DisplayTheme == ImageDisplayTheme.Right)
                    mLayoutImageTheme = "layoutRoomRightContainer";
                else
                    mLayoutImageTheme = "layoutRoomCenterContainer";
            }
        }

        private async void EditLayoutRoom()
        {
            await EditButtonClicked.InvokeAsync(Layout);
        }

        private async void DeleteLayoutRoom()
        {
            await DeleteButtonClicked.InvokeAsync(Layout);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the edit button is clicked
        /// </summary>
        [Parameter]
        public EventCallback<DepartmentLayoutResponseModel> EditButtonClicked { get; set; }

        /// <summary>
        /// Fires when the delete button is clicked
        /// </summary>
        [Parameter]
        public EventCallback<DepartmentLayoutResponseModel> DeleteButtonClicked { get; set; }

        #endregion
    }
}
