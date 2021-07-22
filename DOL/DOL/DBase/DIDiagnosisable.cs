/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DOL.DBase.DIDiagnosisable Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DOL.DBase.DIDiagnosisable.cs: implementation of the DOL.DBase.DIDiagnosisable class.
//
///////////////////////////////////////////////////////////////////////////////

using System.Text;

namespace DOL.DBase
{
	/// <summary>
	/// DOL.DBase.DIDiagnosisable
	/// </summary>
	public interface DIDiagnosisable
	{

	/////////////////////////////////////////////////////////////////////////////////
	#region 基本操作	

		/////////////////////////////////////////////////////////////////////////////        
		/// <summary>
		/// 是否為有效物件
		/// </summary>
		void AssertValid();

		/////////////////////////////////////////////////////////////////////////////       
		/// <summary>
		/// 傾印物件
		/// </summary>
		/// <param name="buffer"> 傾印之緩衝區 </param>
		/// <param name="prefix"> 縮排文字 </param>
		void Dump(StringBuilder buffer, string prefix);

    #endregion

	}
}
