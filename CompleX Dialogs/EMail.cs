using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;

namespace CompleX.Presentation.Controls
{
	/// <summary>
	/// Klasse um eine Mail zu erzeugen
	/// </summary>
	public class EMail
	{

        /// <summary>
        /// Gibt den namen des Standard Mail programms zurück
        /// </summary>
        public static string DefaultEmailClientName
        {
            get
            {
                try
                {
                    RegistryKey hklm = Registry.LocalMachine;
                    RegistryKey mailClients = hklm.OpenSubKey("SOFTWARE\\Clients\\Mail");
                    if (mailClients != null)
                        return (string)mailClients.GetValue("");
                }
                catch (Exception)
                {
                    return string.Empty;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Gibt an ob das senden der email funktionieren kann
        /// </summary>
        public static bool CanWork
        {
            get { return !String.IsNullOrEmpty(DefaultEmailClientName); }
        }

		/// <summary>
		/// Empfänger hinzufügen
		/// </summary>
		/// <param name="email">adresse</param>
		public bool AddRecipientTo(string email)
		{
			return AddRecipient(email, HowTo.MAPI_TO);
		}

		/// <summary>
		/// Empfänger CC hinzufügen
		/// </summary>
		/// <param name="email">adresse</param>
		public bool AddRecipientCC(string email)
		{
			return AddRecipient(email, HowTo.MAPI_TO);
		}

		/// <summary>
		/// Empfänger BCC hinzufügen
		/// </summary>
		/// <param name="email">adresse</param>
		public bool AddRecipientBCC(string email)
		{
			return AddRecipient(email, HowTo.MAPI_TO);
		}

		/// <summary>
		/// Fügt einen Dateianhang hinzu
		/// </summary>
		/// <param name="fileName">Datei</param>
		public void AddAttachment(string fileName)
		{
			m_attachments.Add(fileName);
		}

		/// <summary>
		/// Sends the mail popup.
		/// </summary>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		public int SendMailPopup(string subject, string body)
		{
			return SendMail(subject, body, MAPI_LOGON_UI | MAPI_DIALOG);
		}

		/// <summary>
		/// Sends the mail direct.
		/// </summary>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		public int SendMailDirect(string subject, string body)
		{
			return SendMail(subject, body, MAPI_LOGON_UI);
		}

		[DllImport("MAPI32.DLL")]
		static extern int MAPISendMail(IntPtr sess, IntPtr hwnd, MapiMessage message, int flg, int rsv);

		/// <summary>
		/// Sends the mail.
		/// </summary>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		/// <param name="how">The how.</param>
		int SendMail(string subject, string body, int how)
		{
			MapiMessage msg = new MapiMessage();
			msg.subject = subject;
			msg.noteText = body;

			msg.recips = GetRecipients(out msg.recipCount);
			msg.files = GetAttachments(out msg.fileCount);

			m_lastError = MAPISendMail(new IntPtr(0), new IntPtr(0), msg, how, 0);
			if (m_lastError > 1)
				throw new InvalidOperationException("MAPISendMail failed! " + GetLastError());

			Cleanup(ref msg);
			return m_lastError;
		}

		bool AddRecipient(string email, HowTo howTo)
		{
			MapiRecipDesc recipient = new MapiRecipDesc();

			recipient.recipClass = (int)howTo;
			recipient.name = email;
			m_recipients.Add(recipient);

			return true;
		}

		IntPtr GetRecipients(out int recipCount)
		{
			recipCount = 0;
			if (m_recipients.Count == 0)
				return IntPtr.Zero;

			int size = Marshal.SizeOf(typeof(MapiRecipDesc));
			IntPtr intPtr = Marshal.AllocHGlobal(m_recipients.Count * size);

			int ptr = (int)intPtr;
			foreach (MapiRecipDesc mapiDesc in m_recipients)
			{
				Marshal.StructureToPtr(mapiDesc, (IntPtr)ptr, false);
				ptr += size;
			}

			recipCount = m_recipients.Count;
			return intPtr;
		}

		IntPtr GetAttachments(out int fileCount)
		{
			fileCount = 0;
			if (m_attachments == null)
				return IntPtr.Zero;

			if ((m_attachments.Count <= 0) || (m_attachments.Count > maxAttachments))
				return IntPtr.Zero;

			int size = Marshal.SizeOf(typeof(MapiFileDesc));
			IntPtr intPtr = Marshal.AllocHGlobal(m_attachments.Count * size);

			MapiFileDesc mapiFileDesc = new MapiFileDesc();
			mapiFileDesc.position = -1;
			int ptr = (int)intPtr;
			
			foreach (string strAttachment in m_attachments)
			{
				mapiFileDesc.name = Path.GetFileName(strAttachment);
				mapiFileDesc.path = strAttachment;
				Marshal.StructureToPtr(mapiFileDesc, (IntPtr)ptr, false);
				ptr += size;
			}

			fileCount = m_attachments.Count;
			return intPtr;
		}

		void Cleanup(ref MapiMessage msg)
		{
			int size = Marshal.SizeOf(typeof(MapiRecipDesc));
			int ptr;

			if (msg.recips != IntPtr.Zero)
			{
				ptr = (int)msg.recips;
				for (int i = 0; i < msg.recipCount; i++)
				{
					Marshal.DestroyStructure((IntPtr)ptr, typeof(MapiRecipDesc));
					ptr += size;
				}
				Marshal.FreeHGlobal(msg.recips);
			}

			if (msg.files != IntPtr.Zero)
			{
				size = Marshal.SizeOf(typeof(MapiFileDesc));

				ptr = (int)msg.files;
				for (int i = 0; i < msg.fileCount; i++)
				{
					Marshal.DestroyStructure((IntPtr)ptr, typeof(MapiFileDesc));
					ptr += size;
				}
				Marshal.FreeHGlobal(msg.files);
			}
			
			m_recipients.Clear();
			m_attachments.Clear();
			m_lastError = 0;
		}

		/// <summary>
		/// Gets the last error.
		/// </summary>
		public string GetLastError()
		{
			if (m_lastError <= 26)
				return errors[ m_lastError ];
			return "MAPI error [" + m_lastError + "]";
		}

		readonly string[] errors = new[] {
													"OK [0]", "User abort [1]", "General MAPI failure [2]", "MAPI login failure [3]",
													"Disk full [4]", "Insufficient memory [5]", "Access denied [6]", "-unknown- [7]",
													"Too many sessions [8]", "Too many files were specified [9]", "Too many recipients were specified [10]", "A specified attachment was not found [11]",
													"Attachment open failure [12]", "Attachment write failure [13]", "Unknown recipient [14]", "Bad recipient type [15]",
													"No messages [16]", "Invalid message [17]", "Text too large [18]", "Invalid session [19]",
													"Type not supported [20]", "A recipient was specified ambiguously [21]", "Message in use [22]", "Network failure [23]",
													"Invalid edit fields [24]", "Invalid recipients [25]", "Not supported [26]" 
												};


		readonly List<MapiRecipDesc> m_recipients	= new List<MapiRecipDesc>();
		readonly List<string> m_attachments	= new List<string>();
		int m_lastError;

		const int MAPI_LOGON_UI = 0x00000001;
		const int MAPI_DIALOG = 0x00000008;
		const int maxAttachments = 20;

		enum HowTo{MAPI_ORIG=0, MAPI_TO, MAPI_CC, MAPI_BCC};
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal class MapiMessage
	{
		public int reserved;
		public string subject;
		public string noteText;
		public string messageType;
		public string dateReceived;
		public string conversationID;
		public int flags;
		public IntPtr originator;
		public int recipCount;
		public IntPtr recips;
		public int fileCount;
		public IntPtr files;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal class MapiFileDesc
	{
		public int reserved;
		public int flags;
		public int position;
		public string path;
		public string name;
		public IntPtr type;
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
	internal class MapiRecipDesc
	{
		public int		reserved;
		public int		recipClass;
		public string	name;
		public string	address;
		public int		eIDSize;
		public IntPtr	entryID;
	}
}