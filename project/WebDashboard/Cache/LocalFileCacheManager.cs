using System.IO;
using ThoughtWorks.CruiseControl.WebDashboard.Dashboard;
using ThoughtWorks.CruiseControl.WebDashboard.IO;

namespace ThoughtWorks.CruiseControl.WebDashboard.Cache
{
	// This is currently not used and only here as a basis to develop a new caching strategy
	public class LocalFileCacheManager : ICacheManager
	{
		private readonly IPathMapper pathMapper;

		public LocalFileCacheManager(IPathMapper pathMapper)
		{
			this.pathMapper = pathMapper;
		}

		public void AddContent(IProjectSpecifier projectSpecifier, string directory, string fileName, string content)
		{
			string localFilePath = GetLocalPathForFile(projectSpecifier, directory, fileName);
			CreateDirectoryForFileIfNecessary(localFilePath);
			using (StreamWriter sw = new StreamWriter(localFilePath)) 
			{
				sw.Write(content);
			}
		}

		public string GetContent(IProjectSpecifier projectSpecifier, string directory, string fileName)
		{
			string filepath = GetLocalPathForFile(projectSpecifier, directory, fileName);

			if (File.Exists(filepath))
			{
				return ReadFile(filepath);
			}
			else
			{
				return null;	
			}
		}

		public string GetURLForFile(IProjectSpecifier projectSpecifier, string directory, string fileName)
		{
			return pathMapper.GetAbsoluteURLForRelativePath(GetRelativePathForFile(projectSpecifier, directory, fileName));
		}

		private string GetLocalPathForFile(IProjectSpecifier projectSpecifier, string directory, string fileName)
		{
			return pathMapper.GetLocalPathFromURLPath(GetRelativePathForFile(projectSpecifier, directory, fileName));
		}

		private string GetRelativePathForFile(IProjectSpecifier projectSpecifier, string directory, string fileName)
		{
			string cacheRootDirectory = "cache";
			return Path.Combine(Path.Combine(Path.Combine(Path.Combine(cacheRootDirectory, projectSpecifier.ServerSpecifier.ServerName), projectSpecifier.ProjectName), directory), fileName);
		}

		private void CreateDirectoryForFileIfNecessary(string fullLocalPathOfFile)
		{
			DirectoryInfo dir = new FileInfo(fullLocalPathOfFile).Directory;
			if (! dir.Exists)
			{
				dir.Create();
			}
		}

		private string ReadFile(string path)
		{
			using (StreamReader sr = new StreamReader(path)) 
			{
				return sr.ReadToEnd();
			}
		}
	}
}
