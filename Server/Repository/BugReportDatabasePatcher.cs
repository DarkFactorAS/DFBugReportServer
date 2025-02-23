using DFCommonLib.DataAccess;
using DFCommonLib.Logger;

namespace BugReportServer.Repository
{
    public class BugReportDatabasePatcher : StartupDatabasePatcher
    {
        private static string PATCHER = "BugReport";

        public BugReportDatabasePatcher(IDBPatcher dbPatcher) : base(dbPatcher)
        {
        }        

        public override bool RunPatcher()
        {
            base.RunPatcher();

            _dbPatcher.Patch(PATCHER,2, "CREATE TABLE `bugreports` ( `id` int(11) NOT NULL AUTO_INCREMENT, `title` varchar(2048) NOT NULL DEFAULT '', `message` varchar(2048) NOT NULL DEFAULT '', `email` varchar(100) NOT NULL DEFAULT '', `clientName` varchar(50) NOT NULL DEFAULT '',  `clientVersion` varchar(25) NOT NULL DEFAULT '',  `created` datetime NOT NULL,  `updated` datetime NOT NULL,  PRIMARY KEY (`id`))");
            _dbPatcher.Patch(PATCHER,3, "CREATE TABLE `bugreportfiles` ( `id` int(11) NOT NULL AUTO_INCREMENT, `bugId` int(11) NOT NULL DEFAULT 0, `filename` varchar(300) NOT NULL DEFAULT '', PRIMARY KEY (`id`))");
            return _dbPatcher.Successful();
        }
    }
}