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

namespace DrFoster.Common.Web.Bundling
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

        /// <summary>
        /// Creates an instance of LESS engine.
        /// </summary>
        /// <param name="lessParser">The LESS parser.</param>
        private ILessEngine CreateLessEngine(Parser lessParser)
        {
            var logger = new AspNetTraceLogger(LogLevel.Debug, new Http());

            return new LessEngine(lessParser, logger, _compress, _debug);
        }

        /// <summary>
        /// Gets the file dependencies (@imports) of the LESS file being parsed.
        /// </summary>
        /// <param name="lessParser">The LESS parser.</param>
        /// <returns>An array of file references to the dependent file references.</returns>
        private IEnumerable<FileInfo> GetFileDependencies(Parser lessParser)
        {
            var pathResolver = GetPathResolver(lessParser);

            foreach (var importPath in lessParser.Importer.Imports)
            {
                yield return new FileInfo(pathResolver.GetFullPath(importPath));
            }

            lessParser.Importer.Imports.Clear();
        }

        /// <summary>
        /// Informs the LESS parser about the path to the currently processed file. 
        /// This is done by using a custom <see cref="IPathResolver"/> implementation.
        /// </summary>
        /// <param name="lessParser">The LESS parser.</param>
        /// <param name="currentFilePath">The path to the currently processed file.</param>
        private void SetCurrentFilePath(Parser lessParser, string currentFilePath)
        {
            var importer = lessParser.Importer as Importer;

            if (importer == null)
            {
                throw new InvalidOperationException("Unexpected dotless importer type.");
            }

            var fileReader = importer.FileReader as FileReader;

            if (fileReader == null || !(fileReader.PathResolver is ImportedFilePathResolver))
            {
                fileReader = new FileReader(new ImportedFilePathResolver(currentFilePath));
                importer.FileReader = fileReader;
            }
        }

        /// <summary>
        /// Returns an <see cref="IPathResolver"/> instance used by the specified LESS lessParser.
        /// </summary>
        /// <param name="lessParser">The LESS parser.</param>
        private IPathResolver GetPathResolver(Parser lessParser)
        {
            var importer = lessParser.Importer as Importer;
            var fileReader = importer.FileReader as FileReader;

            return fileReader.PathResolver;
        }
    }
}
