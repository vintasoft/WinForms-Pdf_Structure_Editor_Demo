using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Vintasoft.Imaging.Processing;


namespace DemosCommonCode.Imaging
{
    /// <summary>
    /// A control that allows to view the processing commands.
    /// </summary>
    public class ProcessingCommandControl<TTarget> : ProcessingCommandControlBase
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingCommandControl{TTarget}"/> class.
        /// </summary>
        public ProcessingCommandControl()
            : base()
        {
        }

        #endregion



        #region Properties

        TTarget _processingTarget = default(TTarget);
        /// <summary>
        /// Gets or sets the processing target.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public TTarget ProcessingTarget
        {
            get
            {
                return _processingTarget;
            }
            set
            {
                _processingTarget = value;
            }
        }

        #endregion



        #region Methods

        #region PROTECTED

        /// <summary>
        /// Determines whether the specified command can be executed.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// <b>true</b> if the specified command can be executed;
        /// <b>false</b> if the specified command can NOT be executed.
        /// </returns>
        protected override bool CanExecute(IProcessingCommandInfo command)
        {
            if (_processingTarget != null && GetCommand(command) != null)
                return true;

            return false;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="processingState">The state of processing.</param>
        /// <returns>The command processing result.</returns>
        protected override ProcessingResult Execute(
            IProcessingCommandInfo command,
            ProcessingState processingState)
        {
            IProcessingCommand<TTarget> selectedProcessingCommand = GetCommand(command);
            ProcessingResult result = null;
            if (selectedProcessingCommand != null)
            {
                try
                {
                    result = selectedProcessingCommand.Execute(_processingTarget, processingState);
                }
                catch (TriggerActivationException ex)
                {
                    result = ex.ActivationResult;
                }
            }

            return result;
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Returns the processing command.
        /// </summary>
        /// <param name="commandInfo">Information about the processing command.</param>
        /// <returns>The processing command.</returns>
        private IProcessingCommand<TTarget> GetCommand(IProcessingCommandInfo commandInfo)
        {
            IProcessingCommand<TTarget> result = commandInfo as IProcessingCommand<TTarget>;
            if (result == null)
            {
                // get processing command tree node
                TreeNode node = processingCommandViewer.GetNodeFromProcessingCommand(commandInfo);
                if (node != null)
                {
                    // find the processing command parent

                    ProcessingCommandTree<TTarget> processingCommandTree = null;
                    do
                    {
                        processingCommandTree =
                            processingCommandViewer.GetProcessingCommandFromNode(node) as ProcessingCommandTree<TTarget>;
                        node = node.Parent;
                    }
                    while (node != null && processingCommandTree == null);

                    // if processing command tree is found
                    if (processingCommandTree != null)
                    {
                        try
                        {
                            // build the processing tree
                            List<IProcessingCommand<TTarget>> processingTree =
                                processingCommandTree.BuildProcessingTree(new IProcessingCommandInfo[] { commandInfo });

                            // if processing tree is built
                            if (processingTree != null && processingTree.Count > 0)
                            {
                                if (processingTree.Count == 1)
                                {
                                    return processingTree[0];
                                }
                                else
                                {
                                    CompositeProcessingCommand<TTarget> compositeCommand =
                                        new CompositeProcessingCommand<TTarget>(processingTree);
                                    return compositeCommand;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #endregion

    }
}
