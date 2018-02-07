using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms
{
    public enum AutoCapitalization
    {
		/// <summary>
		/// Don't set anything on the platform component.
		/// Currently this only affects iOS
		/// </summary>
		Default = 0,
		/// <summary>
		/// no automatic text capitalization.
		/// </summary>
		None = 1,
		/// <summary>
		/// capitalize the first character of every word.
		/// </summary>
		Words = 2,
		/// <summary>
		/// capitalize the first character of each sentence. 
		/// </summary>
		Sentences = 3,
		/// <summary>
		///  capitalize all characters. 
		/// </summary>
		Characters = 4
	}
}
