using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DemosCommonCode
{
    /// <summary>
    /// Represents controller of progress visualization of an action.
    /// </summary>
    public class StatusStripActionController
    {
        
        #region Fields

        /// <summary>
        /// The view of action animation.
        /// </summary>
        string[] _subActionAnimation = new string[] { ".", "..", "..." };
        
        /// <summary>
        /// The index of action animation view.
        /// </summary>
        int _subActionAnimationIndex = 0;
        
        /// <summary>
        /// The status label.
        /// </summary>
        ToolStripLabel _statusLabel;

        /// <summary>
        /// The progress bar.
        /// </summary>
        ToolStripProgressBar _progressBar;

        /// <summary>
        /// The status strip with status label and progress bar.
        /// </summary>
        ToolStrip _statusStrip;
        
        /// <summary>
        /// The action start time.
        /// </summary>
        DateTime _actionStartTime;

        /// <summary>
        /// The name of current action.
        /// </summary>
        string _actionName;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusStripActionController"/> class.
        /// </summary>
        /// <param name="statusStrip">The status strip.</param>
        /// <param name="statusLabel">The status label.</param>
        /// <param name="progressBar">The progress bar.</param>
        public StatusStripActionController(
            ToolStrip statusStrip,
            ToolStripLabel statusLabel,
            ToolStripProgressBar progressBar)
        {
            _statusLabel = statusLabel;
            _progressBar = progressBar;
            _statusStrip = statusStrip;
            Reset();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Visualizes the next sub action.
        /// </summary>
        public void NextSubAction()
        {
            NextSubAction(null);
        }

        /// <summary>
        ///  Visualizes the next sub action with specified name.
        /// </summary>
        /// <param name="name">The sub action name.</param>
        public void NextSubAction(string name)
        {
            if (_progressBar.Visible)
            {
                if (_progressBar.Value < _progressBar.Maximum)
                {
                    _progressBar.Value++;
                }
            }
            else
            {
                if (name == null)
                {
                    _statusLabel.Text = string.Format("{0}{1}", _actionName, _subActionAnimation[_subActionAnimationIndex]);
                    _subActionAnimationIndex++;
                    _subActionAnimationIndex %= _subActionAnimation.Length;
                }
                else
                {
                    _statusLabel.Text = string.Format("{0}... ({1})", _actionName, name);
                }
            }
            _statusStrip.Update();
        }

        /// <summary>
        /// Starts an action with specified name.
        /// </summary>
        /// <param name="anctionName">Name of the action.</param>
        public void StartAction(string anctionName)
        {
            StartAction(anctionName, 0);
        }

        /// <summary>
        /// Starts an action with specified name and count of sub actions.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="progressSteps">The progress steps.</param>
        public void StartAction(string actionName, int progressSteps)
        {
            _actionName = actionName;
            bool useProgress = progressSteps > 0;
            if (useProgress)
                _statusLabel.Text = string.Format("{0}: ", _actionName);
            else
                _statusLabel.Text = string.Format("{0}...", _actionName);
            _statusLabel.Visible = true;
            if (useProgress)
            {
                _progressBar.Visible = true;
                _progressBar.Maximum = progressSteps;
                _progressBar.Value = 0;
            }
            _actionStartTime = DateTime.Now;
            _statusStrip.Update();
        }

        /// <summary>
        /// Ends the action.
        /// </summary>
        public void EndAction()
        {
            double actionMs = (DateTime.Now - _actionStartTime).TotalMilliseconds;
            _progressBar.Visible = false;
            _statusLabel.Text = string.Format("{0}: {1} ms.", _actionName, actionMs);
        }

        /// <summary>
        /// Resets this action controller.
        /// </summary>
        public void Reset()
        {
            _statusLabel.Visible = false;
            _progressBar.Visible = false;
        }

        #endregion

    }
}
