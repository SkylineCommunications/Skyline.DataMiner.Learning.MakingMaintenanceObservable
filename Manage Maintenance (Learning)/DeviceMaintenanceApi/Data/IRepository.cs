namespace DeviceMaintenanceApi.Data
{
	using System;
	using System.Collections.Generic;
	using DeviceMaintenanceApi.Models;

	/// <summary>
	/// Defines the contract for a repository that manages devices and their maintenance windows.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Gets all devices from the repository.
		/// </summary>
		/// <returns>A collection of all devices.</returns>
		IEnumerable<Device> GetDevices();

		/// <summary>
		/// Gets a specific device by its identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the device.</param>
		/// <returns>The device with the specified identifier, or null if not found.</returns>
		Device GetDevice(Guid id);

		/// <summary>
		/// Creates a new device in the repository.
		/// </summary>
		/// <param name="device">The device to create.</param>
		/// <returns>The created device.</returns>
		Device CreateDevice(Device device);

		/// <summary>
		/// Updates an existing device in the repository.
		/// </summary>
		/// <param name="device">The device with updated information.</param>
		/// <returns>The updated device, or null if not found.</returns>
		Device UpdateDevice(Device device);

		/// <summary>
		/// Deletes a device from the repository.
		/// </summary>
		/// <param name="id">The unique identifier of the device to delete.</param>
		void DeleteDevice(Guid id);

		/// <summary>
		/// Gets all maintenance windows for a specific device.
		/// </summary>
		/// <param name="deviceId">The unique identifier of the device.</param>
		/// <returns>A collection of maintenance windows for the specified device.</returns>
		IEnumerable<MaintenanceWindow> GetMaintenancesByDevice(Guid deviceId);

		/// <summary>
		/// Gets a specific maintenance window by its identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the maintenance window.</param>
		/// <returns>The maintenance window with the specified identifier, or null if not found.</returns>
		MaintenanceWindow GetMaintenance(Guid id);

		/// <summary>
		/// Creates a new maintenance window in the repository.
		/// </summary>
		/// <param name="maintenanceWindow">The maintenance window to create.</param>
		/// <returns>The created maintenance window, or null if the associated device doesn't exist.</returns>
		MaintenanceWindow CreateMaintenance(MaintenanceWindow maintenanceWindow);

		/// <summary>
		/// Updates an existing maintenance window in the repository.
		/// </summary>
		/// <param name="maintenanceWindow">The maintenance window with updated information.</param>
		/// <returns>The updated maintenance window, or null if not found or the associated device doesn't exist.</returns>
		MaintenanceWindow UpdateMaintenance(MaintenanceWindow maintenanceWindow);

		/// <summary>
		/// Deletes a maintenance window from the repository.
		/// </summary>
		/// <param name="id">The unique identifier of the maintenance window to delete.</param>
		void DeleteMaintenance(Guid id);
	}
}