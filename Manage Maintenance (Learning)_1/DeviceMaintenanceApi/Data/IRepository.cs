namespace DeviceMaintenanceApi.Data
{
	using System;
	using System.Collections.Generic;
	using DeviceMaintenanceApi.Models;

	public interface IRepository
	{
		IEnumerable<Device> GetDevices();

		Device GetDevice(int id);

		Device CreateDevice(Device device);

		Device UpdateDevice(Device device);

		void DeleteDevice(int id);

		IEnumerable<MaintenanceWindow> GetMaintenanceByDevice(Guid deviceId);

		MaintenanceWindow GetMaintenance(Guid id);

		MaintenanceWindow CreateMaintenance(MaintenanceWindow maintenanceWindow);

		MaintenanceWindow UpdateMaintenance(MaintenanceWindow maintenanceWindow);

		void DeleteMaintenance(Guid id);
	}
}