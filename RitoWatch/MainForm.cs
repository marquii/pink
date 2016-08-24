/*
 * Created by SharpDevelop.
 * User: marcs
 * Date: 15/07/2016
 * Time: 15:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace RitoWatch
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void processPing(long rtt) {
			if (rtt<30) {
				this.BackColor=Color.Lime;
			} else if (rtt<70) {
				this.BackColor=Color.Green;
			} else if (rtt<120) {
				this.BackColor=Color.Yellow;
			} else if (rtt<300) {
				this.BackColor=Color.Orange;
			} else {
				this.BackColor=Color.Red;
			}
			label1.Text=rtt.ToString();
		}
		
		String dispatchServer() {
			String[] prodServers = {"prod.euw1.lol.riotgames.com","prod.na2.lol.riotgames.com","prod.eun1.lol.riotgames.com",
				"prod.kr.lol.riotgames.com","prod.br.lol.riotgames.com","prod.tr.lol.riotgames.com","prod.ru.lol.riotgames.com",
				"prod.la1.lol.riotgames.com","prod.la2.lol.riotgames.com","prod.oc1.lol.riotgames.com","prod.pbe1.lol.riotgames.com"};
			String[] chatServers = {"chat.euw1.lol.riotgames.com","chat.na2.lol.riotgames.com","chat.eun1.lol.riotgames.com",
				"chat.kr.lol.riotgames.com","chat.br.lol.riotgames.com","chat.tr.lol.riotgames.com","chat.ru.lol.riotgames.com",
				"chat.la1.lol.riotgames.com","chat.la2.lol.riotgames.com","chat.oc1.lol.riotgames.com","chat.pbe1.lol.riotgames.com"};
			String[] lqServers = {"lq.euw1.lol.riotgames.com","lq.na2.lol.riotgames.com","lq.eun1.lol.riotgames.com",
				"lq.kr.lol.riotgames.com","lq.br.lol.riotgames.com","lq.tr.lol.riotgames.com","lq.ru.lol.riotgames.com",
				"lq.la1.lol.riotgames.com","lq.la2.lol.riotgames.com","lq.oc1.lol.riotgames.com","lq.pbe1.lol.riotgames.com"};
			switch (comboBox2.SelectedIndex) {
				case 0:
					return prodServers[comboBox1.SelectedIndex];
				case 1:
					return lqServers[comboBox1.SelectedIndex];
				case 2:
					return chatServers[comboBox1.SelectedIndex];
				default:
					return "prod.euw1.lol.riotgames.com";
			}
		}
		
		void Timer1Tick(object sender, EventArgs e)
		{
			Ping p = new Ping();
			p.PingCompleted+=pingCompletedCallback;
			p.SendAsync(dispatchServer(),new System.Threading.AutoResetEvent(false));
			
		}
		
		void pingCompletedCallback(object sender, PingCompletedEventArgs e) {
			PingReply pr = e.Reply;
			if (pr.Status.Equals(IPStatus.Success)) {
				processPing(pr.RoundtripTime);
			} else {
				this.BackColor=Color.Maroon;
				label1.Text = "--";
			}
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			Timer1Tick(sender,e);
		}
		void Label1Click(object sender, EventArgs e)
		{
	
		}
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			label1.Text="...";
			this.BackColor=Color.Gray;
			Timer1Tick(sender,e);
		}
	}
}
