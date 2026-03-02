namespace DeviceMaintenanceApi.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Models;

	public class InMemoryRepository : IRepository
	{
		private readonly List<Device> _devices;
		private readonly List<MaintenanceWindow> _maintenanceWindows;

		public InMemoryRepository()
		{
			_devices = DemoSeedData.GetDevices();
			_maintenanceWindows = DemoSeedData.GetMaintenanceWindows();
		}

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

		public IEnumerable<Device> GetDevices() => _devices;

		private Device GetDevice(Guid id) => _devices.FirstOrDefault(d => d.Id == id);

		Device IRepository.GetDevice(Guid id)
		{
			var device = GetDevice(id);
			return device is null ? null : CloneDevice(device);
		}

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
			_devices.Add(deviceClone);
			return deviceClone;
		}

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

		public void DeleteDevice(Guid id)
		{
			var d = GetDevice(id);
			if (d is null)
			{
				throw new InvalidOperationException("Device not found");
			}

			_devices.Remove(d);
		}

		public IEnumerable<MaintenanceWindow> GetMaintenancesByDevice(Guid deviceId)
			=> _maintenanceWindows.Where(mw => mw.DeviceId == deviceId).Select(CloneMaintenanceWindow);

		private MaintenanceWindow GetMaintenance(Guid id)
			=> _maintenanceWindows.FirstOrDefault(mw => mw.Id == id);

		MaintenanceWindow IRepository.GetMaintenance(Guid id)
		{
			var maintenance = GetMaintenance(id);
			return maintenance is null ? null : CloneMaintenanceWindow(maintenance);
		}

		public MaintenanceWindow CreateMaintenance(MaintenanceWindow maintenanceWindow)
		{
			if (maintenanceWindow == null) throw new ArgumentNullException(nameof(maintenanceWindow));

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

			var mwClone = CloneMaintenanceWindow(maintenanceWindow);
			_maintenanceWindows.Add(mwClone);
			return CloneMaintenanceWindow(mwClone);
		}

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

		public void DeleteMaintenance(Guid id)
		{
			var mw = _maintenanceWindows.FirstOrDefault(x => x.Id == id);
			if (mw is null)
			{
				throw new InvalidOperationException("Maintenance window not found");
			}

			_maintenanceWindows.Remove(mw);
		}
	}
}