namespace DeviceMaintenanceApi.Models
{
    /// <summary>
    /// Specifies the impact level of a maintenance window on device operations.
    /// </summary>
    public enum MaintenanceWindowImpact
    {
        /// <summary>
        /// Normal impact level.
        /// </summary>
        Normal,

        /// <summary>
        /// Low impact level.
        /// </summary>
        Low,

        /// <summary>
        /// High impact level.
        /// </summary>
        High,
    }
}