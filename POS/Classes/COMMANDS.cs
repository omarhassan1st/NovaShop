using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elections.Classes
{
     class COMMANDS
    {
        public delegate void Message(object txt);
        public static Message Error = (object txt) => MessageBox.Show(txt.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        public static Message Information = (object txt) => MessageBox.Show(txt.ToString(), "توضيح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        public static object Question(object txt)
        {
            DialogResult Answer = MessageBox.Show(txt.ToString(), "سؤال", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Answer == DialogResult.Yes)
                return "Yes";
            else
                return "No";
        }
    }
}
