namespace PersonalOrganizerApp
{
    partial class ReminderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvReminders = new System.Windows.Forms.DataGridView();
            this.btnAddReminder = new System.Windows.Forms.Button();
            this.btnUpdateReminder = new System.Windows.Forms.Button();
            this.btnDeleteReminder = new System.Windows.Forms.Button();
            this.btnFilterMeeting = new System.Windows.Forms.Button();
            this.btnFilterTask = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReminders)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReminders
            // 
            this.dgvReminders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReminders.Location = new System.Drawing.Point(207, 34);
            this.dgvReminders.Name = "dgvReminders";
            this.dgvReminders.RowHeadersWidth = 51;
            this.dgvReminders.RowTemplate.Height = 24;
            this.dgvReminders.Size = new System.Drawing.Size(606, 348);
            this.dgvReminders.TabIndex = 0;
            // 
            // btnAddReminder
            // 
            this.btnAddReminder.Location = new System.Drawing.Point(75, 426);
            this.btnAddReminder.Name = "btnAddReminder";
            this.btnAddReminder.Size = new System.Drawing.Size(122, 63);
            this.btnAddReminder.TabIndex = 1;
            this.btnAddReminder.Text = "Add Reminder";
            this.btnAddReminder.UseVisualStyleBackColor = true;
            // 
            // btnUpdateReminder
            // 
            this.btnUpdateReminder.Location = new System.Drawing.Point(232, 421);
            this.btnUpdateReminder.Name = "btnUpdateReminder";
            this.btnUpdateReminder.Size = new System.Drawing.Size(110, 67);
            this.btnUpdateReminder.TabIndex = 2;
            this.btnUpdateReminder.Text = "Update";
            this.btnUpdateReminder.UseVisualStyleBackColor = true;
            // 
            // btnDeleteReminder
            // 
            this.btnDeleteReminder.Location = new System.Drawing.Point(383, 424);
            this.btnDeleteReminder.Name = "btnDeleteReminder";
            this.btnDeleteReminder.Size = new System.Drawing.Size(108, 63);
            this.btnDeleteReminder.TabIndex = 3;
            this.btnDeleteReminder.Text = "Delete";
            this.btnDeleteReminder.UseVisualStyleBackColor = true;
            // 
            // btnFilterMeeting
            // 
            this.btnFilterMeeting.Location = new System.Drawing.Point(555, 430);
            this.btnFilterMeeting.Name = "btnFilterMeeting";
            this.btnFilterMeeting.Size = new System.Drawing.Size(132, 58);
            this.btnFilterMeeting.TabIndex = 4;
            this.btnFilterMeeting.Text = "Meeting";
            this.btnFilterMeeting.UseVisualStyleBackColor = true;
            // 
            // btnFilterTask
            // 
            this.btnFilterTask.Location = new System.Drawing.Point(728, 433);
            this.btnFilterTask.Name = "btnFilterTask";
            this.btnFilterTask.Size = new System.Drawing.Size(110, 55);
            this.btnFilterTask.TabIndex = 5;
            this.btnFilterTask.Text = "Task";
            this.btnFilterTask.UseVisualStyleBackColor = true;
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(877, 427);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(113, 61);
            this.btnShowAll.TabIndex = 6;
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.UseVisualStyleBackColor = true;
            // 
            // ReminderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.btnFilterTask);
            this.Controls.Add(this.btnFilterMeeting);
            this.Controls.Add(this.btnDeleteReminder);
            this.Controls.Add(this.btnUpdateReminder);
            this.Controls.Add(this.btnAddReminder);
            this.Controls.Add(this.dgvReminders);
            this.Name = "ReminderForm";
            this.Text = "Reminder";
            this.Load += new System.EventHandler(this.ReminderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReminders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReminders;
        private System.Windows.Forms.Button btnAddReminder;
        private System.Windows.Forms.Button btnUpdateReminder;
        private System.Windows.Forms.Button btnDeleteReminder;
        private System.Windows.Forms.Button btnFilterMeeting;
        private System.Windows.Forms.Button btnFilterTask;
        private System.Windows.Forms.Button btnShowAll;
    }
}