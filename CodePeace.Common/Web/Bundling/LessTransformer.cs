using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using dotless.Core;
using dotless.Core.Abstractions;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Loggers;
using dotless.Core.Parser;

namespace CodePeace.Common.Web.Bundling
{
    public class LessTransformer : ILessTransformer
    {
        private readonly bool _compress;
        private readonly bool _debug;

        public LessTransformer()
        {

        }

        public LessTransformer(bool compress, bool debug)
        {
            _compress = compress;
            _debug = debug;
        }

        public SourceTransformation Transform(IEnumerable<FileInfo> files)
        {
            var lessParser = new Parser();
            var lessEngine = CreateLessEngine(lessParser);
            var bundleFiles = new List<FileInfo>();
            var content = new StringBuilder();

            foreach (var bundleFile in files)
            {
                bundleFiles.Add(bundleFile);

                var css = TransformToCss(lessParser, bundleFile, lessEngine);
                content.AppendFormat("{0}\n", css);
                SetCurrentFilePath(lessParser, bundleFile.FullName);
                var source = File.ReadAllText(bundleFile.FullName);
                content.Append(lessEngine.TransformToCss(source, bundleFile.FullName));
                content.AppendLine();

                bundleFiles.AddRange(GetFileDependencies(lessParser));
            }

            return new SourceTransformation
            {
                Dependencies = bundleFiles,
                Source = content.ToString()
            };
        }

        private static string TransformToCss(Parser lessParser, FileSystemInfo bundleFile, ILessEngine lessEngine)
        {
            SetCurrentFilePath(lessParser, bundleFile.FullName);

            var source = File.ReadAllText(bundleFile.FullName);
            var css = lessEngine.TransformToCss(source, bundleFile.FullName);

            return css;
        }

        private ILessEngine CreateLessEngine(Parser lessParser)
        {
            var logger = new AspNetTraceLogger(LogLevel.Debug, new Http());

            return new LessEngine(lessParser, logger, _compress, _debug);
        }

        private IEnumerable<FileInfo> GetFileDependencies(Parser lessParser)
        {
            var pathResolver = GetPathResolver(lessParser);

            foreach (var importPath in lessParser.Importer.Imports)
            {
                yield return new FileInfo(pathResolver.GetFullPath(importPath));
            }

            lessParser.Importer.Imports.Clear();
        }

        private static void SetCurrentFilePath(Parser lessParser, string currentFilePath)
        {
            var importer = lessParser.Importer as Importer;

            if (importer == null)
            {
                throw new InvalidOperationException("Unexpected dotless importer type.");
            }

            var fileReader = importer.FileReader as FileReader;

            if (fileReader != null && fileReader.PathResolver is ImportedFilePathResolver)
            {
                return;
            }

            fileReader = new FileReader(new ImportedFilePathResolver(currentFilePath));
            importer.FileReader = fileReader;
        }

        public IPathResolver GetPathResolver(Parser lessParser)
        {
            if (fileReader == null || !(fileReader.PathResolver is ImportedFilePathResolver))
            {
                fileReader = new FileReader(new ImportedFilePathResolver(currentFilePath));
                importer.FileReader = fileReader;
            }
        }
    }
}

