using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Vintasoft.Imaging.Pdf.Processing.PdfA;
using Vintasoft.Imaging.Processing;
using Vintasoft.Imaging.Processing.Analyzers;


namespace DemosCommonCode.Imaging
{
    /// <summary>
    /// A control that allows to view processing commands.
    /// </summary>
    public partial class ProcessingCommandViewer : TreeView
    {

        #region Constants

        #region INTERNAL

        /// <summary>
        /// Key of the command image.
        /// </summary>
        internal const string IMAGE_KEY_COMMAND = "Command";

        /// <summary>
        /// Image key for the verifier command.
        /// </summary>
        internal const string IMAGE_KEY_VERIFIER_COMMAND = "VerificationProfile";

        /// <summary>
        /// Image key for the converter command.
        /// </summary>
        internal const string IMAGE_KEY_CONVERTER_COMMAND = "ConversionProfile"; 

        #endregion


        #region PRIVATE

        /// <summary>
        /// Key of the default node image.
        /// </summary>
        private const string IMAGE_KEY_DEFAULT = "ProcessingCommandDefault";

        /// <summary>
        /// Key of the property node image.
        /// </summary>
        private const string IMAGE_KEY_PROPERTY = "ProcessingCommandProperty";

        /// <summary>
        /// Key of the composite command image.
        /// </summary>
        private const string IMAGE_KEY_COMPOSITE_COMMAND = "CompositeCommand";

        /// <summary>
        /// Image key for the analyzer command.
        /// </summary>
        private const string IMAGE_KEY_ANALYZER_COMMAND = "Analyzer";

        /// <summary>
        /// Image key for the conditional command.
        /// </summary>
        private const string IMAGE_KEY_CONDITIONAL_COMMAND = "ConditionalCommand";

        /// <summary>
        /// Image key for trigger command error.
        /// </summary>
        private const string IMAGE_KEY_TRIGGER_COMMAND_ERROR = "TriggerImportant";

        /// <summary>
        /// Image key for trigger command warning.
        /// </summary>
        private const string IMAGE_KEY_TRIGGER_COMMAND_WARNING = "TriggerUnimportant";

        /// <summary>
        /// Image key for trigger command information.
        /// </summary>
        private const string IMAGE_KEY_TRIGGER_COMMAND_INFORMATION = "TriggerInformation";

        /// <summary>
        /// Image key for predicate command.
        /// </summary>
        private const string IMAGE_KEY_PREDICATE_COMMAND = "Predicate";

        /// <summary>
        /// Image key for property getter command.
        /// </summary>
        private const string IMAGE_KEY_PROPERTY_GETTER = "PropertyGetter";

        /// <summary>
        /// Image key for the property setter command.
        /// </summary>
        private const string IMAGE_KEY_PROPERTY_SETTER = "PropertySetter";

        /// <summary>
        /// Image key for the composite predicate analyzer.
        /// </summary>
        private const string IMAGE_KEY_COMPOSITE_PREDICATE_ANALYZER = "CompositePredicateAnalyzer";

        /// <summary>
        /// Image key for the fixup command.
        /// </summary>
        private const string IMAGE_KEY_FIXUP_COMMAND = "FixupCommand"; 

        #endregion

        #endregion



        #region Fields

        /// <summary>
        /// Tree node to the processing command table.
        /// </summary>
        Dictionary<TreeNode, IProcessingCommandTreeInfo> _processingCommandTreeInfo =
            new Dictionary<TreeNode, IProcessingCommandTreeInfo>();

        /// <summary>
        /// Indicates that the processing command is adding now.
        /// </summary>
        int _addProcessingCommandTreeInfo = 0;

        /// <summary>
        /// Context menu of tree node.
        /// </summary>
        ContextMenuStrip _treeNodeMenu = new ContextMenuStrip();

        /// <summary>
        /// Dictionary: the image key => the resource name.
        /// </summary>
        static Dictionary<string, string> _imageKeyToResourceName =
            new Dictionary<string, string>();

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes the <see cref="ProcessingCommandViewer"/> class.
        /// </summary>
        static ProcessingCommandViewer()
        {
            string resourceFormatName =
               "DemosCommonCode.Imaging.Processing.ProcessingCommandViewer.Resources.{0}.png";

            _imageKeyToResourceName.Add(IMAGE_KEY_DEFAULT, string.Format(resourceFormatName, "DefaultCommand"));
            _imageKeyToResourceName.Add(IMAGE_KEY_PROPERTY, string.Format(resourceFormatName, "PropertyCommand"));
            _imageKeyToResourceName.Add(IMAGE_KEY_COMMAND, string.Format(resourceFormatName, "Command"));
            _imageKeyToResourceName.Add(IMAGE_KEY_CONDITIONAL_COMMAND, string.Format(resourceFormatName, "ConditionalCommand"));
            _imageKeyToResourceName.Add(IMAGE_KEY_VERIFIER_COMMAND, string.Format(resourceFormatName, "VerificationProfile"));
            _imageKeyToResourceName.Add(IMAGE_KEY_ANALYZER_COMMAND, string.Format(resourceFormatName, "Analyzer"));
            _imageKeyToResourceName.Add(IMAGE_KEY_CONVERTER_COMMAND, string.Format(resourceFormatName, "ConversionProfile"));
            _imageKeyToResourceName.Add(IMAGE_KEY_TRIGGER_COMMAND_ERROR, string.Format(resourceFormatName, "TriggerImportant"));
            _imageKeyToResourceName.Add(IMAGE_KEY_TRIGGER_COMMAND_WARNING, string.Format(resourceFormatName, "TriggerUnimportant"));
            _imageKeyToResourceName.Add(IMAGE_KEY_TRIGGER_COMMAND_INFORMATION, string.Format(resourceFormatName, "TriggerInformation"));
            _imageKeyToResourceName.Add(IMAGE_KEY_COMPOSITE_COMMAND, string.Format(resourceFormatName, "CompositeCommand"));
            _imageKeyToResourceName.Add(IMAGE_KEY_PREDICATE_COMMAND, string.Format(resourceFormatName, "Predicate"));
            _imageKeyToResourceName.Add(IMAGE_KEY_PROPERTY_GETTER, string.Format(resourceFormatName, "PropertyGetter"));
            _imageKeyToResourceName.Add(IMAGE_KEY_PROPERTY_SETTER, string.Format(resourceFormatName, "PropertySetter"));
            _imageKeyToResourceName.Add(IMAGE_KEY_COMPOSITE_PREDICATE_ANALYZER, string.Format(resourceFormatName, "CompositePredicateAnalyzer"));
            _imageKeyToResourceName.Add(IMAGE_KEY_FIXUP_COMMAND, string.Format(resourceFormatName, "FixupCommand"));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingCommandViewer"/> class.
        /// </summary>
        public ProcessingCommandViewer()
        {
            HideSelection = false;
            ImageList = new ImageList();
            ImageList.ImageSize = new Size(16, 16);
            ImageList.ColorDepth = ColorDepth.Depth32Bit;

            LoadImageResources(ImageList);

            ProcessingCommands = null;

            _treeNodeMenu = new ContextMenuStrip();
            _treeNodeMenu.Opening += new CancelEventHandler(treeNodeMenu_Opening);

            ToolStripMenuItem expandAllMenuItem = new ToolStripMenuItem("Expand All");
            expandAllMenuItem.Click += new EventHandler(expandAllMenuItem_Click);
            _treeNodeMenu.Items.Add(expandAllMenuItem);

            ToolStripMenuItem collapseAllMenuItem = new ToolStripMenuItem("Collapse All");
            collapseAllMenuItem.Click += new EventHandler(collapseAllMenuItem_Click);
            _treeNodeMenu.Items.Add(collapseAllMenuItem);
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
                _processingCommands = value;

                Enabled = _processingCommands != null && _processingCommands.Length > 0;

                BeginUpdate();
                BuildProcessingCommandsTree();
                EndUpdate();
            }
        }

        IProcessingCommandInfo _selectedProcessingCommand = null;
        /// <summary>
        /// Gets or sets the selected processing command.
        /// </summary>
        [Browsable(false)]
        public IProcessingCommandInfo SelectedProcessingCommand
        {
            get
            {
                return _selectedProcessingCommand;
            }
            set
            {
                SelectedNode = FindNode(Nodes, value);
                _selectedProcessingCommand = value;
            }
        }

        bool _viewProcessingTreeStructure = false;
        /// <summary>
        /// Gets or sets a value indicating whether the processing commands must be shown as a tree.
        /// </summary>
        /// <value>
        /// <b>True</b> - the processing commands must be shown as a tree;<br />
        /// <b>false</b> - the processing commands must be shown as a list.<br />
        /// Default value is <b>false</b>.
        /// </value>
        [Browsable(true)]
        [DefaultValue(false)]
        [Description("View processing tree structure.")]
        public bool ViewProcessingTreeStructure
        {
            get
            {
                return _viewProcessingTreeStructure;
            }
            set
            {
                if (_viewProcessingTreeStructure != value)
                {
                    bool prevValue = _viewProcessingTreeStructure;
                    _viewProcessingTreeStructure = value;

                    if (_processingCommandTreeInfo.Count > 0)
                    {
                        BeginUpdate();
                        TreeNode[] nodes = new TreeNode[_processingCommandTreeInfo.Count];
                        _processingCommandTreeInfo.Keys.CopyTo(nodes, 0);
                        foreach (TreeNode node in nodes)
                        {
                            IProcessingCommandTreeInfo command = _processingCommandTreeInfo[node];

                            if (prevValue)
                                RemoveNodes(node.Nodes, (IEnumerable)command);
                            else
                                RemoveNodes(node.Nodes, ((IProcessingCommandTreeInfo)command).ProcessingTreeNodes);

                            if (_viewProcessingTreeStructure)
                                AddNodes(node.Nodes, (IEnumerable)command);
                            else
                                AddNodes(node.Nodes, ((IProcessingCommandTreeInfo)command).ProcessingTreeNodes);
                        }
                        EndUpdate();
                    }
                }
            }
        }

        #endregion



        #region Methods

        #region PROTECTED

        /// <summary>
        /// Mouse is clicked on a tree node.
        /// </summary>
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Button == MouseButtons.Right)
                SelectedNode = e.Node;
            base.OnNodeMouseClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:AfterSelect" /> event.
        /// </summary>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            _selectedProcessingCommand = GetProcessingCommandFromNode(e.Node);

            base.OnAfterSelect(e);
        }

        #endregion


        #region INTERNAL

        /// <summary>
        /// Updates the processing commands tree.
        /// </summary>
        internal void UpdateProcessingCommandsTree()
        {
            BeginUpdate();

            // save information about expanded nodes in the processing commands tree to the dictionary
            Dictionary<string, bool> expandedNodes = new Dictionary<string, bool>();
            foreach (TreeNode node in Nodes)
                AddExpandedNodes(node, expandedNodes);

            // update the processing commands tree
            BuildProcessingCommandsTree();

            // restore expanded nodes in the processing commands tree
            foreach (TreeNode node in Nodes)
                UpdateNodeExpandState(node, expandedNodes);

            EndUpdate();
        }

        /// <summary>
        /// Returns the processing command associated with specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// The processing command if command is found; otherwise, <b>null</b>.
        /// </returns>
        internal IProcessingCommandInfo GetProcessingCommandFromNode(TreeNode node)
        {
            if (node == null)
                return null;

            return node.Tag as IProcessingCommandInfo;
        }

        /// <summary>
        /// Returns the node associated with the specified processing command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The node.</returns>
        internal TreeNode GetNodeFromProcessingCommand(IProcessingCommandInfo command)
        {
            return FindNode(Nodes, command);
        }

        /// <summary>
        /// Loads the image resources.
        /// </summary>
        /// <param name="imageList">The image list.</param>
        internal static void LoadImageResources(ImageList imageList)
        {
            foreach (string key in _imageKeyToResourceName.Keys)
                AddImageResourceToImageList(imageList, key);
        }

        /// <summary>
        /// Returns the image resource for the specified processing command.
        /// </summary>
        /// <param name="command">The command.</param>
        internal static Image GetImageResource(IProcessingCommandInfo command)
        {
            string key = GetCommandImageKey(command);
            return GetImageResource(key);
        }

        /// <summary>
        /// Returns the key of image.
        /// </summary>
        /// <param name="command">The command.</param>
        internal static string GetCommandImageKey(IProcessingCommandInfo command)
        {
            string imageKey = IMAGE_KEY_COMMAND;

            if (command != null)
            {
                Type commandType = ((object)command).GetType();

                if (ProcessingDemosTools.IsNameEqual(commandType, "ConversionProfile"))
                    imageKey = IMAGE_KEY_CONVERTER_COMMAND;
                else if (ProcessingDemosTools.IsNameEqual(commandType, "VerificationProfile"))
                    imageKey = IMAGE_KEY_VERIFIER_COMMAND;
                else if (ProcessingDemosTools.IsNameEqual(commandType, "PropertyGetter"))
                    imageKey = IMAGE_KEY_PROPERTY_GETTER;
                else if (ProcessingDemosTools.IsNameEqual(commandType, "PropertySetter"))
                    imageKey = IMAGE_KEY_PROPERTY_SETTER;
                else if (ProcessingDemosTools.IsNameEqual(commandType, "CompositePredicateAnalyzer"))
                    imageKey = IMAGE_KEY_COMPOSITE_PREDICATE_ANALYZER;
                else if (ProcessingDemosTools.IsNameEqual(commandType, "PredicateAnalyzer"))
                    imageKey = IMAGE_KEY_PREDICATE_COMMAND;
                else if (ProcessingDemosTools.IsNameEqual(commandType, "Analyzer"))
                    imageKey = IMAGE_KEY_ANALYZER_COMMAND;
                else if (!command.Name.StartsWith("Fixups") && command.Name.StartsWith("Fixup"))
                    imageKey = IMAGE_KEY_FIXUP_COMMAND;
                else if (ProcessingDemosTools.IsNameEqual(commandType, "ConditionalCommand"))
                    imageKey = IMAGE_KEY_CONDITIONAL_COMMAND;
                else if (command is ITriggerInfo)
                {
                    ITriggerInfo trigger = (ITriggerInfo)command;
                    switch (trigger.Severity)
                    {
                        case TriggerSeverity.Important:
                            imageKey = IMAGE_KEY_TRIGGER_COMMAND_ERROR;
                            break;

                        case TriggerSeverity.Unimportant:
                            imageKey = IMAGE_KEY_TRIGGER_COMMAND_WARNING;
                            break;

                        case TriggerSeverity.Information:
                            imageKey = IMAGE_KEY_TRIGGER_COMMAND_INFORMATION;
                            break;
                    }
                }
                else if (command is IEnumerable)
                    imageKey = IMAGE_KEY_COMPOSITE_COMMAND;
            }

            return imageKey;
        }

        #endregion


        #region PRIVATE

        #region UI

        /// <summary>
        /// Handles the Opening event of TreeNodeMenu object.
        /// </summary>
        private void treeNodeMenu_Opening(object sender, CancelEventArgs e)
        {
            // if context menu must be closed
            if (SelectedNode.Nodes.Count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                _treeNodeMenu.Items[0].Enabled = CanExpand(SelectedNode);
                _treeNodeMenu.Items[1].Enabled = CanCollapse(SelectedNode);
            }
        }

        /// <summary>
        /// Handles the Click event of ExpandAllMenuItem object.
        /// </summary>
        private void expandAllMenuItem_Click(object sender, EventArgs e)
        {
            BeginUpdate();
            // expand all nodes
            SelectedNode.ExpandAll();
            EndUpdate();
        }

        /// <summary>
        /// Handles the Click event of CollapseAllMenuItem object.
        /// </summary>
        private void collapseAllMenuItem_Click(object sender, EventArgs e)
        {
            BeginUpdate();
            // collapse all nodes
            SelectedNode.Collapse(false);
            EndUpdate();
        }

        #endregion


        /// <summary>
        /// Adds the expanded nodes to the specified dictionary.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="expandedNodes">The dictionary with expanded nodes.</param>
        private void AddExpandedNodes(TreeNode node, Dictionary<string, bool> expandedNodes)
        {
            if (node.IsExpanded)
                expandedNodes.Add(node.FullPath, true);

            foreach (TreeNode subNode in node.Nodes)
                AddExpandedNodes(subNode, expandedNodes);
        }

        /// <summary>
        /// Updates the node expand state.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="expandedNodes">The dictionary with expanded nodes.</param>
        private void UpdateNodeExpandState(TreeNode node, Dictionary<string, bool> expandedNodes)
        {
            if (expandedNodes.Count == 0)
                return;

            bool isExpanded;
            if (expandedNodes.TryGetValue(node.FullPath, out isExpanded) && isExpanded)
                node.Expand();

            foreach (TreeNode subNode in node.Nodes)
                UpdateNodeExpandState(subNode, expandedNodes);
        }

        /// <summary>
        /// Builds the processing commands tree.
        /// </summary>
        private void BuildProcessingCommandsTree()
        {
            Nodes.Clear();
            _processingCommandTreeInfo.Clear();
            if (_processingCommands != null)
                AddNodes(Nodes, _processingCommands);
        }

        /// <summary>
        /// Adds the processing commands to the processing commands tree.
        /// </summary>
        /// <param name="rootNodeCollection">The root node collection.</param>
        /// <param name="collection">Information about the processing command,
        /// which should be added to the processing commands tree.</param>
        private void AddNodes(TreeNodeCollection rootNodeCollection, IEnumerable collection)
        {
            foreach (object item in collection)
            {
                if (item is IProcessingCommandInfo)
                    AddNode(rootNodeCollection, (IProcessingCommandInfo)item);
            }
        }

        /// <summary>
        /// Removes the processing commands from the processing commands tree.
        /// </summary>
        /// <param name="rootNodeCollection">The root node collection.</param>
        /// <param name="collection">Information about the processing command,
        /// which should be removed from the processing commands tree.</param>
        private void RemoveNodes(TreeNodeCollection rootNodeCollection, IEnumerable collection)
        {
            foreach (object item in collection)
            {
                if (item is IProcessingCommandInfo)
                    RemoveNode(rootNodeCollection, (IProcessingCommandInfo)item);
            }
        }

        /// <summary>
        /// Adds the object set to the node collection.
        /// </summary>
        /// <param name="rootNodeCollection">The root node collection.</param>
        /// <param name="objectSet">The set of objects.</param>
        private void AddObjectSet(
            TreeNodeCollection rootNodeCollection,
            IEnumerable objectSet)
        {
            foreach (object obj in objectSet)
            {
                TreeNode node = rootNodeCollection.Add(obj.ToString());
                node.ContextMenuStrip = _treeNodeMenu;
            }
        }

        Dictionary<IProcessingCommandInfo, TreeNode> _addedPdfaProcessing = new Dictionary<IProcessingCommandInfo, TreeNode>();

        /// <summary>
        /// Adds the processing command to the processing commands tree.
        /// </summary>
        /// <param name="rootNodeCollection">The root node collection.</param>
        /// <param name="command">The command.</param>
        private TreeNode AddNode(
            TreeNodeCollection rootNodeCollection,
            IProcessingCommandInfo command)
        {
            string commandName = ((object)command).ToString();

            TreeNode node = rootNodeCollection.Add(commandName);

            node.ContextMenuStrip = _treeNodeMenu;
            node.Tag = command;
            string imageKey = GetCommandImageKey(command);
            SetImageKey(node, imageKey);

            string commandTypeNodeName = string.Format("Type: {0}",
                ProcessingDemosTools.GetReadableTypeName(((object)command).GetType()));
            AddPropertyTreeNode(node.Nodes, commandTypeNodeName);

            string targetTypeNodeName = string.Format("Target: {0}",
                ProcessingDemosTools.GetReadableTypeName(command.TargetType));
            AddPropertyTreeNode(node.Nodes, targetTypeNodeName);

            if (command is IValueProcessing)
            {
                IValueProcessing targetConverter = (IValueProcessing)command;

                string destTargetTypeNodeName = string.Format("ProcessingTarget: {0}",
                    ProcessingDemosTools.GetReadableTypeName(targetConverter.ProcessingTargetType));
                AddPropertyTreeNode(node.Nodes, destTargetTypeNodeName);

                TreeNode converterNode = AddPropertyTreeNode(node.Nodes, "Analyzer");
                AddNode(converterNode.Nodes, targetConverter.Analyzer);
            }

            if (command is PdfAVerifier || command is PdfAConverter)
            {
                if (_addedPdfaProcessing.ContainsKey(command))
                    return node;
                _addedPdfaProcessing.Add(command, node);
            }

            if (command is ITriggerInfo)
            {
                ITriggerInfo trigger = (ITriggerInfo)command;

                if (trigger.Predicate != null)
                {
                    TreeNode predicateNode = AddPropertyTreeNode(node.Nodes, "Predicate");
                    AddNode(predicateNode.Nodes, trigger.Predicate);
                }

                string activateValueNodeName = string.Format("ActivateValue: {0}", trigger.ActivationValue);
                AddPropertyTreeNode(node.Nodes, activateValueNodeName);

                string severityNodeName = string.Format("Severity: {0}", trigger.Severity);
                AddPropertyTreeNode(node.Nodes, severityNodeName);
            }
            else if (command is ICompositePredicateAnalyzerInfo)
            {
                ICompositePredicateAnalyzerInfo targetPredicateExpression = (ICompositePredicateAnalyzerInfo)command;

                string expressionOperatorNodeName = string.Format("ConditionalOperator: {0}",
                    targetPredicateExpression.ConditionalOperator);
                AddPropertyTreeNode(node.Nodes, expressionOperatorNodeName);

                if (targetPredicateExpression.Arguments != null)
                {
                    TreeNode argumentsNode = AddPropertyTreeNode(node.Nodes, "Arguments");
                    AddNodes(argumentsNode.Nodes, targetPredicateExpression.Arguments);
                }
            }
            else if (command is IAnalyzerResultsComparerInfo)
            {
                IAnalyzerResultsComparerInfo targetComparisonPredicate = (IAnalyzerResultsComparerInfo)command;

                TreeNode leftArgumentNode = AddPropertyTreeNode(node.Nodes, "LeftAnalyzer");
                AddNode(leftArgumentNode.Nodes, targetComparisonPredicate.LeftAnalyzer);

                string operatorNodeName = string.Format("ComparisonOperator: {0}",
                    targetComparisonPredicate.ComparisonOperator);
                AddPropertyTreeNode(node.Nodes, operatorNodeName);

                if (targetComparisonPredicate.RightAnalyzer != null)
                {
                    TreeNode rightArgumentNode = AddPropertyTreeNode(node.Nodes, "RightAnalyzer");
                    AddNode(rightArgumentNode.Nodes, targetComparisonPredicate.RightAnalyzer);
                }
                else
                {
                    string valueNodeName = string.Format("RightConstantValue: {0}",
                        targetComparisonPredicate.RightConstantValue);
                    AddPropertyTreeNode(node.Nodes, valueNodeName);
                }
            }
            else if (command is IConditionalCommandInfo)
            {
                IConditionalCommandInfo conditionalCommand = (IConditionalCommandInfo)command;

                TreeNode conditionNode = AddPropertyTreeNode(node.Nodes, "Condition");
                AddNode(conditionNode.Nodes, conditionalCommand.Condition);

                if (conditionalCommand.IfBranch != null)
                {
                    TreeNode ifNode = AddPropertyTreeNode(node.Nodes, "IfBranch");
                    AddNode(ifNode.Nodes, conditionalCommand.IfBranch);
                }
                if (conditionalCommand.ElseBranch != null)
                {
                    TreeNode elseNode = AddPropertyTreeNode(node.Nodes, "ElseBranch");
                    AddNode(elseNode.Nodes, conditionalCommand.ElseBranch);
                }
            }
            else if (command is ISetContainsAnalyzerResultPredicateInfo)
            {
                ISetContainsAnalyzerResultPredicateInfo setContainsAnalyzerResultPredicate =
                    (ISetContainsAnalyzerResultPredicateInfo)command;

                TreeNode analyzerNode = AddPropertyTreeNode(node.Nodes, "Analyzer");
                AddNode(analyzerNode.Nodes, setContainsAnalyzerResultPredicate.Analyzer);

                TreeNode referenceSetNode = AddPropertyTreeNode(node.Nodes, "ReferenceSet");
                AddObjectSet(referenceSetNode.Nodes, setContainsAnalyzerResultPredicate.ReferenceSet);
            }
            else if (command is IProcessingCommandTreeInfo)
            {
                if (_addProcessingCommandTreeInfo == 0)
                    _processingCommandTreeInfo[node] = (IProcessingCommandTreeInfo)command;
                _addProcessingCommandTreeInfo++;
                try
                {
                    if (_viewProcessingTreeStructure)
                        AddNodes(node.Nodes, (IEnumerable)command);
                    else
                        AddNodes(node.Nodes, ((IProcessingCommandTreeInfo)command).ProcessingTreeNodes);
                }
                finally
                {
                    _addProcessingCommandTreeInfo--;
                }
            }
            else if (command is IAnalyzerWrapperInfo)
            {
                TreeNode analyzerNode = AddPropertyTreeNode(node.Nodes, "Analyzer");
                AddNode(analyzerNode.Nodes, ((IAnalyzerWrapperInfo)command).Analyzer);
            }
            else if (command is IEnumerable)
            {
                AddNodes(node.Nodes, (IEnumerable)command);
            }

            return node;
        }

        /// <summary>
        /// Removes the processing command from the processing commands tree.
        /// </summary>
        /// <param name="rootNodeCollection">The root node collection.</param>
        /// <param name="command">The processing command,
        /// which should be removed from the processing commands tree.</param>
        private void RemoveNode(
            TreeNodeCollection rootNodeCollection,
            IProcessingCommandInfo command)
        {
            TreeNode node = FindNode(rootNodeCollection, command);
            rootNodeCollection.Remove(node);
        }

        /// <summary>
        /// Adds the tree node and sets an icon of property.
        /// </summary>
        /// <param name="rootNodeCollection">The root node collection.</param>
        /// <param name="text">The text.</param>
        private TreeNode AddPropertyTreeNode(TreeNodeCollection rootNodeCollection, string text)
        {
            // if string does not contain any inappropriate symbol
            if (GetIsValidPropertyName(text))
            {
                TreeNode node = rootNodeCollection.Add(text);
                node.ContextMenuStrip = _treeNodeMenu;
                SetImageKey(node, IMAGE_KEY_PROPERTY);
                return node;
            }
            return null;
        }

        /// <summary>
        /// Returns a value that indicates that property name is correct.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <returns>
        /// <b>true</b> - if property name is correct.
        /// <b>false</b> - if property name is not correct.
        /// </returns>
        private bool GetIsValidPropertyName(string name)
        {
            // for each symbol in property name
            foreach (Char c in name)
            {
                // if symbol is not number or latin letter
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == ':' || c == ' '))
                    // property name is not correct
                    return false;
            }
            // property name is correct
            return true;
        }

        /// <summary>
        /// Finds a node.
        /// </summary>
        /// <param name="nodes">The nodes, where node must be searched.</param>
        /// <param name="processingCommand">The processing command,
        /// which is associated with the searching node.</param>
        /// <returns>
        /// A tree node if node is found; otherwise, <b>null</b>.
        /// </returns>
        private TreeNode FindNode(TreeNodeCollection nodes, IProcessingCommandInfo processingCommand)
        {
            if (processingCommand == null || nodes.Count == 0)
                return null;

            foreach (TreeNode node in nodes)
            {
                if (GetProcessingCommandFromNode(node) == processingCommand)
                    return node;

                TreeNode result = FindNode(node.Nodes, processingCommand);
                if (result != null)
                    return result;
            }

            return null;
        }

        /// <summary>
        /// Sets the image key of node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="imageKey">The image key.</param>
        private void SetImageKey(TreeNode node, string imageKey)
        {
            node.ImageKey = imageKey;
            node.SelectedImageKey = imageKey;
        }

        /// <summary>
        /// Adds the image resource to the image list of the tree view.
        /// </summary>
        /// <param name="imageList">The image list of tree view.</param>
        /// <param name="imageKey">The image key.</param>
        private static void AddImageResourceToImageList(ImageList imageList, string imageKey)
        {
            Image image = GetImageResource(imageKey);
            if (image == null)
                throw new KeyNotFoundException(imageKey);

            imageList.Images.Add(imageKey, image);
        }

        /// <summary>
        /// Returns the image resource, which is associated with the specified key.
        /// </summary>
        /// <param name="imageKey">The image key.</param>
        /// <returns>
        /// The image resource if resource is found; otherwise, <b>null</b>.</returns>
        private static Image GetImageResource(string imageKey)
        {
            string resourceFullName = null;
            if (_imageKeyToResourceName.TryGetValue(imageKey, out resourceFullName))
                return DemosResourcesManager.GetResourceAsBitmap(resourceFullName);

            return null;
        }

        /// <summary>
        /// Determines whether the tree node can be expanded.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// <b>True</b> if tree node can be expanded; otherwise, <b>false</b>.
        /// </returns>
        private bool CanExpand(TreeNode node)
        {
            if (node.Nodes.Count > 0 && !node.IsExpanded)
                return true;

            foreach (TreeNode children in node.Nodes)
            {
                if (CanExpand(children))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the tree node can be collapsed.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// <b>True</b> if tree node can be collapsed; otherwise, <b>false</b>.
        /// </returns>
        private bool CanCollapse(TreeNode node)
        {
            if (node.Nodes.Count > 0 && node.IsExpanded)
                return true;

            foreach (TreeNode children in node.Nodes)
            {
                if (CanCollapse(children))
                    return true;
            }

            return false;
        }

        #endregion

        #endregion



        #region Delegates

        private delegate IProcessingCommandInfo GetSelectedProcessingCommandDelegate();

        #endregion

    }
}
