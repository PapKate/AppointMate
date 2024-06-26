﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using static MeetBase.Blazor.CssVariables;

namespace MeetEdu
{
    /// <summary>
    /// The department preview
    /// </summary>
    public partial class DepartmentPreview
    {
        #region Public Properties

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public DepartmentResponseModel? Model { get; set; }

        /// <summary>
        /// A flag indicating whether it is a preview or not
        /// </summary>
        [Parameter]
        public bool IsPreview { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// The JS runtime service
        /// </summary>
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentPreview() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if(Model!.Location is not null)
                    await JSRuntime.InvokeVoidAsync("ShowLeafletMap", Model!.Id, Model.Location.Latitude, Model.Location.Longitude);
            }

        }

        #endregion

        #region Private Methods

        private string SetStyle()
        {
            if (IsPreview)
            {

                return $"cursor: pointer; {BorderBrushVariable.SetCssColor(Model!.Color)}";
            }
            else
            {
                return "border: none;";
            }
        }

        private void DepartmentContainer_OnClick()
        {
            if (!IsPreview)
                return;
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            NavigationManager.NavigateToDepartmentPage(Model!.Id);
        }

        #endregion
    }
}
