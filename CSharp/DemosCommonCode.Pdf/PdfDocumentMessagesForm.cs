using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vintasoft.Imaging.Pdf;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view warning and errors, which are occured during
    /// loading or rendering of PDF document.
    /// </summary>
    public partial class PdfDocumentMessagesForm : Form
    {

        #region Fields

        PdfRuntimeMessage[] _messages;

        #endregion



        #region Constructor

        public PdfDocumentMessagesForm(IList<PdfRuntimeMessage> messages)
        {
            InitializeComponent();

            _messages = new PdfRuntimeMessage[messages.Count];
            messages.CopyTo(_messages, 0);

            Init();
        }

        public PdfDocumentMessagesForm(PdfRuntimeMessage[] messages)
        {
            InitializeComponent();

            _messages = messages;

            Init();
        }      

        #endregion



        #region Methods
        
        private void Init()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            Dictionary<string, string> types = new Dictionary<string, string>();
            for (int i = 0; i < _messages.Length; i++)
            {
                PdfRuntimeMessage msg = _messages[i];
                string name = msg.ToString();
                if (counts.ContainsKey(name))
                {
                    counts[name] = counts[name] + 1;
                }
                else
                {
                    counts.Add(name, 1);
                    string messageType = null;
                    if (msg is PdfRuntimeError)
                        messageType = "Error";
                    else if (msg is PdfRuntimeWarning)
                        messageType = "Warning";
                    else
                        messageType = "Message";
                    types.Add(name, messageType);
                }
            }
            FillData(counts, types);
        }

        private void FillData(Dictionary<string, int> counts, Dictionary<string, string> types)
        {
            foreach (string name in counts.Keys)
            {
                int index = dataGridView.Rows.Count;
                dataGridView.Rows.Add();
                dataGridView.Rows[index].Cells[0].Value = types[name];
                dataGridView.Rows[index].Cells[1].Value = counts[name];
                dataGridView.Rows[index].Cells[2].Value = name;
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of dataGridView object.
        /// </summary>
        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _messages.Length)
            {
                PdfRuntimeMessage message = _messages[e.RowIndex];
                string text = message.ToString();
                if (message.InnerException != null)
                {
                    text += Environment.NewLine;
                    text += string.Format("{0}: {1}", message.InnerException.GetType(), message.Message);
                    text += Environment.NewLine;
                    text += string.Format("StackTrace: {0}", message.InnerException.StackTrace);
                }
                Clipboard.SetText(text);
                MessageBox.Show(text);
            }
        }

        #endregion

    }
}
