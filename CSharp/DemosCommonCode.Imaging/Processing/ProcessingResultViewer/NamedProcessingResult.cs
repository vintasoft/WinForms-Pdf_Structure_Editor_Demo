using System.Collections.Generic;

using Vintasoft.Imaging.Processing;


namespace DemosCommonCode.Imaging
{
    /// <summary>
    /// Stores a named result of execution of a composite processing command.
    /// </summary>
    public class NamedProcessingResult : CompositeProcessingResult
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedProcessingResult"/> class.
        /// </summary>
        /// <param name="name">The name of processing command.</param>
        /// <param name="processingCommandInfo">The processing command information.</param>
        /// <param name="target">The processing target.</param>
        /// <param name="results">The processing results.</param>
        public NamedProcessingResult(
            string name,
            IProcessingCommandInfo processingCommandInfo,
            object target,
            ICollection<ProcessingResult> results)
            : base(processingCommandInfo, target, results)
        {
            _description = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedProcessingResult"/> class.
        /// </summary>
        /// <param name="parentProcessingResult">The parent processing result.</param>
        /// <param name="results">The processing results.</param>
        public NamedProcessingResult(
            ProcessingResult parentProcessingResult,
            ICollection<ProcessingResult> results)
            : this(parentProcessingResult.Description, parentProcessingResult.ProcessingCommand,
            parentProcessingResult.Target, results)
        {
            _parentProcessingResult = parentProcessingResult;
        }

        #endregion



        #region Properties

        string _description = string.Empty;
        /// <summary>
        /// Gets the description of processing result.
        /// </summary>
        /// <value>
        /// Default value is <see cref="System.String"/>.Empty.
        /// </value>
        public override string Description
        {
            get
            {
                return _description;
            }
        }

        ProcessingResult _parentProcessingResult = null;
        /// <summary>
        /// Gets the parent processing result.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        public ProcessingResult ParentProcessingResult
        {
            get
            {
                return _parentProcessingResult;
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Releases all resources used by this object.
        /// </summary>
        public override void Dispose()
        {
        }

        #endregion

    }
}