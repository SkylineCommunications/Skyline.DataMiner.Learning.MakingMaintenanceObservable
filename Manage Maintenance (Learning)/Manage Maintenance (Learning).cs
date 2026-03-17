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
	using Skyline.DataMiner.Automation;

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
				engine.Timeout = TimeSpan.FromHours(1.5);
				var manageMaintenanceController = new ManageMaintenanceController(engine);
				manageMaintenanceController.ShowMaintenanceOverview();
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