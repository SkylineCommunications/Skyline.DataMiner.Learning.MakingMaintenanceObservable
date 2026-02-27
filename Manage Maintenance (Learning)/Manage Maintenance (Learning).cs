/*
***********************************************
*  Copyright (c), Skyline Communications NV.  *
***********************************************

Revision History:

DATE		VERSION		AUTHOR			COMMENTS

27-02-2026	1.0.0.1		PVP, Skyline	Initial version
****************************************************************************
*/

namespace Skyline.DataMiner.Learning.MakingMaintenanceObservable.ManageMaintenance
{
	using System;
	using DeviceMaintenanceApi.Data;
	using Dialogs.MaintenanceOverview;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;


	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			try
			{
				// Controls the event loop and switch between dialogs
				var controller = new InteractiveController(engine);

				// Create an instance of the dialog you wish to show.
				var repository = new InMemoryRepository();
				var maintenanceOverviewDialog = new MaintenanceOverviewDialog(engine);
				maintenanceOverviewDialog.Load(repository);

				// Starts the event loop and shows the first dialog.
				controller.ShowDialog(maintenanceOverviewDialog);
			}
			catch (ScriptAbortException)
			{
				throw;
			}
			catch (ScriptForceAbortException)
			{
				throw;
			}
			catch (ScriptTimeoutException)
			{
				throw;
			}
			catch (InteractiveUserDetachedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				engine.Log(ex.ToString());
				engine.ExitFail(ex.Message);
			}
		}
	}
}