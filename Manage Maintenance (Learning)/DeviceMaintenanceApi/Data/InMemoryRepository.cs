namespace DeviceMaintenanceApi.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DeviceMaintenanceApi.Models;

	/// <summary>
	/// Provides an in-memory implementation of the repository for managing devices and maintenance windows.
	/// </summary>
	public class InMemoryRepository : IRepository
	{
		private readonly List<Device> devices;
		private readonly List<MaintenanceWindow> maintenanceWindows;

		/// <summary>
		/// Initializes a new instance of the <see cref="InMemoryRepository"/> class with demo seed data.
		/// </summary>
		public InMemoryRepository()
		{
			devices = DemoSeedData.GetDevices();
			maintenanceWindows = DemoSeedData.GetMaintenanceWindows();
		}

		/// <summary>
		/// Creates a deep copy of the specified device.
		/// </summary>
		/// <param name="device">The device to clone.</param>
		/// <returns>A new device instance with the same property values.</returns>
		public static Device CloneDevice(Device device)
		{
			if (device is null)
			{
				throw new ArgumentNullException(nameof(device));
			}

			return new Device
			{
				Id = device.Id,
				Name = device.Name,
				Description = device.Description,
			};
		}

		/// <summary>
		/// Creates a deep copy of the specified maintenance window.
		/// </summary>
		/// <param name="maintenanceWindow">The maintenance window to clone.</param>
		/// <returns>A new maintenance window instance with the same property values.</returns>
		public static MaintenanceWindow CloneMaintenanceWindow(MaintenanceWindow maintenanceWindow)
		{
			if (maintenanceWindow == null)
			{
				throw new ArgumentNullException(nameof(maintenanceWindow));
			}

			return new MaintenanceWindow
			{
				Id = maintenanceWindow.Id,
				Description = maintenanceWindow.Description,
				DeviceId = maintenanceWindow.DeviceId,
				Start = maintenanceWindow.Start,
				End = maintenanceWindow.End,
				Type = maintenanceWindow.Type,
				Impact = maintenanceWindow.Impact,
			};
		}

		/// <inheritdoc />
		public IEnumerable<Device> GetDevices() => devices;

		/// <inheritdoc />
		public Device CreateDevice(Device device)
		{
			if (device.Id == Guid.Empty)
			{
				device.Id = Guid.NewGuid();
			}
			else
			{
				// If the device already exists, we can update it
				var existing = GetDevice(device.Id);
				if (existing != null)
				{
					throw new InvalidOperationException("Device with the same ID already exists.");
				}
			}

			var deviceClone = CloneDevice(device);
			devices.Add(deviceClone);
			return deviceClone;
		}

		/// <inheritdoc />
		public Device UpdateDevice(Device updated)
		{
			var existing = GetDevice(updated.Id);
			if (existing == null)
			{
				return null;
			}

			existing.Name = updated.Name;
			existing.Description = updated.Description;
			return CloneDevice(existing);
		}

		/// <inheritdoc />
		public void DeleteDevice(Guid id)
		{
			var d = GetDevice(id);
			if (d is null)
			{
				throw new InvalidOperationException("Device not found");
			}

			devices.Remove(d);
		}

		/// <inheritdoc />
		public IEnumerable<MaintenanceWindow> GetMaintenancesByDevice(Guid deviceId)
			=> maintenanceWindows.Where(mw => mw.DeviceId == deviceId).Select(CloneMaintenanceWindow);

		/// <inheritdoc />
		public MaintenanceWindow CreateMaintenance(MaintenanceWindow maintenanceWindow)
		{
			if (maintenanceWindow == null)
			{
				throw new ArgumentNullException(nameof(maintenanceWindow));
			}

			// Ensure the device exists
			var device = GetDevice(maintenanceWindow.DeviceId);
			if (device == null)
			{
				return null;
			}

			if (maintenanceWindow.Id == Guid.Empty)
			{
				maintenanceWindow.Id = Guid.NewGuid();
			}
			else
			{
				var existing = GetMaintenance(maintenanceWindow.Id);
				if (existing != null)
				{
					throw new InvalidOperationException("Maintenance window with the same ID already exists.");
				}
			}

			var cloneMaintenanceWindow = CloneMaintenanceWindow(maintenanceWindow);
			maintenanceWindows.Add(cloneMaintenanceWindow);
			return CloneMaintenanceWindow(cloneMaintenanceWindow);
		}

		/// <inheritdoc />
		public MaintenanceWindow UpdateMaintenance(MaintenanceWindow updated)
		{
			if (updated is null)
			{
				throw new ArgumentNullException(nameof(updated));
			}

			var existing = GetMaintenance(updated.Id);
			if (existing is null)
			{
				throw new InvalidOperationException($"Maintenance window with ID {updated.Id} does not exist.");
			}

			// Ensure the target device exists
			var device = GetDevice(updated.DeviceId);
			if (device == null)
			{
				return null;
			}

			existing.DeviceId = updated.DeviceId;
			existing.Start = updated.Start;
			existing.End = updated.End;
			existing.Description = updated.Description;
			existing.Type = updated.Type;
			existing.Impact = updated.Impact;

			return CloneMaintenanceWindow(existing);
		}

		/// <inheritdoc />
		public void DeleteMaintenance(Guid id)
		{
			var mw = maintenanceWindows.FirstOrDefault(x => x.Id == id);
			if (mw is null)
			{
				throw new InvalidOperationException("Maintenance window not found");
			}

			maintenanceWindows.Remove(mw);
		}

		/// <inheritdoc />
		Device IRepository.GetDevice(Guid id)
		{
			var device = GetDevice(id);
			return device is null ? null : CloneDevice(device);
		}

		/// <inheritdoc />
		MaintenanceWindow IRepository.GetMaintenance(Guid id)
		{
			var maintenance = GetMaintenance(id);
			return maintenance is null ? null : CloneMaintenanceWindow(maintenance);
		}

		/// <summary>
		/// Gets the internal device reference by its identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the device.</param>
		/// <returns>The internal device reference, or null if not found.</returns>
		private Device GetDevice(Guid id) => devices.FirstOrDefault(d => d.Id == id);

		/// <summary>
		/// Gets the internal maintenance window reference by its identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the maintenance window.</param>
		/// <returns>The internal maintenance window reference, or null if not found.</returns>
		private MaintenanceWindow GetMaintenance(Guid id)
			=> maintenanceWindows.FirstOrDefault(mw => mw.Id == id);
	}
}