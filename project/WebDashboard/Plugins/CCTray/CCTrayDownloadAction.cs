using System.IO;
using ThoughtWorks.CruiseControl.Core.Reporting.Dashboard.Navigation;
using ThoughtWorks.CruiseControl.WebDashboard.IO;
using ThoughtWorks.CruiseControl.WebDashboard.MVC;
using ThoughtWorks.CruiseControl.WebDashboard.MVC.Cruise;

namespace ThoughtWorks.CruiseControl.WebDashboard.Plugins.CCTray
{
	public class CCTrayDownloadAction : ICruiseAction
	{
		public const string ActionName = "CCTrayDownload";
		private readonly IPhysicalApplicationPathProvider physicalApplicationPathProvider;

		public CCTrayDownloadAction(IPhysicalApplicationPathProvider physicalApplicationPathProvider)
		{
			this.physicalApplicationPathProvider = physicalApplicationPathProvider;
		}

		public IResponse Execute(ICruiseRequest cruiseRequest)
		{
			DirectoryInfo cctrayPath = new DirectoryInfo(Path.Combine(physicalApplicationPathProvider.PhysicalApplicationPath, "CCTray"));
			if (cctrayPath.Exists)
			{
				FileInfo[] files = cctrayPath.GetFiles("*CCTray*.*");
				if (files.Length > 0)
				{
					return new RedirectResponse("CCTray/" + files[0].Name);
				}
			}
			return new HtmlFragmentResponse("<h3>Unable to locate CCTray installer at path: " + cctrayPath + "</h3>");
		}
	}
}