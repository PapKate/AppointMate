﻿using MudBlazor;

using static MeetBase.Blazor.PaletteColors;

namespace MeetCore.Shared
{
    /// <summary>
    /// The main layout
    /// </summary>
    public partial class MainLayout
    {
        #region Private Members

        /// <summary>
        /// The theme provider
        /// </summary>
        private MudTheme? mMeetCoreTheme;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainLayout() : base()
        {
            mMeetCoreTheme = new MudTheme()
            {
                Typography = new Typography()
                {
                    Default = new Default()
                    {
                        FontFamily = new[] { "Objektiv VF Trial", "sans-serif" }
                    }
                },
                Palette = new()
                {
                    Primary = Amber,
                    PrimaryContrastText = DarkGray,
                    Secondary = Persimmon,
                    SecondaryContrastText = White,
                    Tertiary = Pink,
                    TertiaryContrastText = White,
                    Info = Blue,
                    InfoContrastText = White,
                    Success = Green,
                    SuccessContrastText = White,
                    Warning = Yellow,
                    WarningContrastText = DarkGray,
                    Error = Red,
                    ErrorContrastText = White,
                    Dark = DarkGray,
                    DarkContrastText = White,
                    TextPrimary = DarkGray,
                    TextSecondary = Gray,
                    TextDisabled = LightGray,
                    ActionDisabledBackground = LightGray,
                }
            };
        }

        #endregion
    }
}
