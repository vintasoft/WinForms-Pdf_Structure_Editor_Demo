using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Vintasoft.Imaging.Processing;
using Vintasoft.Imaging.Processing.Analyzers;


namespace DemosCommonCode.Imaging
{
    /// <summary>
    /// A control that allows to view the execution result of the processing commands.
    /// </summary>
    public partial class ProcessingResultViewer : TreeView
    {

        #region Nested classes

        /// <summary>
        /// Contains information about items, which are added to the tree view.
        /// </summary>
        class AddedItemsInfo
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="AddedItemsInfo"/> class.
            /// </summary>
            /// <param name="count">The count of added items.</param>
            /// <param name="collection">The added items.</param>
            public AddedItemsInfo(int count, IEnumerable collection)
            {
                _count = count;
                _collection = collection;
            }



            int _count;
            /// <summary>
            /// Gets the count of added items.
            /// </summary>
            public int Count
            {
                get
                {
                    return _count;
                }
            }

            IEnumerable _collection;
            /// <summary>
            /// Gets the collection of added items.
            /// </summary>
            public IEnumerable Collection
            {
                get
                {
                    return _collection;
                }
            }
        }

        #endregion



        #region Constants

        /// <summary>
        /// The maximum count of elements, which can be added to the tree view
        /// without the "Show next elements" button.
        /// </summary>
        private const int MAX_SHOW_PROCESSING_RESULTS = 100;

        /// <summary>
        /// Image key for the default node.
        /// </summary>
        private const string IMAGE_KEY_DEFAULT = "ProcessingResultDefault";

        /// <summary>
        /// Image key for the property node.
        /// </summary>
        private const string IMAGE_KEY_PROPERTY = "ProcessingResultProperty";

        /// <summary>
        /// Image key for the verification failed.
        /// </summary>
        private const string IMAGE_KEY_VERIFICATION_FAILED = "ProfileResultFail";

        /// <summary>
        /// Image key for the verification passed.
        /// </summary>
        private const string IMAGE_KEY_VERIFICATION_PASSED = "ProfileResultSuccess";

        /// <summary>
        /// Image key for the failed conversion.
        /// </summary>
        private const string IMAGE_KEY_CONVERSION_FAILED = IMAGE_KEY_VERIFICATION_FAILED;

        /// <summary>
        /// Image key for the passed conversion.
        /// </summary>
        private const string IMAGE_KEY_CONVERSION_PASSED = IMAGE_KEY_VERIFICATION_PASSED;

        /// <summary>
        /// Image key for the composite result.
        /// </summary>
        private const string IMAGE_KEY_COMPOSITE_RESULT = "CompositeResult";

        /// <summary>
        /// Image key for the trigger error.
        /// </summary>
        private const string IMAGE_KEY_TRIGGER_ERROR = "TriggerActivationImportant";

        /// <summary>
        /// Image key for the trigger warning.
        /// </summary>
        private const string IMAGE_KEY_TRIGGER_WARNING = "TriggerActivationUnimportant";

        /// <summary>
        /// Image key for the trigger information.
        /// </summary>
        private const string IMAGE_KEY_TRIGGER_INFORMATION = "TriggerActivationInformation";

        /// <summary>
        /// Image key for the applied commands.
        /// </summary>
        private const string IMAGE_KEY_APPLIED_COMMANDS = "AppliedCommand";

        /// <summary>
        /// Name of the private node.
        /// </summary>
        private const string PRIVATE_TAG_NAME = "_PRIVATE";

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingResultViewer"/> class.
        /// </summary>
        public ProcessingResultViewer()
        {
            ImageList = new ImageList();
            ImageList.ImageSize = new Size(16, 16);
            ImageList.ColorDepth = ColorDepth.Depth32Bit;

            AddImageResourceToImageList(IMAGE_KEY_DEFAULT, "DefaultResult");
            AddImageResourceToImageList(IMAGE_KEY_PROPERTY, "PropertyResult");

            AddImageResourceToImageList(IMAGE_KEY_VERIFICATION_PASSED, "ProfileResultSuccess");
            AddImageResourceToImageList(IMAGE_KEY_VERIFICATION_FAILED, "ProfileResultFail");

            AddImageResourceToImageList(IMAGE_KEY_COMPOSITE_RESULT, "CompositeResult");

            AddImageResourceToImageList(IMAGE_KEY_TRIGGER_ERROR, "TriggerActivationImportant");
            AddImageResourceToImageList(IMAGE_KEY_TRIGGER_WARNING, "TriggerActivationUnimportant");
            AddImageResourceToImageList(IMAGE_KEY_TRIGGER_INFORMATION, "TriggerActivationInformation");

            AddImageResourceToImageList(IMAGE_KEY_APPLIED_COMMANDS, "AppliedCommand");

            ProcessingCommandViewer.LoadImageResources(ImageList);
        }

        #endregion



        #region Properties

        ProcessingResult _processingResult = null;
        /// <summary>
        /// Gets or sets the processing result.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProcessingResult ProcessingResult
        {
            get
            {
                return _processingResult;
            }
            set
            {
                _processingResult = value;

                Enabled = _processingResult != null;

                BeginUpdate();
                Nodes.Clear();
                AddTreeNodeItem(Nodes, _processingResult);
                if (Nodes.Count == 1)
                    Nodes[0].Expand();
                EndUpdate();
            }
        }

        #endregion



        #region Methods

        #region PROTECTED

        /// <summary>
        /// Builds new level of tree before node expands.
        /// </summary>
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            try
            {
                // get expanding tree node
                TreeNode node = e.Node;

                // if node is not empty
                // and node tag is not empty 
                // and node has subnode(s)
                // and last subnode is private node
                if (node != null &&
                    node.Tag != null &&
                    node.Nodes.Count > 0 &&
                    node.Nodes[node.Nodes.Count - 1].Text == PRIVATE_TAG_NAME)
                {
                    ProcessingResult processingResult = node.Tag as ProcessingResult;
                    // if node is processing result
                    if (processingResult != null)
                    {
                        IEnumerable<ProcessingResult> innerResults = processingResult as IEnumerable<ProcessingResult>;
                        // if processing result is collection of results
                        if (innerResults != null)
                        {
                            this.BeginUpdate();
                            // remove private node
                            node.Nodes.RemoveAt(node.Nodes.Count - 1);
                            // add tree node items from collection
                            AddTreeNodeItems(node, innerResults);
                            this.EndUpdate();
                        }
                        return;
                    }


                    Dictionary<IProcessingCommandInfo, List<ProcessingTargetChangedResult>> appliedCommands =
                        node.Tag as Dictionary<IProcessingCommandInfo, List<ProcessingTargetChangedResult>>;
                    // if node is applied commands
                    if (appliedCommands != null)
                    {
                        this.BeginUpdate();
                        // remove private node
                        node.Nodes.RemoveAt(node.Nodes.Count - 1);
                        // for each processing command
                        foreach (IProcessingCommandInfo processingCommand in appliedCommands.Keys)
                        {
                            // add processing command node
                            TreeNode processingCommadNode = AddProcessingCommandNode(node, processingCommand, false);
                            if (processingCommadNode != null)
                            {
                                processingCommadNode.Tag = appliedCommands[processingCommand];
                                // add private node
                                processingCommadNode.Nodes.Add(PRIVATE_TAG_NAME);
                            }
                        }
                        this.EndUpdate();

                        return;
                    }


                    List<ProcessingTargetChangedResult> triggerActivationResult =
                        node.Tag as List<ProcessingTargetChangedResult>;
                    // if node is trigger activation result
                    if (triggerActivationResult != null)
                    {
                        // add all trigger activation results
                        AddTreeNodeItemsFromList(node, triggerActivationResult, "");
                        return;
                    }


                    Dictionary<IProcessingCommandInfo, List<ProcessingErrorResult>> processingErrors =
                        node.Tag as Dictionary<IProcessingCommandInfo, List<ProcessingErrorResult>>;
                    // if node is processing errors
                    if (processingErrors != null)
                    {
                        this.BeginUpdate();
                        // remove private node
                        node.Nodes.RemoveAt(node.Nodes.Count - 1);
                        // for each processing command
                        foreach (IProcessingCommandInfo processingCommand in processingErrors.Keys)
                        {
                            // add private node
                            node.Nodes.Add(PRIVATE_TAG_NAME);

                            // add all error results
                            AddTreeNodeItemsFromList(node, processingErrors[processingCommand], IMAGE_KEY_TRIGGER_ERROR);
                        }
                        this.EndUpdate();

                        return;
                    }


                    List<ProcessingErrorResult> processingErrorResults =
                        node.Tag as List<ProcessingErrorResult>;
                    // if node is processing error result
                    if (processingErrorResults != null)
                    {
                        // add all error results
                        AddTreeNodeItemsFromList(node, processingErrorResults, IMAGE_KEY_TRIGGER_ERROR);
                        return;
                    }

                }
            }
            finally
            {
                base.OnBeforeExpand(e);
            }
        }

        /// <summary>
        /// Adds the processing results.
        /// </summary>
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            AddedItemsInfo nextItemsInfo = node.Tag as AddedItemsInfo;
            if (nextItemsInfo != null)
            {
                BeginUpdate();
                TreeNode parentNode = node.Parent;
                int index = parentNode.Nodes.IndexOf(node);
                node.Remove();

                AddTreeNodeItems(parentNode, nextItemsInfo);

                if (parentNode.Nodes.Count > index)
                    this.SelectedNode = parentNode.Nodes[index];
                EndUpdate();
                e.Cancel = true;
            }

            base.OnBeforeSelect(e);
        }

        #endregion


        #region PRIVATE

        /// <summary> 
        /// Adds tree node items form list.
        /// </summary>
        /// <param name="node">A tree node.</param>
        /// <param name="items">A list of results.</param>
        /// <param name="nodeImageKey">Image key of new nodes.</param>
        private void AddTreeNodeItemsFromList(TreeNode node, IList items, string nodeImageKey)
        {
            // get dictionary: processing command name => list of processing results
            Dictionary<string, IList> sortedItem = new Dictionary<string, IList>();

            // for each processing result
            foreach (ProcessingResult item in items)
            {
                // get node name
                string nodeName = GetNodeName(item);

                IList list = null;
                // if dictionary does not have processing command name
                if (!sortedItem.TryGetValue(nodeName, out list))
                {
                    // create new list
                    list = new List<ProcessingResult>();
                    // add processing command name with new list to dictionary
                    sortedItem.Add(nodeName, list);
                }
                // add processing result
                list.Add(item);
            }

            this.BeginUpdate();
            // remove private node
            node.Nodes.RemoveAt(node.Nodes.Count - 1);
            // if number of processing results is not equal to number of processing commands
            if (items.Count != sortedItem.Count)
            {
                // for each processing command
                foreach (string itemName in sortedItem.Keys)
                {
                    // get processing results
                    IList list = sortedItem[itemName];
                    // get node name
                    string nodeName = string.Format("{0} ({1} matches)", itemName, list.Count);
                    // add processing result node
                    TreeNode newNode = node.Nodes.Add(nodeName);
                    if (nodeImageKey != "")
                        SetImageKey(newNode, nodeImageKey);
                    // add all processing results subnodes
                    AddTreeNodeItems(newNode, list);
                }
            }
            else
            {
                // add processing results
                AddTreeNodeItems(node, items);
            }
            this.EndUpdate();
        }


        /// <summary>
        /// Adds the tree node item.
        /// </summary>
        /// <param name="rootNodeCollection">The root node collection.</param>
        /// <param name="processingResult">The processing result.</param>
        private TreeNode AddTreeNodeItem(
            TreeNodeCollection rootNodeCollection,
            ProcessingResult processingResult)
        {
            if (processingResult == null)
                return null;

            // get processing result image resource key
            string nodeImageKey = GetImageKey(processingResult);
            // get processing result name
            string processingResultName = GetNodeName(processingResult);
            // add processing result node
            TreeNode node = rootNodeCollection.Add(processingResultName);

            // if processing result target is specified
            if (processingResult.Target != null)
            {
                string targetNodeName = "Target: ";
                // add target name
                targetNodeName += ProcessingDemosTools.GetReadableTypeName(processingResult.Target.GetType());
                // create tree view item with target name
                TreeNode targetNode = node.Nodes.Add(targetNodeName);
                // update tree view item image
                SetImageKey(targetNode, IMAGE_KEY_PROPERTY);
                targetNode.Nodes.Add(processingResult.Target.ToString());
            }

            // if processing command is specified
            if (processingResult.ProcessingCommand != null)
                AddProcessingCommandNode(node, processingResult.ProcessingCommand, true);

            // if current result is analyzer
            if (processingResult is AnalyzerResult)
            {
                // get analyzer result
                object value = ((AnalyzerResult)processingResult).GetValue();
                if (value != null)
                {
                    // add tree view item with analyzer result
                    TreeNode valueNode = node.Nodes.Add(string.Format("Value: {0}", value));
                    SetImageKey(valueNode, IMAGE_KEY_PROPERTY);
                }
            }

            // if current result is conversion profile result
            if (processingResult is ConversionProfileResult)
            {
                ConversionProfileResult conversionProfileResult = (ConversionProfileResult)processingResult;
                // if conversion result has applied commands
                if (conversionProfileResult.AppliedCommands.Count > 0)
                {
                    // create applied commands node
                    TreeNode appliedCommandsNode = node.Nodes.Add("Applied Commands");
                    appliedCommandsNode.Nodes.Add(PRIVATE_TAG_NAME);
                    // update applied commands image
                    SetImageKey(appliedCommandsNode, IMAGE_KEY_APPLIED_COMMANDS);
                    appliedCommandsNode.Tag = conversionProfileResult.AppliedCommands;
                }
            }

            // if current result is processing error result
            if (processingResult is ProcessingErrorResult)
            {
                // get processing execution error
                Exception ex = ((ProcessingErrorResult)processingResult).ProcessingException;
                // create tree node with processing execution error
                TreeNode processingCommandNode = node.Nodes.Add(string.Format("ProcessingException: {0}", ex.Message));
                SetImageKey(processingCommandNode, IMAGE_KEY_PROPERTY);
            }

            // if current result is processing profile result
            if (processingResult is ProcessingProfileResult)
            {
                ProcessingProfileResult processingProfileResult = (ProcessingProfileResult)processingResult;

                // if processing result has errors
                if (processingProfileResult.ProcessingErrors != null && processingProfileResult.ProcessingErrors.Count > 0)
                {
                    // add processing errors node
                    TreeNode processingErrorsNode = node.Nodes.Add("Processing Errors");
                    processingErrorsNode.Nodes.Add(PRIVATE_TAG_NAME);
                    SetImageKey(processingErrorsNode, IMAGE_KEY_TRIGGER_ERROR);
                    processingErrorsNode.Tag = processingProfileResult.ProcessingErrors;
                }

                // for each trigger in activated triggers
                foreach (ITriggerInfo triggerInfo in processingProfileResult.ActivatedTriggers.Keys)
                {
                    // get trigger activation result
                    List<TriggerActivationResult> activatedTrigger =
                        processingProfileResult.ActivatedTriggers[triggerInfo];

                    // get trigger name
                    string processingCommandInfoName = triggerInfo.Name;
                    // if the trigger has ben activated several times
                    if (activatedTrigger.Count > 1)
                        // add activates count to trigger name
                        processingCommandInfoName += string.Format(" ({0} matches)", activatedTrigger.Count);
                    // create trigger node
                    TreeNode processingCommandInfoNode = node.Nodes.Add(processingCommandInfoName);
                    // update trigger node image
                    SetImageKey(processingCommandInfoNode, GetTriggerImageKey(triggerInfo.Severity));

                    AddTreeNodeItems(processingCommandInfoNode, activatedTrigger);
                }

                // if processing result contains the detailed result
                if (processingProfileResult.DetailedResult != null)
                {
                    // create detailed result node
                    TreeNode detailedResultNode = node.Nodes.Add("DetailedResult");
                    // update detailed result node image
                    SetImageKey(detailedResultNode, IMAGE_KEY_COMPOSITE_RESULT);

                    AddTreeNodeItem(detailedResultNode.Nodes, processingProfileResult.DetailedResult);
                }
            }
            // if current result is trigger activation result
            else if (processingResult is TriggerActivationResult)
            {
                TriggerActivationResult triggerResult = (TriggerActivationResult)processingResult;
                // create node with severity information
                TreeNode severityNode = node.Nodes.Add(string.Format("Severity: {0}", triggerResult.Severity));
                // update severity node image
                SetImageKey(severityNode, IMAGE_KEY_PROPERTY);

                // if trigger contains predicate result
                if (triggerResult.PredicateResult != null)
                {
                    // create predicate node result
                    TreeNode predicateResultNode = node.Nodes.Add("PredicateResult");
                    // update predicate node result image
                    SetImageKey(predicateResultNode, IMAGE_KEY_PROPERTY);

                    AddTreeNodeItem(predicateResultNode.Nodes, triggerResult.PredicateResult);
                }
            }
            // if processing result is array
            else if (processingResult is IEnumerable<ProcessingResult>)
            {
                node.Nodes.Add(PRIVATE_TAG_NAME);
            }

            // update current node image
            SetImageKey(node, nodeImageKey);
            node.Tag = processingResult;

            return node;
        }

        /// <summary>
        /// Adds the processing command node.
        /// </summary>
        /// <param name="rootNode">The root node, where new processing command node must be added.</param>
        /// <param name="processingCommand">The processing command.</param>
        /// <param name="showProcessingCommandType">Indicates that processing command type must be shown.</param>
        private TreeNode AddProcessingCommandNode(
            TreeNode rootNode,
            IProcessingCommandInfo processingCommand,
            bool showProcessingCommandType)
        {
            string nodeHeader = string.Empty;
            // if processing command type must be shown
            if (showProcessingCommandType)
            {
                // get processing command type
                string processingCommandType = ProcessingDemosTools.GetReadableTypeName(((object)processingCommand).GetType());

                // if processing command type is correct
                if (GetIsValidTypename(processingCommandType))
                {
                    nodeHeader = string.Format("ProcessingCommand ({0}): ", processingCommandType);
                }
                else
                {
                    nodeHeader = "ProcessingCommand: ";
                }
            }
            // create header name
            nodeHeader = string.Format("{0}{1}", nodeHeader, processingCommand);

            // create tree view node
            TreeNode processingCommadNode = rootNode.Nodes.Add(nodeHeader);
            // get processing command key
            string processingCommandImageKey = ProcessingCommandViewer.GetCommandImageKey(processingCommand);
            if (processingCommandImageKey == ProcessingCommandViewer.IMAGE_KEY_CONVERTER_COMMAND ||
                processingCommandImageKey == ProcessingCommandViewer.IMAGE_KEY_VERIFIER_COMMAND)
                processingCommandImageKey = ProcessingCommandViewer.IMAGE_KEY_COMMAND;
            // update tree view image
            SetImageKey(processingCommadNode, processingCommandImageKey);
            return processingCommadNode;
        }

        /// <summary>
        /// Returns the name of the node.
        /// </summary>
        /// <param name="processingResult">The processing result.</param>
        /// <returns>The node name.</returns>
        private string GetNodeName(ProcessingResult processingResult)
        {
            string name = processingResult.Description;

            if (processingResult is NamedProcessingResult)
            {
                NamedProcessingResult namedProcessingResult =
                    (NamedProcessingResult)processingResult;

                if (namedProcessingResult.ParentProcessingResult != null)
                    name = GetNodeName(namedProcessingResult.ParentProcessingResult);
            }

            return name;
        }

        /// <summary>
        /// Returns the image key of processing result.
        /// </summary>
        /// <param name="processingResult">The processing result.</param>
        /// <returns>The image key.</returns>
        private string GetImageKey(ProcessingResult processingResult)
        {
            string nodeImageKey = IMAGE_KEY_DEFAULT;

            // if named result
            if (processingResult is NamedProcessingResult)
            {
                NamedProcessingResult namedProcessingResult =
                    (NamedProcessingResult)processingResult;

                if (namedProcessingResult.ParentProcessingResult == null)
                {
                    if (namedProcessingResult.Results.Count > 0)
                    {
                        ProcessingResult[] results =
                            new ProcessingResult[namedProcessingResult.Results.Count];
                        namedProcessingResult.Results.CopyTo(results, 0);

                        string subNodeImageKey = GetImageKey(results[0]);
                        string currentNodeImageKey;

                        for (int i = 1; i < results.Length; i++)
                        {
                            currentNodeImageKey = GetImageKey(results[i]);
                            if (subNodeImageKey != currentNodeImageKey)
                            {
                                subNodeImageKey = null;
                                break;
                            }
                        }

                        if (subNodeImageKey != null)
                            // get image key of first result
                            nodeImageKey = subNodeImageKey;
                    }
                }
                else
                {
                    nodeImageKey = GetImageKey(namedProcessingResult.ParentProcessingResult);
                }

                if (nodeImageKey == IMAGE_KEY_DEFAULT)
                    nodeImageKey = IMAGE_KEY_COMPOSITE_RESULT;
            }
            // if verification result
            else if (processingResult is VerificationProfileResult)
            {
                VerificationProfileResult verificationProfileResult =
                    (VerificationProfileResult)processingResult;

                if (verificationProfileResult.IsPassed)
                    nodeImageKey = IMAGE_KEY_VERIFICATION_PASSED;
                else
                    nodeImageKey = IMAGE_KEY_VERIFICATION_FAILED;
            }
            // if conversion result
            else if (processingResult is ConversionProfileResult)
            {
                ConversionProfileResult conversionProfileResult =
                    (ConversionProfileResult)processingResult;

                if (conversionProfileResult.IsSuccessful)
                    nodeImageKey = IMAGE_KEY_CONVERSION_PASSED;
                else
                    nodeImageKey = IMAGE_KEY_CONVERSION_FAILED;
            }
            // if trigger activate result
            else if (processingResult is TriggerActivationResult)
            {
                TriggerActivationResult triggerResult = (TriggerActivationResult)processingResult;
                nodeImageKey = GetTriggerImageKey(triggerResult.Severity);
            }
            // if collection of processing result
            else if (processingResult is IEnumerable<ProcessingResult>)
            {
                if (nodeImageKey == IMAGE_KEY_DEFAULT)
                    nodeImageKey = IMAGE_KEY_COMPOSITE_RESULT;
            }
            // if processing error result
            else if (processingResult is ProcessingErrorResult)
            {
                nodeImageKey = IMAGE_KEY_TRIGGER_ERROR;
            }
            return nodeImageKey;
        }

        /// <summary>
        /// Adds the tree node items.
        /// </summary>
        /// <param name="root">The root node.</param>
        /// <param name="collection">The collection of processing result.</param>
        private void AddTreeNodeItems(TreeNode root, IEnumerable collection)
        {
            AddTreeNodeItems(root, new AddedItemsInfo(0, collection));
        }

        /// <summary> 
        /// Adds the tree node items.
        /// </summary>
        /// <param name="root">The root node.</param>
        /// <param name="nextItemsInfo">The information about next items.</param>
        private void AddTreeNodeItems(TreeNode root, AddedItemsInfo nextItemsInfo)
        {
            int index = 0;
            int startIndex = nextItemsInfo.Count;
            int lastItemIndex = nextItemsInfo.Count + MAX_SHOW_PROCESSING_RESULTS - 1;

            foreach (object result in nextItemsInfo.Collection)
            {
                if (!(result is ProcessingResult))
                    continue;

                if (index >= startIndex)
                {
                    AddTreeNodeItem(root.Nodes, (ProcessingResult)result);
                    if (index == lastItemIndex)
                    {
                        string nodeName = string.Format("Show next {0} elements.", MAX_SHOW_PROCESSING_RESULTS);
                        TreeNode showNextElements = root.Nodes.Add(nodeName);
                        showNextElements.Tag = new AddedItemsInfo(index + 1, nextItemsInfo.Collection);
                        break;
                    }
                }
                index++;
            }
        }

        /// <summary>
        /// Returns the trigger image key.
        /// </summary>
        /// <param name="triggerSeverity">The trigger severity.</param>
        /// <returns>The trigger image key.</returns>
        private string GetTriggerImageKey(TriggerSeverity triggerSeverity)
        {
            switch (triggerSeverity)
            {
                case TriggerSeverity.Important:
                    return IMAGE_KEY_TRIGGER_ERROR;

                case TriggerSeverity.Unimportant:
                    return IMAGE_KEY_TRIGGER_WARNING;

                case TriggerSeverity.Information:
                    return IMAGE_KEY_TRIGGER_INFORMATION;
            }

            throw new NotImplementedException();
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
        /// Adds the image resource to the viewer.
        /// </summary>
        /// <param name="key">The key of image resource.</param>
        /// <param name="name">The name of image file.</param>
        private void AddImageResourceToImageList(string key, string name)
        {
            string resourceFormatName =
                "DemosCommonCode.Imaging.Processing.ProcessingResultViewer.Resources.{0}.png";

            string resourceFullName = string.Format(resourceFormatName, name);
            Bitmap bitmap = DemosResourcesManager.GetResourceAsBitmap(resourceFullName);
            ImageList.Images.Add(key, bitmap);
        }

        /// <summary>
        /// Returns a value that indicates that type name is correct.
        /// </summary>
        /// <param name="name">The type name.</param>
        /// <returns>
        /// <b>true</b> - if type name is correct.
        /// <b>false</b> - if type name is not correct.
        /// </returns>
        private bool GetIsValidTypename(string name)
        {
            // for each symbol in type name
            foreach (Char c in name)
            {
                // if symbol is not number or latin letter
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')))
                    // type name is not correct
                    return false;
            }
            // type name is correct
            return true;
        }

        #endregion

        #endregion

    }
}
