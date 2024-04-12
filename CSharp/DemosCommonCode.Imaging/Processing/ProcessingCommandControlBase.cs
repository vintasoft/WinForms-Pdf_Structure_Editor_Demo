using System;
using System.ComponentModel;
using System.Windows.Forms;

using Vintasoft.Imaging.Processing;
using Vintasoft.Imaging.Utils;


namespace DemosCommonCode.Imaging
{
    /// <summary>
    /// A base control that allows to view the processing commands.
    /// </summary>
    public partial class ProcessingCommandControlBase : UserControl
    {

        #region Fields

        /// <summary>
        /// The parent form.
        /// </summary>
        Form _parentForm = null;

        /// <summary>
        /// The result of processing command.
        /// </summary>
        ProcessingResult _processingCommandResult = null;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingCommandControlBase"/> class.
        /// </summary>
        protected ProcessingCommandControlBase()
        {
            InitializeComponent();

            using (ProcessingState processingState = new ProcessingState())
            {
                // get the default values 
                StorePredicateResults = processingState.StorePredicateResults;
                ThrowTriggerActivatedException = processingState.ThrowTriggerActivatedException;
            }
        }

        #endregion



        #region Properties

        IProcessingCommandInfo[] _processingCommands = null;
        /// <summary>
        /// Gets or sets the processing commands.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        [Browsable(false)]
        public IProcessingCommandInfo[] ProcessingCommands
        {
            get
            {
                return _processingCommands;
            }
            set
            {
                if (_processingCommands != value)
                {
                    _processingCommands = value;

                    processingCommandViewer.ProcessingCommands = _processingCommands;

                    if (_processingCommands.Length > 0)
                        SelectedProcessingCommand = _processingCommands[0];
                    else
                        SelectedProcessingCommand = null;
                }

                UpdateUI();
            }
        }

        /// <summary>
        /// Gets or sets the selected processing command.
        /// </summary>
        [Browsable(false)]
        public IProcessingCommandInfo SelectedProcessingCommand
        {
            get
            {
                return processingCommandViewer.SelectedProcessingCommand;
            }
            set
            {
                processingCommandViewer.SelectedProcessingCommand = value;
            }
        }

        bool _storePredicateResults = false;
        /// <summary>
        /// Gets or sets a value indicating whether the processing results 
        /// must store predicate results of triggers, analyzer result comparers and etc.
        /// </summary>
        /// <value>
        /// <b>True</b> - the processing results must store predicate results of triggers,
        /// analyzer result comparers and etc; otherwise, <b>false</b>.
        /// Default value is <b>false</b>.
        /// </value>
        public bool StorePredicateResults
        {
            get
            {
                return _storePredicateResults;
            }
            set
            {
                _storePredicateResults = value;
            }
        }

        bool _throwTriggerActivatedException = false;
        /// <summary>
        /// Gets or sets a value indicating whether exception must be thrown if 
        /// an important trigger is activated.
        /// </summary>
        /// <value>
        /// <b>True</b> - exception must be thrown if important trigger is activated; otherwise, <b>false</b>.
        /// Default value is <b>false</b>.
        /// </value>
        public bool ThrowTriggerActivatedException
        {
            get
            {
                return _throwTriggerActivatedException;
            }
            set
            {
                _throwTriggerActivatedException = value;
            }
        }

        bool _updateCommandViewerWhenPropertyChanged = false;
        /// <summary>
        /// Gets or sets a value indicating whether the command viewer must
        /// be updated when property of selected command is changed.
        /// </summary>
        /// <value>
        /// <b>True</b> - command viewer must be updated when property of selected command is changed; otherwise, <b>false</b>.
        /// Default value is <b>false</b>.
        /// </value>
        public bool UpdateCommandViewerWhenPropertyChanged
        {
            get
            {
                return _updateCommandViewerWhenPropertyChanged;
            }
            set
            {
                _updateCommandViewerWhenPropertyChanged = value;
            }
        }

        #endregion



        #region Methods

        #region PROTECTED

        /// <summary>
        /// The parent form is changed.
        /// </summary>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            Form parentForm = this.ParentForm;
            if (parentForm != null)
                parentForm.FormClosed += new FormClosedEventHandler(parentForm_FormClosed);

            if (_parentForm != null)
                _parentForm.FormClosed -= parentForm_FormClosed;
            _parentForm = parentForm;
        }

        /// <summary>
        /// Determines whether the specified command can be executed.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// <b>true</b> if the specified command can be executed;
        /// <b>false</b> if the specified command can NOT be executed.
        /// </returns>
        protected virtual bool CanExecute(IProcessingCommandInfo command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="processingState">The state of processing.</param>
        /// <returns>The command processing result.</returns>
        protected virtual ProcessingResult Execute(
            IProcessingCommandInfo command,
            ProcessingState processingState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes the results.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>
        /// The changed result.
        /// </returns>
        protected virtual ProcessingResult ProcessResult(ProcessingResult result)
        {
            return result;
        }

        /// <summary>
        /// Refreshes the results on control.
        /// </summary>
        protected void RefreshResults()
        {
            if (_processingCommandResult == null)
                return;

            ProcessingResult result = ProcessResult(_processingCommandResult);
            ShowResults(result);
        }

        #endregion


        #region PRIVATE

        #region UI

        /// <summary>
        /// Handles the PropertyValueChanged event of propertyGrid object.
        /// </summary>
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            // if command viewer must be updated
            if (UpdateCommandViewerWhenPropertyChanged)
                // update the command viewer
                processingCommandViewer.UpdateProcessingCommandsTree();
        }

        /// <summary>
        /// Handles the CheckedChanged event of viewProcessingTreeStructureCheckBox object.
        /// </summary>
        private void viewProcessingTreeStructureCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // if the processing commands must be shown as a tree
            if (viewProcessingTreeStructureCheckBox.Checked)
                processingCommandViewer.ViewProcessingTreeStructure = true;
            else
                processingCommandViewer.ViewProcessingTreeStructure = false;
        }

        /// <summary>
        /// Handles the AfterSelect event of processingCommandViewer object.
        /// </summary>
        private void processingCommandViewer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateUI();

            // get selected processing command
            IProcessingCommandInfo command = SelectedProcessingCommand;
            // update property grid
            propertyGrid.SelectedObject = command;

            // get the command name
            string commandName = string.Empty;
            if (command != null)
                // get the readable command name
                commandName = ProcessingDemosTools.GetReadableTypeName(((object)command).GetType());
            // update command name group box text
            propertyGridGroupBox.Text = commandName;

            if (SelectedProcessingCommandChanged != null)
                SelectedProcessingCommandChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Click event of executeButton object.
        /// </summary>
        private void executeButton_Click(object sender, EventArgs e)
        {
            // clear old result
            ShowResults(null);

            // get form caption
            string caption = ((object)SelectedProcessingCommand).ToString();
            // create progress form
            using (ActionProgressForm progressForm =
                new ActionProgressForm(ExecuteSelectedProcessingCommand, 3, caption))
            {
                progressForm.StartPosition = FormStartPosition.CenterParent;
                progressForm.CloseAfterComplete = true;
                progressForm.RunAndShowDialog(this.ParentForm);
            }

            // get result
            ProcessingResult result = _processingCommandResult;
            if (result != null)
                // process result
                result = ProcessResult(result);
            // show result
            ShowResults(result);

            if (showResultsAfretExecuteCheckBox.Checked)
                mainTabControl.SelectedTab = resultTabPage;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of mainTabControl object.
        /// </summary>
        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if processing tab page is selected
            if (mainTabControl.SelectedTab == processingTabPage)
                // move focus to command viewer
                processingCommandViewer.Focus();
        }

        /// <summary>
        /// Handles the FormClosed event of parentForm object.
        /// </summary>
        private void parentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // get processing result
            ProcessingResult result = processingResultViewer.ProcessingResult;
            // if processing result must be removed
            if (result != _processingCommandResult)
                result.Dispose();

            // if current processing result must be removed
            if (_processingCommandResult != null)
                _processingCommandResult.Dispose();
        }

        #endregion


        /// <summary>
        /// Updates the user interface of this form.
        /// </summary>
        private void UpdateUI()
        {
            IProcessingCommandInfo selectedCommand = SelectedProcessingCommand;
            bool isCommandSelected = selectedCommand != null;

            executeButton.Enabled = isCommandSelected && CanExecute(selectedCommand);

            string executeText = "Execute";
            if (selectedCommand != null)
            {
                Type commandType = ((object)selectedCommand).GetType();

                if (ProcessingDemosTools.IsNameEqual(commandType, "Analyzer") ||
                    ProcessingDemosTools.IsNameEqual(commandType, "Verifier"))
                    executeText = "Analyze";
            }
            executeButton.Text = executeText;
        }

        /// <summary>
        /// Shows the results of processing command.
        /// </summary>
        /// <param name="result">The result.</param>
        private void ShowResults(ProcessingResult result)
        {
            string caption = string.Empty;
            if (result != null)
                caption = ((object)SelectedProcessingCommand).ToString();
            resultGroupBox.Text = caption;

            ProcessingResult prevousResult = processingResultViewer.ProcessingResult;

            processingResultViewer.ProcessingResult = result;

            if (prevousResult != null &&
                prevousResult != _processingCommandResult)
                prevousResult.Dispose();
        }

        /// <summary>
        /// Executes the selected processing command.
        /// </summary>
        /// <param name="progressController">The progress controller.</param>
        private void ExecuteSelectedProcessingCommand(IActionProgressController progressController)
        {
            IProcessingCommandInfo selectedProcessingCommandInfo = SelectedProcessingCommand;
            if (_processingCommandResult != null)
            {
                _processingCommandResult.Dispose();
                _processingCommandResult = null;
            }

            using (ProcessingState processingState = new ProcessingState(progressController))
            {
                processingState.StorePredicateResults = StorePredicateResults;
                processingState.ThrowTriggerActivatedException = ThrowTriggerActivatedException;
                _processingCommandResult = Execute(selectedProcessingCommandInfo, processingState);
            }
        }

        #endregion

        #endregion



        #region Events

        /// <summary>
        /// Occurs when the selected processing command is changed.
        /// </summary>
        public event EventHandler SelectedProcessingCommandChanged;

        #endregion

    }
}
