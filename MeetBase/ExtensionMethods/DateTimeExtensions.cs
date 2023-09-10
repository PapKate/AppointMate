﻿namespace MeetBase
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns a string that represents the specified <paramref name="dt"/> using the ISO8601 format
        /// </summary>
        /// <param name="dt">The date time</param>
        /// <returns></returns>
        public static string ToISO8601String(this DateTime dt) => dt.ToString(LocalizationConstants.ISO8601Format, LocalizationConstants.Culture);

        /// <summary>
        /// Returns a string that represents the specified <paramref name="dto"/> using the ISO8601 format
        /// </summary>
        /// <param name="dto">The date time offset</param>
        /// <returns></returns>
        public static string ToISO8601String(this DateTimeOffset dto) => dto.LocalDateTime.ToString(LocalizationConstants.ISO8601Format, LocalizationConstants.Culture);

        #endregion
    }
}
