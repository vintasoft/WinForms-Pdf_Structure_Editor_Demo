using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Processing;
using Vintasoft.Imaging.Processing.Analyzers;

using DemosCommonCode.Imaging;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view the PDF document processing commands.
    /// </summary>
    public partial class DocumentProcessingCommandForm : Form
    {

        #region Fields

        /// <summary>
        /// The preview selected menu item.
        /// </summary>
        ToolStripMenuItem _prevMenuItem = null;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentProcessingCommandForm"/> class.
        /// </summary>
        /// <param name="processingTarget">The processing target.</param>
        /// <param name="processingCommands">The processing commands.</param>
        public DocumentProcessingCommandForm(
            PdfDocument processingTarget,
            params IProcessingCommand<PdfDocument>[] processingCommands)
        {
            InitializeComponent();

            detailedToolStripMenuItem.Tag = ProcessingResultTreeType.Detailed;
            byPageToolStripMenuItem.Tag = ProcessingResultTreeType.ByPage;
            UpdateProcessingResultViewType(detailedToolStripMenuItem);

            documentProcessingCommandControl.ProcessingTarget = processingTarget;
            documentProcessingCommandControl.ProcessingCommands = processingCommands;

            decreaseMemoryUsageToolStripMenuItem.Checked = !documentProcessingCommandControl.StorePredicateResults;
            fastModeToolStripMenuItem.Checked = documentProcessingCommandControl.ThrowTriggerActivatedException;
        }

        #endregion



        #region Porperties

        /// <summary>
        /// Gets or sets a value indicating whether the command viewer must
        /// be updated when property of selected command is changed.
        /// </summary>
        /// <value>
        /// <b>True</b> if command viewer must be updated when property of selected command is changed; otherwise, <b>false</b>.
        /// </value>
        public bool UpdateCommandViewerWhenPropertyChanged
        {
            get
            {
                return documentProcessingCommandControl.UpdateCommandViewerWhenPropertyChanged;
            }
            set
            {
                documentProcessingCommandControl.UpdateCommandViewerWhenPropertyChanged = value;
            }
        }

        #endregion



        #region Methods

        #region PUBLIC, STATIC

        /// <summary>
        /// Gets the full names of processing commands,
        /// processed the full names of processing commands
        /// by excluding the specified root namespace from fullnames of processing commands,
        /// creates the hierarchical submenus based on processed names of processing commands.
        /// </summary>
        /// <param name="rootMenu">The root menu, where new submenus must be added.</param>
        /// <param name="rootNamespace">The root namespace, which must be excluded from
        /// the full names of processing commands.</param>
        /// <param name="getProcessingTargetDelegate">The delegate that allows to get
        /// the target for the processing commands.</param>
        /// <param name="processingCommands">The processing commands
        /// for which submenus must be created.</param>
        public static void BuildMenu(
            ToolStripMenuItem rootMenu,
            string rootNamespace,
            ProcessingCommandForm<PdfDocument>.GetProcessingTargetDelegate getProcessingTargetDelegate,
            params IProcessingCommand<PdfDocument>[] processingCommands)
        {
            ProcessingCommandForm<PdfDocument>.BuildMenu(
                rootMenu,
                rootNamespace,
                getProcessingTargetDelegate,
                CreateProcessingCommandsForm,
                processingCommands);
        }

        /// <summary>
        /// Executes the processing of PDF document using the specified processing command.
        /// </summary>
        /// <param name="processingTarget">The target of processing.</param>
        /// <param name="command">The processing command.</param>
        public static void ExecuteDocumentProcessing(
            PdfDocument processingTarget,
            IProcessingCommand<PdfDocument> command)
        {
            ExecuteDocumentProcessing(processingTarget, command, true);
        }

        /// <summary>
        /// Executes the processing of PDF document using the specified processing command.
        /// </summary>
        /// <param name="processingTarget">The target of processing.</param>
        /// <param name="command">The processing command.</param>
        /// <param name="showDialog">Indicates whether need show processing dialog.</param>
        public static void ExecuteDocumentProcessing(
            PdfDocument processingTarget,
            IProcessingCommand<PdfDocument> command,
            bool showDialog)
        {
            ExecuteDocumentProcessing(processingTarget, command, showDialog, false);
        }

        /// <summary>
        /// Executes the processing of PDF document using the specified processing command.
        /// </summary>
        /// <param name="processingTarget">The target of processing.</param>
        /// <param name="command">The processing command.</param>
        /// <param name="showDialog">Indicates whether need show processing dialog.</param>
        /// <param name="updateCommandViewerAfterPropertyChanged">
        /// Indicates whether the command viewer must be updated when property of selected command is changed.
        /// </param>
        public static void ExecuteDocumentProcessing(
            PdfDocument processingTarget,
            IProcessingCommand<PdfDocument> command,
            bool showDialog,
            bool updateCommandViewerAfterPropertyChanged)
        {
            if (showDialog)
            {
                using (DocumentProcessingCommandForm form =
                    new DocumentProcessingCommandForm(processingTarget, command))
                {
                    form.UpdateCommandViewerWhenPropertyChanged = updateCommandViewerAfterPropertyChanged;

                    form.ShowDialog();
                }
            }
            else
            {
                ProcessingDemosTools.ExecuteProcessing(processingTarget, command);
            }
        }

        #endregion


        #region PRIVATE, STATIC

        /// <summary>
        /// Creates the processing commands form.
        /// </summary>
        /// <param name="processingTarget">The processing target.</param>
        /// <param name="processingCommands">The processing commands.</param>
        private static Form CreateProcessingCommandsForm(
            PdfDocument processingTarget,
            IProcessingCommand<PdfDocument>[] processingCommands)
        {
            return new DocumentProcessingCommandForm(processingTarget, processingCommands);
        }

        #endregion


        #region PRIVATE

        #region UI

        /// <summary>
        /// Handles the Click event of ProcessingResultViewToolStripMenuItem object.
        /// </summary>
        private void ProcessingResultViewToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // change the view type of processing result tree
            UpdateProcessingResultViewType((ToolStripMenuItem)sender);
        }

        /// <summary>
        /// Handles the CheckedChanged event of DecreaseMemoryUsageToolStripMenuItem object.
        /// </summary>
        private void decreaseMemoryUsageToolStripMenuItem_CheckedChanged(
            object sender,
            System.EventArgs e)
        {
            // if processing results must store predicate results of triggers
            if (decreaseMemoryUsageToolStripMenuItem.Checked)
                documentProcessingCommandControl.StorePredicateResults = false;
            else
                documentProcessingCommandControl.StorePredicateResults = true;
        }

        /// <summary>
        /// Handles the CheckedChanged event of FastModeToolStripMenuItem object.
        /// </summary>
        private void fastModeToolStripMenuItem_CheckedChanged(object sender, System.EventArgs e)
        {
            // if exception must be thrown if an important trigger is activated
            if (fastModeToolStripMenuItem.Checked)
                documentProcessingCommandControl.ThrowTriggerActivatedException = true;
            else
                documentProcessingCommandControl.ThrowTriggerActivatedException = false;
        }

        #endregion


        /// <summary>
        /// Updates the type of the processing result view.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        private void UpdateProcessingResultViewType(ToolStripMenuItem menuItem)
        {
            // if view type is not changed
            if (_prevMenuItem == menuItem)
                return;

            if (_prevMenuItem != null)
                // uncheck preview menu item
                _prevMenuItem.Checked = false;

            // check current menu item
            menuItem.Checked = true;
            // change view type
            documentProcessingCommandControl.ViewType = (ProcessingResultTreeType)menuItem.Tag;
            _prevMenuItem = menuItem;
        }

        #endregion

        #endregion

    }
}
