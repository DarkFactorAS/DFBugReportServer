using DFCommonLib.DataAccess;
using DFCommonLib.Logger;

namespace BugReportServer.Repository
{
    public interface IStartupDatabasePatcher
    {
        bool RunPatcher();
    }

    public class StartupDatabasePatcher : IStartupDatabasePatcher
    {
        private static string PATCHER = "BugReport";

        private IDBPatcher _dbPatcher;

        public StartupDatabasePatcher(IDBPatcher dbPatcher)
        {
            _dbPatcher = dbPatcher;
        }

        public bool RunPatcher()
        {
            _dbPatcher.Init();
            _dbPatcher.Patch(PATCHER,1, "CREATE TABLE `logtable` ( `id` int(11) NOT NULL AUTO_INCREMENT, `created` datetime NOT NULL, `loglevel` int(11) NOT NULL, `groupname` varchar(100) NOT NULL DEFAULT '', `message` varchar(1024) NOT NULL DEFAULT '', PRIMARY KEY (`id`))");
            _dbPatcher.Patch(PATCHER,2, "CREATE TABLE `bugreports` ( `id` int(11) NOT NULL AUTO_INCREMENT, `title` varchar(2048) NOT NULL DEFAULT '', `message` varchar(2048) NOT NULL DEFAULT '', `email` varchar(100) NOT NULL DEFAULT '', `clientName` varchar(50) NOT NULL DEFAULT '',  `clientVersion` varchar(25) NOT NULL DEFAULT '',  `created` datetime NOT NULL,  `updated` datetime NOT NULL,  PRIMARY KEY (`id`))");
            _dbPatcher.Patch(PATCHER,3, "CREATE TABLE `bugreportfiles` ( `id` int(11) NOT NULL AUTO_INCREMENT, `bugId` int(11) NOT NULL DEFAULT 0, `filename` varchar(300) NOT NULL DEFAULT '', PRIMARY KEY (`id`))");
            return _dbPatcher.Successful();
        }
    }
}