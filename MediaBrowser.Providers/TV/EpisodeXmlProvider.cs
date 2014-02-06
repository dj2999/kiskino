﻿using MediaBrowser.Common.IO;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Logging;
using System.IO;
using System.Threading;

namespace MediaBrowser.Providers.TV
{
    public class EpisodeXmlProvider : BaseXmlProvider<Episode>
    {
        private readonly ILogger _logger;

        public EpisodeXmlProvider(IFileSystem fileSystem, ILogger logger)
            : base(fileSystem)
        {
            _logger = logger;
        }

        protected override void Fetch(Episode item, string path, CancellationToken cancellationToken)
        {
            new EpisodeXmlParser(_logger).Fetch(item, path, cancellationToken);
        }

        protected override FileInfo GetXmlFile(ItemInfo info)
        {
            var metadataPath = Path.GetDirectoryName(info.Path);
            metadataPath = Path.Combine(metadataPath, "metadata");
            var metadataFile = Path.Combine(metadataPath, Path.ChangeExtension(Path.GetFileName(info.Path), ".xml"));

            return new FileInfo(metadataFile);
        }
    }
}
