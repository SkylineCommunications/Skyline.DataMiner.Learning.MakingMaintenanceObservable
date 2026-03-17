namespace DeviceMaintenanceApi.Models
{
    /// <summary>
    /// Specifies the type of maintenance being performed during a maintenance window.
    /// </summary>
    public enum MaintenanceWindowType
    {
        /// <summary>
        /// Firmware update maintenance.
        /// </summary>
        FirmwareUpdate,

        /// <summary>
        /// Testing or validation maintenance.
        /// </summary>
        Testing,

        /// <summary>
        /// Hardware-related maintenance.
        /// </summary>
        Hardware,

        /// <summary>
        /// Other maintenance type.
        /// </summary>
        Other,
    }
}