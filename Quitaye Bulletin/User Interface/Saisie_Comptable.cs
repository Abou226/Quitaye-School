using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Saisie_Comptable : Form
    {
        Form currentChildForm;
        public Saisie_Comptable()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;


            temp = 1;
            timer1.Start();
        }

        public int temp;
        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == 0x84)
            //{
            //    Point pos = new Point(m.LParam.ToInt32());
            //    pos = this.PointToClient(pos);
            //    if (pos.Y < cCaption)
            //    {
            //        m.Result = (IntPtr)2;
            //        return;
            //    }
            //    if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
            //    {
            //        m.Result = (IntPtr)17;
            //        return;
            //    }
            //}
            //base.WndProc(ref m);


            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }


        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Saisie_Comptable_Load(object sender, EventArgs e)
        {

        }

        private void ChildForm(Form form)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(form);
            panelContenedor.Tag = form;
            form.BringToFront();
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (temp == 1)
            {
                //if (lblTitleChildForm.Text == "Recette(s)" || lblTitleChildForm.Text == "Dépense(s)")
                //{
                //    if (lblTitleChildForm.Text == "Recette(s)")
                //    {

                //        ChildForm(new Dépenses_Recette(lblTitleChildForm.Text));
                //    }
                //    else
                //    {

                //        ChildForm(new Dépenses_Recette(lblTitleChildForm.Text));
                //    }
                //}
                temp = 0;
                timer1.Stop();
            }
        }
    }
}
