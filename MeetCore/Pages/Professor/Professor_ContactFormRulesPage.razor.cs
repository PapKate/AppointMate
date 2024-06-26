﻿using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The contact form rules page
    /// </summary>
    public partial class Professor_ContactFormRulesPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        private readonly List<AppointmentRuleResponseModel> mRules = new();

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorResponseModel Professor => StateManager.Professor!;

        /// <summary>
        /// The department
        /// </summary>
        public DepartmentResponseModel Department => StateManager.Department!;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The dialog service
        /// </summary>
        [Inject]
        protected IDialogService DialogService { get; set; } = default!;

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
        public Professor_ContactFormRulesPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var response = await Client.GetAppointmentRulesAsync(new() { IncludeProfessors = new List<string>() { StateManager.Professor!.Id } });

            // If there was an error...
            if (!response.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Info);
                // Return
                return;
            }

            if (response.Result.IsNullOrEmpty())
            {
                // Show the error
                Snackbar.Add("No rules", Severity.Info);
                // Return
                return;
            }

            mRules.AddRange(response.Result);

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        private async void AddRule()
        {
            var result = await OpenRuleDialog(new AppointmentRuleRequestModel() { ProfessorId = Professor.Id }, Client.AddAppointmentRuleAsync);

            if (result is not null)
            {
                mRules.Add(result);
                StateHasChanged();
            }
        }

        /// <summary>
        /// Edits the specified <paramref name="rule"/>
        /// </summary>
        /// <param name="rule">The rule</param>
        private async void EditRule(AppointmentRuleResponseModel rule)
        {
            var model = new AppointmentRuleRequestModel()
            {
                Color = rule.Color,
                Name = rule.Name,
                Note = rule.Note,
                Description = rule.Description,
                Duration = rule.Duration,
                HasRemoteOption = rule.HasRemoteOption,
                ProfessorId = Professor.Id,
                DateFrom = rule.DateFrom,
                DateTo = rule.DateTo,
                StartMinutes = rule.StartMinutes
            };

            var result = await OpenRuleDialog(model, async (model) => await Client.UpdateAppointmentRuleAsync(rule.Id, model));

            if (result is not null)
            {
                var index = mRules.IndexOf(rule);
                mRules.Remove(rule);
                mRules.Insert(index, result);
                StateHasChanged();
            }
        }

        /// <summary>
        /// Removes the specified <paramref name="rule"/>
        /// </summary>
        /// <param name="rule">The rule</param>
        private async void RemoveRule(AppointmentRuleResponseModel rule)
        {
            var response = await Client.DeleteAppointmentRuleAsync(rule.Id);

            // If there was an error...
            if (!response.IsSuccessful)
            {
                Console.WriteLine(response.ErrorMessage);
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Error);
                // Return
                return;
            }

            if (response.Result is not null)
            {
                mRules.Remove(rule);
                StateHasChanged();
            }
            // Return the rule
            return;
        }

        private async Task<AppointmentRuleResponseModel?> OpenRuleDialog(AppointmentRuleRequestModel model,
                                                                     Func<AppointmentRuleRequestModel, Task<WebRequestResult<AppointmentRuleResponseModel>>> requestAction)
        {
            var parameters = new DialogParameters<AppointmentRuleDialog> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<AppointmentRuleDialog>(null, parameters, mDialogOptions);

            // Once the dialog is closed...
            // Gets the result
            var result = await dialog.Result;

            // If there is no result or the dialog was closed by canceling the inner actions...
            if (result is null || result.Canceled)
            {
                // Return 
                return null;
            }

            // If the result is of the specified type...
            if (result.Data is AppointmentRuleRequestModel updatedModel)
            {
                // Updates the rule
                var ruleResponse = await requestAction(updatedModel);

                // If there was an error...
                if (!ruleResponse.IsSuccessful)
                {
                    Console.WriteLine(ruleResponse.ErrorMessage);
                    // Show the error
                    Snackbar.Add(ruleResponse.ErrorMessage, Severity.Error);
                    // Return
                    return null;
                }

                // Return the rule
                return ruleResponse.Result;
            }

            // Return
            return null;
        }

        #endregion
    }
}
